using UnityEngine;

namespace Enemy {
    public class SpawnEnemies : MonoBehaviour {
        
        [SerializeField]
        private Vector2[] spawnArea; 
        
        [SerializeField]
        [Range(1f, 10f)]
        private float spawnTime = 1.0f;
        
        [SerializeField]
        private GameObject enemyPrefab;

        private void Start() {
            this.InvokeRepeating(nameof(SpawnEnemy), 10f, spawnTime);
        }

        private void SpawnEnemy() {
            Vector2 spawnPosition = GetRandomSpawnPosition();
            GameObject go = Instantiate(enemyPrefab, this.transform.position, Quaternion.identity);
            go.transform.position = spawnPosition;
            Debug.Log($"[SpawnEnemies] Spawned enemy at position: {spawnPosition}");
        }
        
        private Vector2 GetRandomSpawnPosition() {
            float x = Random.Range(spawnArea[0].x, spawnArea[1].x);
            float y = Random.Range(spawnArea[0].y, spawnArea[1].y);
            
            return new Vector3(x, 0.37f, y);
        }
    }
}