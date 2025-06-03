using UnityEngine;

namespace WildBall.Inputs
{
    [RequireComponent(typeof(Rigidbody))]

    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField, Range(0, 10)] private float speed = 8.0f;
        private Rigidbody playerRigidbody;


        private void Awake()
        {
            playerRigidbody = GetComponent<Rigidbody>();
        }

        public void MoveCharacter(Vector3 movement)
        {
            playerRigidbody.AddForce(movement * speed);
        }

#if UNITY_EDITOR
        [ContextMenu("Reset values")]
        public void Reset()
        {
            speed = 8;
        }
#endif
    }
    
    
}
