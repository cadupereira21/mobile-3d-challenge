﻿using System;
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
        private int carryCapacity;
        
        [Header("Drop Settings")]
        [SerializeField]
        private Transform dropBox;
        
        [SerializeField]
        [Range(1f, 10f)]
        private float dropSpeed = 1.0f;
        
        private PlayerPointsController _playerPointsController;

        public int CarryCapacity => carryCapacity;

        private readonly Stack<Enemy.Enemy> _carriedEnemies = new ();
        
        public bool CanCarryEnemy => _carriedEnemies.Count < CarryCapacity;
        
        public bool IsCarryingEnemies => _carriedEnemies.Count > 0;

        private void Awake() {
            _playerPointsController = this.GetComponentInParent<PlayerPointsController>();
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
            
            _playerPointsController.AddPointsForDroppedEnemies(enemiesDropped.Count);

            foreach (Enemy.Enemy enemy in enemiesDropped) {
                Destroy(enemy.gameObject);
            }
            
            callback.Invoke();
        }

        public void ChangeCarryCapacity(int newCarryCapacity) {
            carryCapacity = newCarryCapacity;
            Debug.Log($"[PlayerCarryController] Carry capacity changed to: {CarryCapacity}");
            
        }
        
    }
}