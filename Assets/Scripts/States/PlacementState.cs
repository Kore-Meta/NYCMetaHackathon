using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementState : BaseState
{
    public override void EnterState()
    {
        base.EnterState();

        GameStateMachine.Instance.ConveyorBeltBuilder.startBuilding = true;
        GameStateMachine.Instance.ConveyorBeltBuilder.Evt_OnBeltBuilt.AddListener(GameStateMachine.Instance.CookingManager.InstantiateChefStation);
        ViewController.Instance.PlacementView.ShowView();
        ViewController.Instance.PlacementView.EvtPlacementCompletePressed.AddListener(OnPlacementComplete);
        ViewController.Instance.PlacementView.EvtPlacementResetPressed.AddListener(OnPlacementReset);
    }

    public override void ExitState()
    {
        base.ExitState();
        ViewController.Instance.PlacementView.EvtPlacementCompletePressed.RemoveListener(OnPlacementComplete);
        ViewController.Instance.PlacementView.EvtPlacementResetPressed.RemoveListener(OnPlacementReset);
        ViewController.Instance.PlacementView.HideView();
    }

    private void OnPlacementComplete()
    {
        if (!GameStateMachine.Instance.ConveyorBeltBuilder.isBeltBuilt)
        {
            return;
        }
        GameStateMachine.Instance.CookingManager.DisableChefStationGrabbable();
        GameStateMachine.Instance.AdvanceState();
    }

    private void OnPlacementReset()
    {
        GameStateMachine.Instance.ConveyorBeltBuilder.Reset();
    }
}
