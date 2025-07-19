using System;
using Player;
using TMPro;
using UI.Store;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI {
    public class StoreUI : MonoBehaviour {

        [Header("Store Panel Buttons")]
        [SerializeField] 
        private Button closeStoreButton;
        
        [SerializeField] 
        private Button cancelButton;
        
        [SerializeField]
        private Button saveButton;

        [SerializeField] 
        private TextMeshProUGUI totalCostTMP;
        
        [Header("Attributes")]
        [SerializeField] 
        private AttributeUI carryCapacityAttribute;

        [SerializeField]
        private AttributeUI pointsAttribute;
        
        [SerializeField]
        private ColorAttributeUI colorAttribute;
        
        [Header("Player Controllers")]
        [SerializeField]
        private PlayerCarryController carryController;

        [SerializeField] 
        private PlayerPointsController pointsController;
        
        [SerializeField]
        private Material underwearMaterial;

        private void Awake() {
            closeStoreButton.gameObject.SetActive(true);
            cancelButton.gameObject.SetActive(true);
            saveButton.gameObject.SetActive(true);
            totalCostTMP.gameObject.SetActive(true);
            carryCapacityAttribute.gameObject.SetActive(true);
            pointsAttribute.gameObject.SetActive(true);
            colorAttribute.gameObject.SetActive(true);
        }

        private void OnEnable() {
            totalCostTMP.text = "$0";
            carryCapacityAttribute.Init(carryController.CarryCapacity, totalCostTMP);
            pointsAttribute.Init(pointsController.EnemyPointsWorth, totalCostTMP);
            colorAttribute.Init(underwearMaterial.color, totalCostTMP);
        }

        public void OnCloseStoreButtonClick(UnityAction action) {
            if (closeStoreButton != null) {
                closeStoreButton.onClick.RemoveAllListeners();
                closeStoreButton.onClick.AddListener(action);
            } else {
                Debug.LogError("[StoreUI] Close store button is not assigned.");
            }
        }

        public void OnCancelButtonClick(UnityAction action) {
            if (cancelButton != null) {
                cancelButton.onClick.RemoveAllListeners();
                cancelButton.onClick.AddListener(() => {
                    CancelImprovements();
                    action.Invoke();
                });
            } else {
                Debug.LogError("[StoreUI] Cancel button is not assigned.");
            }
        }

        private void CancelImprovements() {
            carryCapacityAttribute.OnCancelButtonClick();
            pointsAttribute.OnCancelButtonClick();
            colorAttribute.OnCancelButtonClick();
        }
        
        public void OnSaveButtonClick(UnityAction action) {
            if (saveButton != null) {
                saveButton.onClick.RemoveAllListeners();
                saveButton.onClick.AddListener(() => {
                    if (ConfirmImprovements()) {
                        action.Invoke();
                    }
                });
            } else {
                Debug.LogError("[StoreUI] Save button is not assigned.");
            }
        }
        
        private bool ConfirmImprovements() {
            int totalCost = int.Parse(totalCostTMP.text[1..]);
            if (totalCost > pointsController.Points) {
                Debug.LogWarning("[StoreUI] Not enough points to save changes.");
                return false;
            }
            
            int newCarryCapacity = carryCapacityAttribute.NewValue;
            int newPoints = pointsAttribute.NewValue;
            Color newColor = colorAttribute.NewColor;
            
            carryController.ChangeCarryCapacity(newCarryCapacity);
            pointsController.ChangePointsWorth(newPoints);
            underwearMaterial.color = newColor;
            
            pointsController.SubtractPoints(totalCost);
            
            return true;
        }
    }
}