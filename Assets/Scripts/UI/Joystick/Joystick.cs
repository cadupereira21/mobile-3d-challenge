using UnityEngine;
using UnityEngine.UI;

namespace UI.Joystick {
    public class Joystick : MonoBehaviour  {
    
        [SerializeField]
        private Image joystickHandle;
    
        [SerializeField]
        private GameObject joystickBackground;

        private void Awake() {
            HideJoystick();
        }

        // Update is called once per frame
        private void Update() {
            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began) {
                    ShowJoystick(touch);
                } else if (touch.phase is TouchPhase.Canceled or TouchPhase.Ended) {
                    HideJoystick();
                }
            }
        }
    
        private void ShowJoystick(Touch touch) {
            Vector2 touchScreenPosition = touch.position;

            joystickBackground.transform.position = touchScreenPosition;
        
            SetImageOpacity(joystickHandle, 0.8f);
            joystickBackground.SetActive(true);
        }

        public void HideJoystick() {
            SetImageOpacity(joystickHandle, 0f);
            joystickBackground.SetActive(false);
        }

        private void SetImageOpacity(Image image, float opacity) {
            Color color = image.color;
            color.a = opacity;
            image.color = color;
        }
    }
}
