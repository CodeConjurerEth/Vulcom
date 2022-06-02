using UnityEngine;

public class InstantiateRemoveCantitateMenu : MonoBehaviour
{
    public GameObject RemoveCantitateMenuPrefab;
    public Transform ContentPanel;

    public void OnClickOpen()
    {
        Instantiate(RemoveCantitateMenuPrefab, ContentPanel);
    }
}