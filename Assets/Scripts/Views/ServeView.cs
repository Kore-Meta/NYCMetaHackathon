using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ServeView : BaseView
{
    public UnityEvent EvtServePressed;

    // public void ButtonEvt_Serve()
    // {
    //     EvtServePressed.Invoke();
    // }

    public void DebugServe()
    {
        GameObject dish = GameObject.FindWithTag("Dish");
        dish.GetComponent<Rigidbody>().MovePosition(FindFirstObjectByType<Yokai>().transform.position);
    }
}
