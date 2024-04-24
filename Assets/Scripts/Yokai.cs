using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Yokai : MonoBehaviour
{
    [SerializeField] private GameObject onboardingPanel = null;
    [SerializeField] private GameObject orderPanel = null;
    [SerializeField] private GameObject servePanel = null;
    [SerializeField] private GameObject goodbyePanel = null;

    [SerializeField] private AudioClip onboardingAudio = null;
    [SerializeField] private AudioClip orderAudio = null;
    [SerializeField] private AudioClip serveAudio = null;
    [SerializeField] private AudioClip goodbyeAudio = null;

    [SerializeField] private AudioSource audioSource;

    public UnityEvent EvtOnFoodReceived;

    // Start is called before the first frame update
    void Start()
    {
        //DisableAllPanels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Dish")
        {
            EvtOnFoodReceived.Invoke();
        }
    }

    public float ActivateState(YokaiState yokaiState)
    {
        switch (yokaiState)
        {
            case YokaiState.Onboarding:
                return ShowOnboardingPanel();
            case YokaiState.Order:
                return ShowOrderPanel();
            case YokaiState.WaitToBeServed:
                return ShowServePanel();
            case YokaiState.Goodbye:
                return ShowGoodbyePanel();
            default:
                return 0;
        }
    }

    public void DisableAllPanels()
    {
        if (onboardingPanel != null)
            onboardingPanel.SetActive(false);
        if (orderPanel != null)
            orderPanel.SetActive(false);
        if (servePanel != null)
            servePanel.SetActive(false);
        if (goodbyePanel != null)
            goodbyePanel.SetActive(false);
    }

    public float ShowOnboardingPanel()
    {
        DisableAllPanels();
        if (onboardingPanel != null)
            onboardingPanel.SetActive(true);
        if (onboardingAudio != null)
        {
            audioSource.clip = onboardingAudio;
            audioSource.Play();
        }
        return onboardingAudio ? onboardingAudio.length : 0;
    }

    public float ShowOrderPanel()
    {
        DisableAllPanels();
        if (orderPanel != null)
            orderPanel.SetActive(true);
        if (orderAudio != null)
        {
            audioSource.clip = orderAudio;
            audioSource.Play();
        }
        return orderAudio ? orderAudio.length : 0;
    }

    public float ShowServePanel()
    {
        DisableAllPanels();
        if (servePanel != null)
            servePanel.SetActive(true);
        if (serveAudio != null)
        {
            audioSource.clip = serveAudio;
            audioSource.Play();
        }
        return serveAudio ? serveAudio.length : 0;
    }

    public float ShowGoodbyePanel()
    {
        DisableAllPanels();
        if (goodbyePanel != null)
            goodbyePanel.SetActive(true);
        if (goodbyeAudio != null)
        {
            audioSource.clip = goodbyeAudio;
            audioSource.Play();
        }
        return goodbyeAudio ? goodbyeAudio.length : 0;
    }
}
