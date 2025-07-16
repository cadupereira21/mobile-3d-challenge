using System.Collections;
using UnityEngine;

namespace Player {
    public class PlayerTriggerController : MonoBehaviour {
        
        private static readonly int Punch = Animator.StringToHash("punch");
        
        private static readonly int Carry = Animator.StringToHash("carry");
        
        [SerializeField]
        [Range(0f, 5f)]
        private float punchWaitTime = 1.0f;
        
        [SerializeField]
        [Range(0f, 5f)]
        private float carryWaitTime = 1.0f;
        
        [SerializeField]
        private PlayerCarryController playerCarryController;
        
        private PlayerMovementController _playerMovementController;

        private Animator _playerAnimator;

        private bool _isPunching = false;

        private bool _isCarrying = false;

        private void Awake() {
            _playerMovementController = this.GetComponentInParent<PlayerMovementController>();
            _playerAnimator = this.GetComponentInParent<Animator>();
        }

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Enemy")) {
                Debug.Log($"[PlayerTriggerController] Hit an enemy");
                Enemy.Enemy enemy = other.gameObject.GetComponentInParent<Enemy.Enemy>();
                if (enemy == null) {
                    Debug.LogWarning($"[PlayerTriggerController] No Enemy component found on {other.gameObject.name}");
                    return;
                }

                if (!enemy.IsKnockedDown) {
                    PunchEnemy(enemy);
                } else if (enemy.CanBeCarried) {
                    CarryEnemy(enemy);
                }
            }
        }

        private void PunchEnemy(Enemy.Enemy enemy) {
            Debug.Log($"[PlayerTriggerController] Punching enemy: {enemy.name}");
            if (_isPunching) return; // Prevent multiple punches at the same time
            _isPunching = true;
            this.StopAllCoroutines();
            _playerMovementController.canMove = false;
            _playerAnimator.SetTrigger(Punch);
            enemy.Faint();
            this.StartCoroutine(WaitForAnimationEnd(punchWaitTime, _isPunching));
        }

        private void CarryEnemy(Enemy.Enemy enemy) {
            Debug.Log($"[PlayerTriggerController] Carrying enemy: {enemy.name}");
            if (_isCarrying) return; // Prevent carrying if already carrying an enemy
            _isCarrying = true;
            this.StopAllCoroutines();
            _playerMovementController.canMove = false;
            _playerAnimator.SetTrigger(Carry);
            playerCarryController.CarryEnemy(enemy);
            this.StartCoroutine(WaitForAnimationEnd(carryWaitTime, _isCarrying));
        }
        
        private IEnumerator WaitForAnimationEnd(float waitTime, bool valueToReset) {
            yield return new WaitForSeconds(waitTime);
            _playerMovementController.canMove = true;
            valueToReset = false;
        }
    }
}