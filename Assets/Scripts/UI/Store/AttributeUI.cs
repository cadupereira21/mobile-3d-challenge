using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Store {
    public class AttributeUI : MonoBehaviour {

        [SerializeField] private TextMeshProUGUI attributeCostText;

        [SerializeField] private int attributeCost = 1;

        [SerializeField] private TextMeshProUGUI valueText;

        [SerializeField] private Button minusButton;

        [SerializeField] private Button plusButton;
        
        private TextMeshProUGUI _totalCostText;

        public int NewValue { get; private set; }

        private int _previousValue;

        private void Awake() {
            valueText.gameObject.SetActive(true);
            minusButton.gameObject.SetActive(true);
            plusButton.gameObject.SetActive(true);
            attributeCostText.gameObject.SetActive(true);
            
            minusButton.onClick.AddListener(OnMinusButtonClick);
            plusButton.onClick.AddListener(OnPlusButtonClick);
        }

        private void Start() {
            attributeCostText.text = $"${attributeCost}";
        }

        public void Init(int value, TextMeshProUGUI costText) {
            _previousValue = value;
            NewValue = value;
            
            valueText.text = value.ToString();
            _totalCostText = costText;
        }

        private void OnMinusButtonClick() {
            if (NewValue-1 < _previousValue) return;
            
            NewValue -= 1;
            
            valueText.text = NewValue.ToString();

            int cost = int.Parse(_totalCostText.text[1..]) - attributeCost;
            _totalCostText.text = $"${cost}";
        }

        private void OnPlusButtonClick() {
            NewValue += 1;
            
            valueText.text = NewValue.ToString();

            int cost = int.Parse(_totalCostText.text[1..]) + attributeCost;
            _totalCostText.text = $"${cost}";
        }
        
        public void OnCancelButtonClick() {
            NewValue = _previousValue;
        }
    }
}