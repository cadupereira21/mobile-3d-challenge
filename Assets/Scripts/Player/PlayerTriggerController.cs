using System.Collections;
using UnityEngine;

namespace Player {
    public class PlayerTriggerController : MonoBehaviour {
        
        private static readonly int Punch = Animator.StringToHash("punch");
        
        [SerializeField]
        [Range(0f, 5f)]
        private float punchWaitTime = 1.0f;
        
        private PlayerMovementController _playerMovementController;

        private Animator _playerAnimator;

        private bool _isPunching = false;

        private void Awake() {
            _playerMovementController = this.GetComponent<PlayerMovementController>();
            _playerAnimator = this.GetComponent<Animator>();
        }

        private void OnControllerColliderHit(ControllerColliderHit hit) {
            if (hit.gameObject.CompareTag("Enemy")) {
                if (_isPunching) return; // Prevent multiple punches at the same time
                
                _isPunching = true;
                this.StopAllCoroutines();
                Debug.Log($"[PlayerTriggerController] Player has entered the trigger of {hit.gameObject.name}.");
                _playerMovementController.canMove = false;
                _playerAnimator.SetTrigger(Punch);
                this.StartCoroutine(WaitForAnimationEnd(punchWaitTime)); // Adjust the wait time based on your animation length
            }
        }
        
        private IEnumerator WaitForAnimationEnd(float waitTime) {
            yield return new WaitForSeconds(waitTime);
            _playerMovementController.canMove = true;
            _isPunching = false;
        }
    }
}