
using UnityEngine;

public class InstantiateAddCantitateMenu : MonoBehaviour
{
    public GameObject AddCantitateMenuPrefab;
    public Transform ContentPanel;
    
    public void OnClickOpen()
    {
        Instantiate(AddCantitateMenuPrefab, ContentPanel);
    }
}
