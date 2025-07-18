using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI {
    public class StoreUI : MonoBehaviour {

        [SerializeField] 
        private Button closeStoreButton;
        
        public void OnCloseStoreButtonClick(UnityAction action) {
            if (closeStoreButton != null) {
                closeStoreButton.onClick.RemoveAllListeners();
                closeStoreButton.onClick.AddListener(action);
            } else {
                Debug.LogError("[StoreUI] Close store button is not assigned.");
            }
        }

    }
}