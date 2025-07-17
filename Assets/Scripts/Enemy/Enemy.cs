using System.Collections;
using UnityEngine;

namespace Enemy {
    public class Enemy : MonoBehaviour {
        
        [SerializeField]
        [Range(0f, 5f)]
        private float faintWaitTime = 0.5f;
        
        [SerializeField]
        [Range(0f, 5f)]
        private float carryWaitTime = 1.0f;
        
        private Animator _enemyAnimator;
        
        private Rigidbody[] _ragdollRigidbodies;
        
        public bool IsKnockedDown = false;
        
        public bool CanBeCarried = false;

        private void Awake() {
            _enemyAnimator = this.GetComponentInParent<Animator>();
            _ragdollRigidbodies = this.GetComponentsInChildren<Rigidbody>();
            DisableRagdoll();
        }

        public void Faint() {
            this.StartCoroutine(FaintCoroutine());
        }

        public void BeingCarried() {
            CanBeCarried = false;
        }
        
        private IEnumerator FaintCoroutine() {
            yield return new WaitForSeconds(faintWaitTime);
            EnableRagdoll();
            IsKnockedDown = true;
            yield return new WaitForSeconds(carryWaitTime);
            CanBeCarried = true;
            DisableBodyRagdolls();
        }

        private void DisableRagdoll() {
            foreach (Rigidbody rb in _ragdollRigidbodies) {
                rb.isKinematic = true;
            }

            _enemyAnimator.enabled = true;
        }

        private void DisableBodyRagdolls() {
            foreach (Rigidbody rb in _ragdollRigidbodies) {
                if (rb.mass > 10) {
                    rb.isKinematic = true;
                }
            }
            
            _enemyAnimator.enabled = false;
        }
        
        
        private void EnableRagdoll() {
            foreach (Rigidbody rb in _ragdollRigidbodies) {
                rb.isKinematic = false;
            }
            _enemyAnimator.enabled = false;
        }
    }
}