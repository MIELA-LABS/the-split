using UnityEngine;

namespace MielaLabs.TheSplit
{
    // Bu script karakterin temel hareketini sağlar.
    // WASD veya Ok tuşları ile çalışır.
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [Tooltip("Karakterin hareket hızı")]
        [SerializeField] private float _moveSpeed = 10f;
        
        [Tooltip("Dönüş yumuşaklığı")]
        [SerializeField] private float _rotationSpeed = 15f;

        // Unity Bileşenleri
        private CharacterController _controller;
        private Vector3 _moveDirection;

        private void Awake()
        {
            // CharacterController bileşenini otomatik bul
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            HandleMovement();
            HandleRotation();
        }

        private void HandleMovement()
        {
            // Klavyeden girdileri al (WASD)
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // Yön vektörünü oluştur (Y ekseni 0, çünkü uçmuyoruz)
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                // Karakteri hareket ettir
                _controller.Move(direction * _moveSpeed * Time.deltaTime);
            }
        }

        private void HandleRotation()
        {
            // Hareket yönüne doğru yumuşakça dön
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }
        }
    }
}