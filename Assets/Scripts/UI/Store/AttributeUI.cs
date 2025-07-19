using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Store {
    public class AttributeUI : MonoBehaviour {

        [SerializeField] private TextMeshProUGUI valueText;

        [SerializeField] private Button minusButton;

        [SerializeField] private Button plusButton;

        public int NewValue { get; private set; }

        private int _previousValue;

        private void Awake() {
            valueText.gameObject.SetActive(true);
            minusButton.gameObject.SetActive(true);
            plusButton.gameObject.SetActive(true);

            minusButton.onClick.AddListener(OnMinusButtonClick);
            plusButton.onClick.AddListener(OnPlusButtonClick);
        }

        public void Init(int value) {
            _previousValue = value;
            NewValue = value;
            
            valueText.text = value.ToString();
        }

        private void OnMinusButtonClick() {
            NewValue -= 1;
            valueText.text = NewValue.ToString();
        }

        private void OnPlusButtonClick() {
            NewValue += 1;
            valueText.text = NewValue.ToString();
        }
        
        public void OnConfirmButtonClick() {
            _previousValue = NewValue;
        }
        
        public void OnCancelButtonClick() {
            NewValue = _previousValue;
        }
    }
}