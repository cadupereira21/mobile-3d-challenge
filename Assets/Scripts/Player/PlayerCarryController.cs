using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class PlayerCarryController : MonoBehaviour {

        [SerializeField] 
        private GameObject carryInitialPosition;
        
        [SerializeField]
        private float distanceBetweenCarriedEnemies = 0.5f;

        [SerializeField] 
        private int initalCarryCapacity;
        
        private int _carryCapacity;

        private readonly List<Enemy.Enemy> _carriedEnemies = new ();
        
        public bool CanCarryEnemy => _carriedEnemies.Count-1 >= _carryCapacity;

        private void Awake() {
            _carryCapacity = initalCarryCapacity;
        }

        public void CarryEnemy(Enemy.Enemy enemy) {
            Debug.Log($"[PlayerCarryController] Carrying enemy: {enemy.gameObject.name}");
            enemy.transform.SetParent(carryInitialPosition.transform);
            enemy.transform.localPosition = new Vector3(0, _carriedEnemies.Count * distanceBetweenCarriedEnemies, 0);
            _carriedEnemies.Add(enemy);
        }

        public void DropEnemies() {
            _carriedEnemies.Clear();
        }
        
    }
}