using System;
using Player;
using TMPro;
using UnityEngine;

namespace UI {
    public class PointsUI : MonoBehaviour {
        
        [SerializeField]
        private TextMeshProUGUI pointsText;

        private void Awake() {
            pointsText.gameObject.SetActive(true);
            pointsText.text = "0";
        }

        private void Start() {
            PlayerPointsController.OnPointsChanged.AddListener(UpdatePointsDisplay);
        }
        
        private void UpdatePointsDisplay(int points) {
            pointsText.text = $"{points}";
        }
    }
}
