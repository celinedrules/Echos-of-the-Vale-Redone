using UnityEngine;

namespace BitWaveLabs.EchosOfTheVale.Player
{
    /// <summary>
    /// Handles player movement using input provided by PlayerInputHandler.
    /// </summary>
    [RequireComponent(typeof(PlayerInputHandler))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// Reference to the input handler on this player.
        /// </summary>
        public PlayerInputHandler InputHandler { get; private set; }

        [SerializeField] private float moveSpeed = 3f;

        private Rigidbody2D _rigidbody2D;
        private Vector2 _moveInput;

        private void Awake()
        {
            InputHandler = GetComponent<PlayerInputHandler>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _moveInput = InputHandler.MoveInput.normalized;
        }

        private void FixedUpdate()
        {
            _rigidbody2D.linearVelocity = _moveInput * moveSpeed;
        }
    }
}