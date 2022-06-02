
    using UnityEngine;

    public class DestroyTargetGameObj : MonoBehaviour
    {
        [SerializeField] private GameObject targetToDestroy;

        public void DestroyTarget()
        {
            Destroy(targetToDestroy);
        }
    }
