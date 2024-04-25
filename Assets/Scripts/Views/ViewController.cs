using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    private GameStateName _currentState;

    public static ViewController Instance { get; private set; }

    [SerializeField] private MainMenuView mainMenuView;
    [SerializeField] private OnboardingView onboardingView;
    [SerializeField] private PlacementView placementView;
    [SerializeField] private OrderView orderView;
    [SerializeField] private GrabAndCookView grabAndCookView;
    [SerializeField] private ServeView serveView;
    [SerializeField] private GoodbyeView goodbyeView;

    public MainMenuView MainMenuView => mainMenuView;
    public OnboardingView OnboardingView => onboardingView;
    public PlacementView PlacementView => placementView;
    public OrderView OrderView => orderView;
    public GrabAndCookView GrabAndCookView => grabAndCookView;
    public ServeView ServeView => serveView;
    public GoodbyeView GoodbyeView => goodbyeView;

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
        _currentState = GameStateName.MainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToOrderView()
    {
        if (_currentState == GameStateName.Goodbye)
        {
            goodbyeView.EvtNextYokaiPressed.Invoke();
            _currentState = GameStateName.Order;
        }
    }

    public void AdvanceView()
    {
        switch (_currentState)
        {
            case GameStateName.MainMenu:
                mainMenuView.EvtStartGamePressed.Invoke();
                _currentState = GameStateName.Onboarding;
                Debug.Log("hello" + _currentState);
                break;

            case GameStateName.Onboarding:
                onboardingView.EvtStartGamePressed.Invoke();
                _currentState = GameStateName.Placement;
                Debug.Log("hello" + _currentState);
                break;

            case GameStateName.Placement:
                placementView.EvtPlacementCompletePressed.Invoke();
                _currentState = GameStateName.Order;
                Debug.Log("hello" + _currentState);
                break;

            case GameStateName.Order:
                orderView.EvtPlaceOrderPressed.Invoke();
                _currentState = GameStateName.GrabAndCook;
                Debug.Log("hello" + _currentState);
                break;

            case GameStateName.GrabAndCook:
                grabAndCookView.EvtCookCompletePressed.Invoke();
                _currentState = GameStateName.Serve;
                Debug.Log("hello" + _currentState);
                break;

            case GameStateName.Serve:
                serveView.EvtServePressed.Invoke();
                _currentState = GameStateName.Goodbye;
                Debug.Log("hello" + _currentState);
                break;

            case GameStateName.Goodbye:
                break;

            default:
                break;
        }
    }
}
