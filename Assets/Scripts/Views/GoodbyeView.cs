using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoodbyeView : BaseView
{
    public UnityEvent EvtNextYokaiPressed;
    public UnityEvent EvtBackToMainMenuPressed;

    public void ButtonEvt_NextYokai()
    {
        EvtNextYokaiPressed.Invoke();
    }

    public void ButtonEvt_BackToMainMenu()
    {
        EvtBackToMainMenuPressed.Invoke();
    }
}
