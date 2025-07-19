using UnityEngine;
using UnityEngine.Events;

namespace Player {
    public class PlayerPointsController : MonoBehaviour {
        
        [SerializeField]
        private int enemyPointsWorth = 1;
        
        public int Points { get; private set; } = 0;
        
        public int EnemyPointsWorth => enemyPointsWorth;

        public static readonly UnityEvent<int> OnPointsChanged = new ();
        
        public void AddPointsForDroppedEnemies(int droppedEnemiesCount) {
            int pointsToAdd = droppedEnemiesCount * enemyPointsWorth;
            Points += pointsToAdd;
            OnPointsChanged.Invoke(Points);
            Debug.Log($"[PlayerPointsController] Added {pointsToAdd} points for {droppedEnemiesCount} dropped enemies. Total points: {Points}");
        }
        
        public void SubtractPoints(int pointsToSubtract) {
            Points -= pointsToSubtract;
            OnPointsChanged.Invoke(Points);
            Debug.Log($"[PlayerPointsController] Subtracted {pointsToSubtract} points. Total points: {Points}");
        }
        
        public void ChangePointsWorth(int newPoints) {
            enemyPointsWorth = newPoints;
            Debug.Log($"[PlayerPointsController] Changed enemy points worth to {enemyPointsWorth}");
        }
    }
}