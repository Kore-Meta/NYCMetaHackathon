using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : BaseState
{
    public override void EnterState()
    {
        base.EnterState();
        ViewController.Instance.MainMenuView.ShowView();
        ViewController.Instance.MainMenuView.EvtStartGamePressed.AddListener(OnStartGame);
    }

    public override void ExitState()
    {
        base.ExitState();
        ViewController.Instance.MainMenuView.EvtStartGamePressed.RemoveListener(OnStartGame);
        ViewController.Instance.MainMenuView.HideView();
    }

    private void OnStartGame()
    {
        GameStateMachine.Instance.AdvanceState();
    }
}
