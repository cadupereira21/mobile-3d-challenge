using System;
using Player;
using UI.Store;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI {
    public class StoreUI : MonoBehaviour {

        [SerializeField] 
        private Button closeStoreButton;

        [SerializeField] 
        private AttributeUI carryCapacityAttribute;

        [SerializeField]
        private AttributeUI pointsAttribute;
        
        [SerializeField]
        private ColorAttributeUI colorAttribute;

        [SerializeField] 
        private Button cancelButton;
        
        [SerializeField]
        private Button saveButton;
        
        [Header("Player Controllers")]
        [SerializeField]
        private PlayerCarryController carryController;

        [SerializeField] 
        private PlayerPointsController pointsController;
        
        [SerializeField]
        private Material underwearMaterial;
        
        private int _newCarryCapacity;

        private int _previousCarryCapacity;
        
        private int _newPoints;

        private int _previousPoints;
        
        private Color _newColor;

        private Color _previousColor;

        private void OnEnable() {
            carryCapacityAttribute.Init(carryController.CarryCapacity);
            pointsAttribute.Init(pointsController.EnemyPointsWorth);
            colorAttribute.Init(underwearMaterial.color);
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
                    ConfirmImprovements();
                    action.Invoke();
                });
            } else {
                Debug.LogError("[StoreUI] Save button is not assigned.");
            }
        }
        
        private void ConfirmImprovements() {
            int newCarryCapacity = carryCapacityAttribute.NewValue;
            int newPoints = pointsAttribute.NewValue;
            Color newColor = colorAttribute.NewColor;
            
            carryController.ChangeCarryCapacity(newCarryCapacity);
            pointsController.ChangePoints(newPoints);
            underwearMaterial.color = newColor;
        }
    }
}