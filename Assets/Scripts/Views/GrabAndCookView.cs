using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrabAndCookView : BaseView
{
    public UnityEvent EvtCookCompletePressed;

    public void ButtonEvt_CookComplete()
    {
        EvtCookCompletePressed.Invoke();
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