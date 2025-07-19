using System;
using TMPro;
using UnityEngine;

namespace UI {
    public class ShowFps : MonoBehaviour {
        
        [SerializeField]
        private TextMeshProUGUI fpsText;

        private float deltaTime = 0.0f;

        private void Update() {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            fpsText.text = Mathf.Ceil(fps).ToString();
        }
    }
}