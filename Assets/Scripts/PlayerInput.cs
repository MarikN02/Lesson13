using UnityEngine;

namespace WildBall.Inputs
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerInput : MonoBehaviour
    {
        private Vector3 movement;
        private PlayerMovement playerMovement;


        private void Awake()
        {
            playerMovement = GetComponent<PlayerMovement>();
        }

        void Update()
        {
            float horizontal = Input.GetAxis(GlobalStringVars.HORIZONTAL_AXIS);
            float verticalt = Input.GetAxis(GlobalStringVars.VERTICAL_AXIS);

            movement = new Vector3(horizontal, 0, verticalt).normalized;
        }

        private void FixedUpdate()
        {
            playerMovement.MoveCharacter(movement);
        }
    }
    
}