using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yokai : MonoBehaviour
{
    [SerializeField] private GameObject onboardingPanel;
    [SerializeField] private GameObject orderPanel;
    [SerializeField] private GameObject servePanel;
    [SerializeField] private GameObject goodbyePanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableAllPanels()
    {
        onboardingPanel.SetActive(false);
        orderPanel.SetActive(false);
        servePanel.SetActive(false);
        goodbyePanel.SetActive(false);
    }

    public void ShowOnboardingPanel()
    {
        DisableAllPanels();
        onboardingPanel.SetActive(true);
    }

    public void ShowOrderPanel()
    {
        DisableAllPanels();
        orderPanel.SetActive(true);
    }

    public void ShowServePanel()
    {
        DisableAllPanels();
        servePanel.SetActive(true);
    }

    public void ShowGoodbyePanel()
    {
        DisableAllPanels();
        goodbyePanel.SetActive(true);
    }
}
