using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Events;
using static OVRPlugin;

public class ConveyorBeltBuilder : MonoBehaviour
{
    public GameObject portalPrefab;
    public GameObject portalPreviewPrefab;

    private GameObject portal1;
    private GameObject portal2;
    private GameObject currentPreview;

    public int numberOfPoints = 100;
    public float beltwidth = 0.2f;

    private Vector3[] pathPoints;
    private Vector3[] normalVecs;

    public ConveyorBelt beltPrefab;
    public ConveyorBelt belt;

    public bool startBuilding;
    public bool isBeltBuilt;

    public UnityEvent Evt_OnBeltBuilt;
    public UnityEvent Evt_OnBeltDestroyed;

    [SerializeField] private HandTrackingManager _handTrackingManager;
    [SerializeField] private LayerMask meshLayerMask;
    private OVRHand _dominantHand;
    private (Vector3 point, Vector3 normal, bool hit) _rightHandHit;

    // Start is called before the first frame update
    void Start()
    {
        //startBuilding = false;
        pathPoints = new Vector3[numberOfPoints];
        normalVecs = new Vector3[numberOfPoints];
        belt = Instantiate(beltPrefab);
#if UNITY_EDITOR
#else
        if (_handTrackingManager == null)
        {
            _handTrackingManager = GameStateMachine.Instance.HandTrackingManager;
            _dominantHand = _handTrackingManager.GetDominateHand();
        }
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (!startBuilding)
        {
            return;
        }

        HandlePortalInstantiation();
        HandleConveyorBeltBuildup();

#if UNITY_EDITOR
#else
        // TODO: remove this when done
        //if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        //{
        //    Reset();
        //}
#endif
    }

    public void Reset()
    {
        Destroy(portal1);
        Destroy(portal2);
        isBeltBuilt = false;
        belt.gameObject.SetActive(false);
        Evt_OnBeltDestroyed.Invoke();
    }

    public void HandlePortalInstantiation()
    {
        if (currentPreview == null)
        {
            currentPreview = Instantiate(portalPreviewPrefab);
        }

        if (portal1 != null && portal2 != null)
        {
            return;
        }

#if UNITY_EDITOR
        portal1 = Instantiate(portalPrefab, Vector3.right, Quaternion.identity);
        portal2 = Instantiate(portalPrefab, -Vector3.right, Quaternion.identity);
#else
        HandleHitDetectionHands();
        //Vector3 controllerPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        //Quaternion controllerRot = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
        //Vector3 rayDirection = controllerRot * Vector3.forward;
        //if (Physics.Raycast(controllerPos, rayDirection, out RaycastHit hit))
        //{
        //    currentPreview.transform.position = hit.point;
        //    currentPreview.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

        //    // TODO: ask meta why this line fails sometimes
        //    OVRSemanticClassification anchor = hit.collider.gameObject.GetComponent<OVRSemanticClassification>();
        //    //if (anchor != null)
        //    //{
        //        //Debug.Log($"anchor label: { string.Join(", ", anchor.Labels)}");
        //        if (OVRInput.GetDown(OVRInput.Button.One))
        //        {
        //            if (portal1 == null)
        //            {
        //                portal1 = Instantiate(portalPrefab, currentPreview.transform.position, currentPreview.transform.rotation);
        //            }
        //            else
        //            {
        //                portal2 = Instantiate(portalPrefab, currentPreview.transform.position, currentPreview.transform.rotation);
        //            }
        //        }
        //    //}
        //}
#endif
    }

    public void HandleConveyorBeltBuildup()
    {
        if (portal1 != null && portal2 != null && !isBeltBuilt)
        {
            belt.gameObject.SetActive(true);
            GeneratePathPoints();
            GenerateBelt();
            isBeltBuilt = true;
            Evt_OnBeltBuilt.Invoke();
        }
    }

    void GeneratePathPoints()
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            float t = (float)i / (numberOfPoints - 1);
            Vector3 position = CalculateBeltPath(portal1.transform.position, portal2.transform.position, portal1.transform.up, -portal2.transform.up, t);
            pathPoints[i] = position;
        }
    }

    Vector3 CalculateBeltPath(Vector3 p0, Vector3 p1, Vector3 m0, Vector3 m1, float t)
    {
        // half circle
        if (Mathf.Approximately(Vector3.Dot(m0.normalized, m1.normalized), -1f))
        {
            // Calculate the center of the circle
            Vector3 center = p0 + (p1 - p0) * 0.5f;

            // Calculate the angle
            float angle = Mathf.PI * t;

            // Calculate r vector
            Vector3 r = p0 - center;

            Vector3 pointOnCircle = center + r * Mathf.Cos(angle) + m0 * Mathf.Sin(angle);
            return pointOnCircle;
        }
        // portals on opposite walls and all other cases
        else
        {
            return CalculateCubicHermiteSpline(p0, p1, m0, m1, t);
        }
    }

    Vector3 CalculateCubicHermiteSpline(Vector3 p0, Vector3 p1, Vector3 m0, Vector3 m1, float t)
    {
        float t2 = t * t;
        float t3 = t2 * t;

        float h00 = 2 * t3 - 3 * t2 + 1;
        float h01 = -2 * t3 + 3 * t2;
        float h10 = t3 - 2 * t2 + t;
        float h11 = t3 - t2;

        return h00 * p0 + h01 * p1 + h10 * m0 + h11 * m1;
    }

    private void GenerateBeltMesh(out UnityEngine.Mesh m1, out UnityEngine.Mesh m2)
    {
        //// Create a new mesh
        //Mesh mesh = new Mesh();

        //// Calculate points around the path
        //Vector3[] vertices = new Vector3[pathPoints.Length * numSegments];
        //int[] triangles = new int[6 * (pathPoints.Length - 1) * numSegments];

        //for (int i = 0; i < pathPoints.Length; i++)
        //{
        //    Vector3 pointA = pathPoints[i];
        //    Vector3 pointB;
        //    if (i == pathPoints.Length - 1)
        //    {
        //        pointB = 2 * pointA - pathPoints[i - 1];
        //    }
        //    else
        //    {
        //        pointB = pathPoints[i + 1];
        //    }

        //    Vector3 direction = (pointB - pointA).normalized;
        //    Vector3 perpendicular1 = Vector3.Cross(direction, Vector3.up).normalized;
        //    Vector3 perpendicular2 = Vector3.Cross(direction, perpendicular1).normalized;

        //    for (int j = 0; j < numSegments; j++)
        //    {
        //        float angle = 2f * Mathf.PI * j / numSegments;
        //        Vector3 circlePoint = Mathf.Cos(angle) * tubeRadius * perpendicular1 + Mathf.Sin(angle) * tubeRadius * perpendicular2;
        //        vertices[i * numSegments + j] = pointA + circlePoint;
        //    }
        //}

        //// Set up triangles
        //int triIndex = 0;
        //for (int i = 0; i < pathPoints.Length - 1; i++)
        //{
        //    for (int j = 0; j < numSegments; j++)
        //    {
        //        int currentVert = i * numSegments + j;
        //        int nextVert = ((i + 1) * numSegments + j) % (pathPoints.Length * numSegments);
        //        int nextRowVert = ((i + 1) * numSegments + (j + 1) % numSegments) % (pathPoints.Length * numSegments);

        //        triangles[triIndex++] = currentVert;
        //        triangles[triIndex++] = nextRowVert;
        //        triangles[triIndex++] = nextVert;

        //        triangles[triIndex++] = currentVert;
        //        triangles[triIndex++] = currentVert + 1;
        //        triangles[triIndex++] = nextRowVert + 1;
        //    }
        //}

        //// Assign vertices and triangles to the mesh
        //mesh.vertices = vertices;
        //mesh.triangles = triangles;

        //// Recalculate normals and bounds
        //mesh.RecalculateNormals();
        //mesh.RecalculateBounds();

        //GetComponent<MeshFilter>().mesh = mesh;

        m1 = new UnityEngine.Mesh();
        m2 = new UnityEngine.Mesh();
        List<Vector3> verts1 = new List<Vector3>();
        List<Vector3> verts2 = new List<Vector3>();
        List<int> tris = new List<int>();

        Vector3[] p1s = new Vector3[numberOfPoints];
        Vector3[] p2s = new Vector3[numberOfPoints];
        Vector3[] directions = new Vector3[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            if (i < numberOfPoints - 1)
            {
                directions[i] = pathPoints[i + 1] - pathPoints[i];
            }
            else
            {
                directions[i] = pathPoints[i] - pathPoints[i - 1];
            }
            // TODO: I don't have a better way to deal with the line below
            Vector3 perpendicular = Vector3.Cross(Vector3.up + 0.01f * Vector3.left, directions[i]).normalized;
            p1s[i] = pathPoints[i] + perpendicular * beltwidth;
            p2s[i] = pathPoints[i] - perpendicular * beltwidth;
            normalVecs[i] = Vector3.Cross(directions[i], perpendicular).normalized;
        }

        Vector2[] uvs1 = new Vector2[4 * numberOfPoints - 4];
        Vector2[] uvs2 = new Vector2[4 * numberOfPoints - 4];
        for (int i = 1; i < numberOfPoints; i++)
        {
            Vector3 p1 = p1s[i - 1];
            Vector3 p2 = p2s[i - 1];
            Vector3 p3 = p1s[i];
            Vector3 p4 = p2s[i];

            int offset = 4 * (i - 1);

            int t1 = offset + 0;
            int t2 = offset + 2;
            int t3 = offset + 3;

            int t4 = offset + 3;
            int t5 = offset + 1;
            int t6 = offset + 0;

            verts1.AddRange(new List<Vector3> { p1, p2, p3, p4 });
            verts2.AddRange(new List<Vector3> { p2, p1, p4, p3 });
            tris.AddRange(new List<int> { t1, t2, t3, t4, t5, t6 });

            Vector3 dir1 = Vector3.Cross(normalVecs[i], directions[i]).normalized / directions[i].magnitude;
            Vector3 dir2 = directions[i].normalized / directions[i].magnitude;
            uvs1[4 * i - 4] = new Vector2(Vector3.Dot(p1 - p1, dir1), Vector3.Dot(p1 - p1, dir2));
            uvs1[4 * i - 3] = new Vector2(Vector3.Dot(p2 - p1, dir1), Vector3.Dot(p2 - p1, dir2));
            uvs1[4 * i - 2] = new Vector2(Vector3.Dot(p3 - p1, dir1), Vector3.Dot(p3 - p1, dir2));
            uvs1[4 * i - 1] = new Vector2(Vector3.Dot(p4 - p1, dir1), Vector3.Dot(p4 - p1, dir2));

            uvs2[4 * i - 4] = uvs1[4 * i - 3];
            uvs2[4 * i - 3] = uvs1[4 * i - 4];
            uvs2[4 * i - 2] = uvs1[4 * i - 1];
            uvs2[4 * i - 1] = uvs1[4 * i - 2];
        }

        m1.SetVertices(verts1);
        m1.SetTriangles(tris, 0);
        m1.uv = uvs1;

        m2.SetVertices(verts2);
        m2.SetTriangles(tris, 0);
        m2.uv = uvs2;
    }

    private void GenerateBelt()
    {
        UnityEngine.Mesh m1, m2;
        GenerateBeltMesh(out m1, out m2);
        belt.SetUpBelt(pathPoints, normalVecs, m1, m2);
        belt.ShowPath();
    }

#if UNITY_EDITOR
#else
    private void HandleHitDetectionHands()
    {
        CheckRaycastHit(_dominantHand, out _rightHandHit);

        if (currentPreview != null && _rightHandHit.hit)
        {
            currentPreview.transform.position = _rightHandHit.point;
            currentPreview.transform.rotation = Quaternion.FromToRotation(Vector3.up, _rightHandHit.normal);
            if (_handTrackingManager.IsPinching())
            {
                if (portal1 == null)
                {
                    portal1 = Instantiate(portalPrefab, currentPreview.transform.position, currentPreview.transform.rotation);
                }
                else
                {
                    portal2 = Instantiate(portalPrefab, currentPreview.transform.position, currentPreview.transform.rotation);
                    Destroy(currentPreview);
                }
            }
        }
    }

    private void CheckRaycastHit(OVRHand hand, out (Vector3 point, Vector3 normal, bool hit) raycastHit)
    {
        Ray ray = new Ray(hand.PointerPose.position, hand.PointerPose.forward);
        bool success = Physics.Raycast(ray, out RaycastHit hitInfo, 100.0f, meshLayerMask);
        raycastHit = (hitInfo.point, hitInfo.normal, success);
    }
#endif
}
