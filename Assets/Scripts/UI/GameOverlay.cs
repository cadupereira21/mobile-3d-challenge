using System;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class GameOverlay : MonoBehaviour{

        [SerializeField] private Button storeButton;

        private void Awake() {
            storeButton.onClick.AddListener(OnStoreButtonClick);
            storeButton.gameObject.SetActive(false);
        }

        private void Start() {
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
            Debug.Log("[GameOverlay] Opening store...");
            // Logic to open the store UI
        }
    }
}