using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    
    public GameObject AuthenticationPanel;
    public GameObject LoginPanel;
    public GameObject RegisterPanel;
    public GameObject InventarPanel;
    

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

        InventarPanel.SetActive(false);
        AuthenticationPanel.SetActive(true);
    }

    public void SwitchBetweenLoginRegisterPanels()
    {
        if (!LoginPanel.activeSelf && !RegisterPanel.activeSelf) {
            throw new Exception("Login Panel and Register Panel are disabled, cannot switch between them!");
        }
        else {
            if (LoginPanel.activeSelf) {
                //Switch to Register UI
                LoginPanel.SetActive(false);
                RegisterPanel.SetActive(true);
            }
            else if (RegisterPanel.activeSelf) {
                //Switch to Login UI
                RegisterPanel.SetActive(false);
                LoginPanel.SetActive(true);
            }
        }
    }
    
    public void RunLogout()
    {
        RealmController.LogOutBackend();
    }
}
