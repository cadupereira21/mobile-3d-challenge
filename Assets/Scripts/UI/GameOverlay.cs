using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class GameOverlay : MonoBehaviour {

        [SerializeField] private GameObject moneyOverlay;

        [SerializeField] private Joystick.Joystick joystickOverlay;

        [SerializeField] private Button storeButton;
        
        [SerializeField] private StoreUI storePanel;

        private void Awake() {
            storeButton.onClick.AddListener(OnStoreButtonClick);
            storeButton.gameObject.SetActive(false);
            
            moneyOverlay.SetActive(true);
            joystickOverlay.gameObject.SetActive(true);
        }

        private void Start() {
            storePanel.OnCloseStoreButtonClick(CloseStore);
            
            PlayerTriggerController.OnStoreAreaEntered.AddListener(ShowStoreButton);
            PlayerTriggerController.OnStoreAreaExited.AddListener(HideStoreButton);
        }

        private void OnDestroy() {
            PlayerTriggerController.OnStoreAreaEntered.RemoveListener(ShowStoreButton);
            PlayerTriggerController.OnStoreAreaExited.RemoveListener(HideStoreButton);
        }
        
        private void ShowStoreButton() {
            storeButton.gameObject.SetActive(true);
        }
        
        private void HideStoreButton() {
            storeButton.gameObject.SetActive(false);
        }

        private void OnStoreButtonClick() {
            OpenStore();
        }
        
        private void OpenStore() {
            storePanel.gameObject.SetActive(true);
            
            moneyOverlay.SetActive(false);
            joystickOverlay.HideJoystick();
            joystickOverlay.gameObject.SetActive(false);
            storeButton.gameObject.SetActive(false);
        }

        private void CloseStore() {
            storePanel.gameObject.SetActive(false);
            
            moneyOverlay.SetActive(true);
            joystickOverlay.gameObject.SetActive(true);
            joystickOverlay.HideJoystick();
            storeButton.gameObject.SetActive(true);
        }
    }
}