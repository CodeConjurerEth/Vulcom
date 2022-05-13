
using UnityEngine;

public class OpenAddCantitateMenu : MonoBehaviour
{
    public GameObject AddCantitateMenuPrefab;
    public Transform ContentPanel;
    
    public void OnClickOpen()
    {
        Instantiate(AddCantitateMenuPrefab, ContentPanel);
    }

    public void DestroyAddMenu()
    {
        DestroyImmediate(AddCantitateMenuPrefab);
    }
}
