using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OrderView : BaseView
{
    public UnityEvent EvtPlaceOrderPressed;

    public void ButtonEvt_PlaceOrder()
    {
        EvtPlaceOrderPressed.Invoke();
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