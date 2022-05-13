using UnityEngine;

public class OpenRemoveCantitateMenu : MonoBehaviour
{
    public GameObject RemoveCantitateMenuPrefab;
    public Transform ContentPanel;

    public void OnClickOpen()
    {
        Instantiate(RemoveCantitateMenuPrefab, ContentPanel);
    }

    public void DestroyRemoveMenu()
    {
        DestroyImmediate(RemoveCantitateMenuPrefab);
    }
}