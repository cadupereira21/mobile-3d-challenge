using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player {
    public class PlayerCarryController : MonoBehaviour {

        [Header("Carry Settings")]
        [SerializeField] 
        private GameObject carryInitialPosition;
        
        [SerializeField]
        private float distanceBetweenCarriedEnemies = 0.5f;

        [SerializeField] 
        private int initalCarryCapacity;
        
        [Header("Drop Settings")]
        [SerializeField]
        private Transform dropBox;
        
        [SerializeField]
        [Range(1f, 10f)]
        private float dropSpeed = 1.0f;
        
        private int _carryCapacity;

        private readonly Stack<Enemy.Enemy> _carriedEnemies = new ();
        
        public bool CanCarryEnemy => _carriedEnemies.Count < _carryCapacity;
        
        public bool IsCarryingEnemies => _carriedEnemies.Count > 0;

        private void Awake() {
            _carryCapacity = initalCarryCapacity;
        }

        public void CarryEnemy(Enemy.Enemy enemy) {
            Debug.Log($"[PlayerCarryController] Carrying enemy: {enemy.gameObject.name}");
            enemy.transform.SetParent(carryInitialPosition.transform);
            enemy.transform.localPosition = new Vector3(0, _carriedEnemies.Count * distanceBetweenCarriedEnemies, 0);
            _carriedEnemies.Push(enemy);
        }

        public IEnumerator DropAllEnemies(Action callback) {
            List<Enemy.Enemy> enemiesDropped = new();
            while (_carriedEnemies.Count > 0) {
                Enemy.Enemy enemy = _carriedEnemies.Pop();
                Transform enemyTransform = enemy.transform;
                
                float aux = 0;
                while (aux < 1) {
                    aux += Time.deltaTime * dropSpeed;
                    
                    enemyTransform.position = Vector3.Lerp(enemyTransform.position, dropBox.position, aux);
                    
                    yield return null;
                }
                
                Debug.Log($"[PlayerCarryController] Dropped enemy: {enemy.gameObject.name}");
                enemiesDropped.Add(enemy);
            }

            foreach (Enemy.Enemy enemy in enemiesDropped) {
                Destroy(enemy.gameObject);
            }
            callback.Invoke();
        }
        
    }
}