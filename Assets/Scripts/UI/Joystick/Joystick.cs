using System;
using UnityEngine;

public class Joystick : MonoBehaviour  {
    
    [SerializeField]
    private GameObject joystickHandle;
    
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

        joystickHandle.transform.position = touchScreenPosition;
        joystickBackground.transform.position = touchScreenPosition;
        
        joystickHandle.SetActive(true);
        joystickBackground.SetActive(true);
    }

    private void HideJoystick() {
        joystickHandle.SetActive(false);
        joystickBackground.SetActive(false);
    }
}
