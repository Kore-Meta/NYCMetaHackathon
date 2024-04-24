using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    public static GameStateMachine Instance { get; private set; }

    [SerializeField] private HandTrackingManager handTrackingManager;
    [SerializeField] private YokaiManager yokaiManager;
    [SerializeField] private ConveyorBeltBuilder conveyorBeltBuilder;
    [SerializeField] private LetterBallHandler letterBallHandler;
    [SerializeField] private CookingManager cookingManager;

    public HandTrackingManager HandTrackingManager => handTrackingManager;
    public YokaiManager YokaiManager => yokaiManager;
    public ConveyorBeltBuilder ConveyorBeltBuilder => conveyorBeltBuilder;
    public LetterBallHandler LetterBallHandler => letterBallHandler;
    public CookingManager CookingManager => cookingManager;

    private BaseState _currentState;
    private GameStateName _currentStateName;

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
        _currentStateName = GameStateName.MainMenu;
        ChangeState(new MainMenuState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToOrderState()
    {
        _currentState?.ExitState();
        _currentState = new OrderState();
        _currentState.EnterState();
        _currentStateName = GameStateName.Order;
    }

    private void ChangeState(BaseState newState)
    {
        _currentState?.ExitState();
        _currentState = newState;
        _currentState.EnterState();
    }

    public void AdvanceState()
    {
        switch (_currentStateName)
        {
            case GameStateName.MainMenu:
                ChangeState(new OnboardingState());
                _currentStateName = GameStateName.Onboarding;
                Debug.Log(_currentStateName);
                break;

            case GameStateName.Onboarding:
                ChangeState(new PlacementState());
                _currentStateName = GameStateName.Placement;
                Debug.Log(_currentStateName);
                break;

            case GameStateName.Placement:
                ChangeState(new OrderState());
                _currentStateName = GameStateName.Order;
                Debug.Log(_currentStateName);
                break;

            case GameStateName.Order:
                ChangeState(new GrabAndCookState());
                _currentStateName = GameStateName.GrabAndCook;
                Debug.Log(_currentStateName);
                break;

            case GameStateName.GrabAndCook:
                ChangeState(new ServeState());
                _currentStateName = GameStateName.Serve;
                Debug.Log(_currentStateName);
                break;

            case GameStateName.Serve:
                ChangeState(new GoodbyeState());
                _currentStateName = GameStateName.Goodbye;
                Debug.Log(_currentStateName);
                break;

            case GameStateName.Goodbye:
                Debug.Log(_currentStateName);
                break;

            default:
                break;
        }
    }
}
