using UnityEngine;

namespace GameCamera {
    public class FollowPlayer : MonoBehaviour {
       
        [Header("Player")]
        [SerializeField]
        private Transform playerTransform;
        
        [Header("Camera Settings")]
        [SerializeField]
        private Vector3 offset = new Vector3(0, 5, -10);
        
        [SerializeField]
        private float smoothSpeed = 0.125f;

        private void Awake() {
            if (playerTransform == null) {
                playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
                if (playerTransform == null) {
                    Debug.LogError("Player Transform not found. Please assign it in the inspector or ensure the player has the 'Player' tag.");
                }
            }
        }

        private void LateUpdate() {
            Vector3 desiredPosition = playerTransform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(this.transform.position, desiredPosition, smoothSpeed);
            this.transform.position = smoothedPosition;

            this.transform.LookAt(playerTransform);
        }
        
    }
}
