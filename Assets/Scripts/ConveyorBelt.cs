using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private MeshFilter meshFilter1;
    [SerializeField] private MeshFilter meshFilter2;
    [SerializeField] private LineRenderer lineRenderer;

    public float speed = 0.1f;

    Vector3[] pathPoints;
    Vector3[] normalVecs;
    public float pathLength;

    private ConveyorBeltBuilder beltBuilder;

    public void Reset()
    {
        lineRenderer.enabled = false;
        meshFilter1.mesh = null;
        meshFilter2.mesh = null;
        pathPoints = null;
        normalVecs = null;
    }

    public void SetUpBelt(Vector3[] points, Vector3[] normals, Mesh m1, Mesh m2)
    {
        meshFilter1.mesh = m1;
        meshFilter2.mesh = m2;

        int n = points.Length;
        pathPoints = new Vector3[n];
        normalVecs = new Vector3[n];
        lineRenderer.positionCount = n;
        pathLength = 0;
        for (int i = 0; i < n; i++)
        {
            pathPoints[i] = points[i];
            normalVecs[i] = normals[i];
            lineRenderer.SetPosition(i, pathPoints[i]);

            if (i < n - 1)
            {
                pathLength += Vector3.Distance(pathPoints[i], pathPoints[i + 1]);
            }
        }
    }

    public void ShowPath()
    {
        lineRenderer.enabled = true;
    }

    // Function to get the position on the path given the distance traveled
    public void GetPositionOnPath(float distanceTraveled, out Vector3 position, out Vector3 normal)
    {
        // Calculate the percentage of distance traveled along the path
        float t = Mathf.Clamp01(distanceTraveled / pathLength);

        // Calculate the index of the segment in the path array
        int startIndex = Mathf.FloorToInt(t * (pathPoints.Length - 1));
        int endIndex = Mathf.Min(startIndex + 1, pathPoints.Length - 1);

        // Calculate the percentage of distance traveled within this segment
        float segmentT = (t * (pathPoints.Length - 1)) - startIndex;

        // Interpolate between the start and end points of the segment
        position = Vector3.Lerp(pathPoints[startIndex], pathPoints[endIndex], segmentT);
        normal = Vector3.Lerp(normalVecs[startIndex], normalVecs[endIndex], segmentT);
    }

    // Start is called before the first frame update
    void Start()
    {
        beltBuilder = FindFirstObjectByType<ConveyorBeltBuilder>();
        beltBuilder.Evt_OnBeltDestroyed.AddListener(Reset);
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (meshFilter1.mesh != null)
        {
            meshFilter1.gameObject.GetComponent<MeshRenderer>().material.mainTextureOffset += new Vector2(0, -1) * speed * Time.deltaTime;
        }
        if (meshFilter2.mesh != null)
        {
            meshFilter2.gameObject.GetComponent<MeshRenderer>().material.mainTextureOffset += new Vector2(0, -1) * speed * Time.deltaTime;
        }
    }
}
