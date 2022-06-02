using UnityEngine;

public class InstantiatePrefabToTargetParent : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform targetParent;

    public void Instantiate()
    {
        Instantiate(prefab, targetParent);
    }
}