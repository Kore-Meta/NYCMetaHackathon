using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    public static GameStateMachine Instance { get; private set; }

    [SerializeField] private YokaiManager yokaiManager;
    [SerializeField] private ConveyorBeltBuilder conveyorBeltBuilder;
    [SerializeField] private LetterBallHandler letterBallHandler;
    [SerializeField] private CookingManager cookingManager;

    public YokaiManager YokaiManager => yokaiManager;
    public ConveyorBeltBuilder ConveyorBeltBuilder => conveyorBeltBuilder;
    public LetterBallHandler LetterBallHandler => letterBallHandler;
    public CookingManager CookingManager => cookingManager;

    private BaseState _currentState;

    public bool isTutorialMode = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(new MainMenuState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(BaseState newState)
    {
        _currentState?.ExitState();
        _currentState = newState;
        _currentState.EnterState();
    }
}
