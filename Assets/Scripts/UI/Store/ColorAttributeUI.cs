using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Store {
    public class ColorAttributeUI : MonoBehaviour {
        
        [SerializeField] private TextMeshProUGUI attributeCostText;

        [SerializeField] private int attributeCost = 1;

        [SerializeField] 
        private Material previewUnderwearMaterial;
        
        [Header("Colors")]
        [SerializeField] private Button redColorButton;
        
        [SerializeField]
        private Button yellowColorButton;
        
        [SerializeField]
        private Button greenColorButton;

        [SerializeField] private Button lightBlueButton;
        
        [SerializeField]
        private Button blueColorButton;
        
        [SerializeField]
        private Button purpleColorButton;
        
        [SerializeField]
        private Button pinkColorButton;
        
        private Color _previousColor;
        
        public Color NewColor { get; private set; }
        
        private TextMeshProUGUI _totalCostTMP;

        private bool _isCharged = false;
        
        private void Awake() {
            redColorButton.onClick.AddListener(() => OnColorButtonClick(redColorButton.GetComponent<Image>().color));
            yellowColorButton.onClick.AddListener(() => OnColorButtonClick(yellowColorButton.GetComponent<Image>().color));
            greenColorButton.onClick.AddListener(() => OnColorButtonClick(greenColorButton.GetComponent<Image>().color));
            lightBlueButton.onClick.AddListener(() => OnColorButtonClick(lightBlueButton.GetComponent<Image>().color));
            blueColorButton.onClick.AddListener(() => OnColorButtonClick(blueColorButton.GetComponent<Image>().color));
            purpleColorButton.onClick.AddListener(() => OnColorButtonClick(purpleColorButton.GetComponent<Image>().color));
            pinkColorButton.onClick.AddListener(() => OnColorButtonClick(pinkColorButton.GetComponent<Image>().color));
            
            attributeCostText.gameObject.SetActive(true);
            redColorButton.gameObject.SetActive(true);
            yellowColorButton.gameObject.SetActive(true);
            greenColorButton.gameObject.SetActive(true);
            lightBlueButton.gameObject.SetActive(true);
            blueColorButton.gameObject.SetActive(true);
            purpleColorButton.gameObject.SetActive(true);
            pinkColorButton.gameObject.SetActive(true);
        }

        private void Start() {
            attributeCostText.text = $"${attributeCost}";
        }

        public void Init(Color color, TextMeshProUGUI totalCostText) {
            _previousColor = color;
            NewColor = color;
            _totalCostTMP = totalCostText;
        }
        
        private void OnColorButtonClick(Color color) {
            if (color != _previousColor) {
                if (!_isCharged) {
                    int cost = int.Parse(_totalCostTMP.text[1..]) + attributeCost;
                    _totalCostTMP.text = $"${cost}";   
                    _isCharged = true;
                }
                
                NewColor = color;
                previewUnderwearMaterial.color = NewColor;
            } else {
                if (_isCharged) {
                    int cost = int.Parse(_totalCostTMP.text[1..]) - attributeCost;
                    _totalCostTMP.text = $"${cost}";
                    _isCharged = false;
                }
            }
        }
        
        public void OnCancelButtonClick() {
            NewColor = _previousColor;
            previewUnderwearMaterial.color = NewColor; // Reset the color to the previous value
        }
    }
}