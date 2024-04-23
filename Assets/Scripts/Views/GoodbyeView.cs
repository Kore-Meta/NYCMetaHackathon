using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoodbyeView : BaseView
{
    public UnityEvent EvtNextYokaiPressed;

    public void ButtonEvt_NextYokai()
    {
        EvtNextYokaiPressed.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}