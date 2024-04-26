using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlacementView : BaseView
{
    public UnityEvent EvtPlacementCompletePressed;
    public UnityEvent EvtPlacementResetPressed;
    public AudioSource successAudio;

    // public void ButtonEvt_PlacementComplete()
    // {
    //     EvtPlacementCompletePressed.Invoke();
    // }

    public void ButtonEvt_PlacementReset()
    {
        EvtPlacementResetPressed.Invoke();
    }

    public void PlaySuccessAudio()
    {
        Debug.Log("?????????"); // TODO: for some reason audio isn't played and idk why
        successAudio.Play();
    }
}
