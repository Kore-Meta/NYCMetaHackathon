using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
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

    private BaseView _currentView;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
