using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    
    [SerializeField] private GameObject authenticationPanel;
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject registerPanel;
    [SerializeField] private GameObject inventarPanel;
    [SerializeField] private GameObject stocSelecterPanel;
    [SerializeField] private GameObject amestecPanel;
    [SerializeField] private GameObject metalPanel;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else {
            //if Instance already exists, destroy old one and assign new one to Instance
            Destroy(Instance);
            Instance = this;
            Debug.Log("Too many UIControllers, removing old one and instantiating on " + gameObject);
        }

        inventarPanel.SetActive(false);
        StocSelecterOn();
        authenticationPanel.SetActive(true);
    }

    public void SwitchBetweenLoginRegisterPanels()
    {
        if (!loginPanel.activeSelf && !registerPanel.activeSelf) {
            throw new Exception("Login Panel and Register Panel are disabled, cannot switch between them!");
        }
        else {
            if (loginPanel.activeSelf) {
                //Switch to Register UI
                loginPanel.SetActive(false);
                registerPanel.SetActive(true);
            }
            else if (registerPanel.activeSelf) {
                //Switch to Login UI
                registerPanel.SetActive(false);
                loginPanel.SetActive(true);
            }
        }
    }
    
    public void HideAuthenticationUIOnLogin()
    {   
        if (RealmController.SyncUser != null) {
            registerPanel.SetActive(false);
            loginPanel.SetActive(true);
            authenticationPanel.SetActive(false);
        }
        //else show wrong input message/ no existing account
    }

    public void ShowInventarPanel()
    {
        inventarPanel.SetActive(true);
    }

    public void AmestecPanelOn()
    {
        stocSelecterPanel.SetActive(false);
        metalPanel.SetActive(false);
        amestecPanel.SetActive(true);
    }

    public void MetalPanelOn()
    {
        stocSelecterPanel.SetActive(false);
        amestecPanel.SetActive(false);
        metalPanel.SetActive(true);
    }

    public void StocSelecterOn()
    {
        DisableStocPanels();
        stocSelecterPanel.SetActive(true);
    }

    private void DisableStocPanels()
    {
        amestecPanel.SetActive(false);
        metalPanel.SetActive(false);
    }
}
