using UnityEngine;

namespace MielaLabs.TheSplit
{
    /// <summary>
    /// Handles basic character movement using the CharacterController component.
    /// Supports WASD and Arrow Keys input.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [Tooltip("Movement speed of the character in units per second.")]
        [SerializeField] private float _moveSpeed = 10f;
        
        [Tooltip("How fast the character rotates towards the movement direction.")]
        [SerializeField] private float _rotationSpeed = 15f;

        // Component References
        private CharacterController _controller;
        
        private void Awake()
        {
            // Automatically retrieve the CharacterController attached to this GameObject
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            HandleMovement();
            HandleRotation();
        }

        /// <summary>
        /// Calculates movement vector based on input and moves the character.
        /// </summary>
        private void HandleMovement()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // Create direction vector (Y is 0 because we don't want to fly/sink)
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                // Move the character using Unity's CharacterController
                _controller.Move(direction * _moveSpeed * Time.deltaTime);
            }
        }

        /// <summary>
        /// Rotates the character to face the movement direction.
        /// </summary>
        private void HandleRotation()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                // Smoothly rotate towards the target direction
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }
        }
    }
}