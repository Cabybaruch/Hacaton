using Game.scripts;
using Game.Scripts.UI;
using UnityEditor;
using UnityEngine;


namespace Game.Managers
{
    public class InteractionManager : MonoBehaviour
    {
        [SerializeField] private PopUpView _popUpView;
        
        private ArtifactItem  _currentItem;
        public static InteractionManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }
            Destroy(gameObject);
            
        }
        

        public void InteractWithItem(ArtifactItem  item)
        {
            
        }
    }
    
}