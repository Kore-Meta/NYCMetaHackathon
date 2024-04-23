using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlacementView : BaseView
{
    public UnityEvent EvtPlacementCompletePressed;
    public UnityEvent EvtPlacementResetPressed;

    public void ButtonEvt_PlacementComplete()
    {
        EvtPlacementCompletePressed.Invoke();
    }

    public void ButtonEvt_PlacementReset()
    {
        EvtPlacementResetPressed.Invoke();
    }
}