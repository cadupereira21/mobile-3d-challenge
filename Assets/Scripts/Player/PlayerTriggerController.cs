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
        
        private bool _isDropping = false;

        private void Awake() {
            _playerMovementController = this.GetComponentInParent<PlayerMovementController>();
            _playerAnimator = this.GetComponentInParent<Animator>();
        }

        private void OnTriggerEnter(Collider other) {
            Debug.Log($"[PlayerTriggerController] Trigger entered with {other.gameObject.name}");
            if (other.gameObject.CompareTag("Enemy")) {
                HandleCollisionWithEnemy(other);
            } 
            
            if (other.gameObject.CompareTag("DropBox")) {
                HandleCollisionWithDropBox();
            }
        }

        private void HandleCollisionWithEnemy(Collider other) {
            Debug.Log($"[PlayerTriggerController] Hit an enemy");
                
            Enemy.Enemy enemy = other.gameObject.GetComponent<Enemy.Enemy>();
                
            if (enemy == null) {
                enemy = other.gameObject.GetComponentInParent<Enemy.Enemy>();
            }
                
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

        private void PunchEnemy(Enemy.Enemy enemy) {
            Debug.Log($"[PlayerTriggerController] Punching enemy: {enemy.name}");
            if (_isPunching) return; // Prevent multiple punches at the same time
            _isPunching = true;
            this.StopAllCoroutines();
            _playerMovementController.canMove = false;
            _playerAnimator.SetTrigger(Punch);
            enemy.Faint();
            this.StartCoroutine(WaitForPunchAnimationEnd());
        }

        private void CarryEnemy(Enemy.Enemy enemy) {
            Debug.Log($"[PlayerTriggerController] Carrying enemy: {enemy.name}");
            if (_isCarrying) return; // Prevent carrying if already carrying an enemy

            if (!playerCarryController.CanCarryEnemy) {
                Debug.LogWarning($"[PlayerTriggerController] Cannot carry more enemies, capacity reached.");
                return;
            }
            
            _isCarrying = true;
            this.StopAllCoroutines();
            _playerMovementController.canMove = false;
            _playerAnimator.SetTrigger(Carry);
            this.StartCoroutine(CarryEnemyCoroutine(enemy));
        }
        
        private IEnumerator WaitForPunchAnimationEnd() {
            yield return new WaitForSeconds(punchWaitTime);
            _playerMovementController.canMove = true;
            _isPunching = false;
        }
        
        private IEnumerator CarryEnemyCoroutine(Enemy.Enemy enemy) {
            yield return new WaitForSeconds(carryWaitTime);
            playerCarryController.CarryEnemy(enemy);
            enemy.BeingCarried();
            _playerMovementController.canMove = true;
            _isCarrying = false;
        }
        
        private void HandleCollisionWithDropBox() {
            Debug.Log($"[PlayerTriggerController] Hit a drop box");
            if (playerCarryController.IsCarryingEnemies) {
                Drop();
            } else {
                Debug.LogWarning($"[PlayerTriggerController] No enemy to drop.");
            }
        }

        private void Drop() {
            Debug.Log($"[PlayerTriggerController] Dropping enemies");
            if (_isDropping) return; // Prevent multiple drops at the same time
            
            _isDropping = true;
            this.StopAllCoroutines();
            _playerMovementController.canMove = false;
            playerCarryController.StopAllCoroutines();
            this.StartCoroutine(playerCarryController.DropAllEnemies(() => {
                                                                                _playerMovementController.canMove = true;
                                                                                _isDropping = false;
                                                                            }));
        }
    }
}