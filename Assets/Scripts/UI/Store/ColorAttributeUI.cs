using UnityEngine;
using UnityEngine.UI;

namespace UI.Store {
    public class ColorAttributeUI : MonoBehaviour {

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
        
        private void Awake() {
            redColorButton.onClick.AddListener(() => OnColorButtonClick(redColorButton.GetComponent<Image>().color));
            yellowColorButton.onClick.AddListener(() => OnColorButtonClick(yellowColorButton.GetComponent<Image>().color));
            greenColorButton.onClick.AddListener(() => OnColorButtonClick(greenColorButton.GetComponent<Image>().color));
            lightBlueButton.onClick.AddListener(() => OnColorButtonClick(lightBlueButton.GetComponent<Image>().color));
            blueColorButton.onClick.AddListener(() => OnColorButtonClick(blueColorButton.GetComponent<Image>().color));
            purpleColorButton.onClick.AddListener(() => OnColorButtonClick(purpleColorButton.GetComponent<Image>().color));
            pinkColorButton.onClick.AddListener(() => OnColorButtonClick(pinkColorButton.GetComponent<Image>().color));
        }
        
        public void Init(Color color) {
            _previousColor = color;
            NewColor = color;
        }
        
        private void OnColorButtonClick(Color color) {
            NewColor = color;
            previewUnderwearMaterial.color = NewColor;
        }
        
        public void OnCancelButtonClick() {
            NewColor = _previousColor;
            previewUnderwearMaterial.color = NewColor; // Reset the color to the previous value
        }
    }
}