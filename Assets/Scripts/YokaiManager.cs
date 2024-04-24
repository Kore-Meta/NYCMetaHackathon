using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class YokaiManager : MonoBehaviour
{
    [SerializeField] private bool debugMode = true;

    public YokaiSO[] yokais;

    private int ind;
    private Vector3 instantiatePos;
    private Yokai yokai;

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
        instantiatePos = Camera.main.transform.position + new Vector3(0, 0, 0.8f);
        yokai = Instantiate(yokais[ind].yokaiPrefab, instantiatePos, Quaternion.identity);
        return true;
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
        float time = yokai.ActivateState(yokaiState);
        if (debugMode)
        {
            time = 5;
        }
        //yokai.Invoke("DisableAllPanels", time);
        return time;
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
