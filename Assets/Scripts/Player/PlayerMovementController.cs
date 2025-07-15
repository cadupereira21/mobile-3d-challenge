using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
    public class PlayerMovementController : MonoBehaviour {
        private static readonly int IsRunning = Animator.StringToHash("isRunning");

        [SerializeField]
        [Range(0f, 10f)]
        private float moveSpeed = 5f;
        
        [SerializeField]
        private float controllerDeadZone = 0.1f;

        public bool canMove = true;

        private Vector2 _input;
        
        private CharacterController _characterController;

        private Transform _mainCamera;

        private Animator _animator;

        private void Awake() {
            _characterController = this.GetComponent<CharacterController>();
            _animator = this.GetComponent<Animator>();
            if (Camera.main != null) _mainCamera = Camera.main.transform;
        }

        private void Update() {
            RotatePlayer();
            Move();

            _animator.SetBool(IsRunning, _input != Vector2.zero);
        }

        public void Move(InputAction.CallbackContext context) {
            _input = context.ReadValue<Vector2>();
        }

        private void Move() {
            if (!canMove || _input.magnitude <= controllerDeadZone) return;
            _characterController.Move(this.transform.forward * (_input.magnitude * moveSpeed * Time.deltaTime));        
            _characterController.Move(Vector3.down * (9.81f * Time.deltaTime));
        }

        private void RotatePlayer() {
            Vector3 forward = _mainCamera.TransformDirection(Vector3.forward);
        
            Vector3 right = _mainCamera.TransformDirection(Vector3.right);
            
            Vector3 targetDirection = _input.x * right + _input.y * forward; 
            
            if (canMove && _input != Vector2.zero && targetDirection.magnitude > controllerDeadZone)
            {
                Quaternion freeRotation = Quaternion.LookRotation(targetDirection.normalized);
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(new Vector3(this.transform.eulerAngles.x, freeRotation.eulerAngles.y, this.transform.eulerAngles.z)), 10 * Time.deltaTime);
            }
        }
        
    }
}