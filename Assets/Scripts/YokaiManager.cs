using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class YokaiManager : MonoBehaviour
{
    [SerializeField] private bool debugMode = true;

    public YokaiSO[] yokais;

    public MultiversePortal portalPrefab;
    private MultiversePortal portal;

    private int ind;
    private Vector3 instantiatePos;
    private Yokai yokai;

    private float timer = 0;
    public float moveTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        ind = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool InstantiateYokai()
    {
        if (ind >= yokais.Length)
        {
            return false;
        }
        if (ind == 0)
        {
            portal = Instantiate(portalPrefab, Vector3.zero, Quaternion.identity);
            instantiatePos = new Vector3(Camera.main.transform.position.x, portal.GetHeightAboveGround(), Camera.main.transform.position.z) + Camera.main.transform.forward * 2f;
            portal.transform.position = instantiatePos;
            portal.transform.LookAt(new Vector3(Camera.main.transform.position.x, portal.transform.position.y, Camera.main.transform.position.z));
        }
        portal.PlayEngineAudio();
        StartCoroutine(Co_InstantiateYokai());
        return true;
    }

    IEnumerator Co_InstantiateYokai()
    {
        yield return new WaitForSeconds(2);

        yokai = Instantiate(yokais[ind].yokaiPrefab, portal.centerTransform.position, Quaternion.identity);
        yokai.transform.LookAt(new Vector3(Camera.main.transform.position.x, yokai.transform.position.y, Camera.main.transform.position.z));
        yokai.DisableAllPanels();
        Vector3 startPosition = portal.centerTransform.position;
        Vector3 endPosition = Camera.main.transform.position + Camera.main.transform.forward * 1f;

        while (timer < moveTime)
        {
            yokai.transform.position = Vector3.Lerp(startPosition, endPosition, timer / moveTime);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;
        timer = 0;
    }

    public void DestroyCurrentYokai()
    {
        if (yokai != null)
        {
            Destroy(yokai.gameObject);
            ind++;
        }
    }

    public QuestionSO GetCurrentQuestion()
    {
        return yokais[ind].question;
    }

    public float ActivateYokaiState(YokaiState yokaiState)
    {
        if (yokaiState == YokaiState.Onboarding)
        {
            Invoke("DelayedActivateOnboarding", 4);
            return 8;
        }
        else if (ind > 0 && yokaiState == YokaiState.Order)
        {
            Invoke("DelayedActivateOrder", 4);
            return 6;
        }
        else
        {
            float timer = yokai.ActivateState(yokaiState);
            if (debugMode)
            {
                timer = 5;
            }
            return timer;
        }
    }

    private void DelayedActivateOnboarding()
    {
        yokai.ActivateState(YokaiState.Onboarding);
    }

    private void DelayedActivateOrder()
    {
        yokai.ActivateState(YokaiState.Order);
    }

    public void AddOnFoodReceivedEvt(UnityEvent evt)
    {
        yokai.EvtOnFoodReceived.AddListener(evt.Invoke);
    }

    public void RemoveOnFoodReceivedEvt(UnityEvent evt)
    {
        yokai.EvtOnFoodReceived.RemoveListener(evt.Invoke);
    }
}
