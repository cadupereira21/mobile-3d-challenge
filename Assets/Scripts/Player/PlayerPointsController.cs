using UnityEngine;
using UnityEngine.Events;

namespace Player {
    public class PlayerPointsController : MonoBehaviour {
        
        [SerializeField]
        private int enemyPointsWorth = 1;
        
        public int Points { get; private set; } = 0;
        
        public static readonly UnityEvent<int> OnPointsChanged = new ();
        
        public void AddPointsForDroppedEnemies(int droppedEnemiesCount) {
            int pointsToAdd = droppedEnemiesCount * enemyPointsWorth;
            Points += pointsToAdd;
            OnPointsChanged.Invoke(Points);
            Debug.Log($"[PlayerPointsController] Added {pointsToAdd} points for {droppedEnemiesCount} dropped enemies. Total points: {Points}");
        }
    }
}