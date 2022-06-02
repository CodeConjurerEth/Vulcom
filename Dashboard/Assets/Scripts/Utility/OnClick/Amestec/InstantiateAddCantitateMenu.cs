
using UnityEngine;

public class InstantiateAddCantitateMenu : MonoBehaviour
{
    [SerializeField]private GameObject AddCantitateMenuPrefab;
    [SerializeField]private Transform ContentPanel;
    
    public void OnClickOpen()
    {
        Instantiate(AddCantitateMenuPrefab, ContentPanel);
    }
}
