using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctionality : MonoBehaviour
{
    public static MenuFunctionality Instance;

    public void Awake()
    {
        if(Instance != null && Instance != this) {
            Destroy(Instance);
            Debug.Log("Destroyed MenuFunctionality Instance on: " + Instance.gameObject.ToString() +
                      ", there should only be ONE MenuFunctionality in a scene!");
        }
        Instance = this;
    }

    public void LoadAmestecuriScene()
    {
        SceneManager.LoadScene("AmestecScene", LoadSceneMode.Single);
    }
    
    public void LoadMetaleScene()
    {
        SceneManager.LoadScene("MetaleScene", LoadSceneMode.Single);
    }

    public void LogoutAndLoadLoginScene()
    {
        if (RealmController.SyncUser != null) {
            RealmController.LogOutBackend();

            SceneManager.LoadScene("LoginScene", LoadSceneMode.Single);
        }
    }
}

