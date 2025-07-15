using UnityEngine;

namespace Enemy {
    public class Enemy : MonoBehaviour {

        private static readonly int FaintParam = Animator.StringToHash("faint");
        
        private Animator _enemyAnimator;
        
        public bool IsKnockedDown { get; private set; } = false;

        private void Awake() {
            _enemyAnimator = this.GetComponent<Animator>();
        }
        
        public void Faint() {
            _enemyAnimator.SetTrigger(FaintParam);
            IsKnockedDown = true;
            Debug.Log($"[Enemy] {this.gameObject.name} has fainted.");
        }
    }
}