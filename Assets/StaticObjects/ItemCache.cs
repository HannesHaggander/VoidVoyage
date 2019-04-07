using Resources.Ships;
using UnityEngine;

namespace StaticObjects
{
    public class ItemCache : MonoBehaviour
    {
        public Camera MainCamera { get; private set; }
        public Transform Player { get; private set; }
        public static ItemCache Instance { get; private set; }
        
        protected void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        public void SetMainCamera(Camera mainCamera)
        {
            MainCamera = mainCamera;
        }

        public void SetPlayerEntity(IEntity player)
        {
            Player = player.GetTransform();
        }
    }
}
