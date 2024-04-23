using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnboardingView : BaseView
{
    public UnityEvent EvtStartGamePressed;

    public void ButtonEvt_StartGame()
    {
        EvtStartGamePressed.Invoke();
    }

    public override void ShowView()
    {
        base.ShowView();
    }
}