using BitWaveLabs.EchosOfTheVale.Core;
using BitWaveLabs.EchosOfTheVale.StateMachine.States.PlayerStates;
using UnityEngine;

namespace BitWaveLabs.EchosOfTheVale.Player
{
    /// <summary>
    /// Handles player movement using input provided by PlayerInputHandler.
    /// </summary>
    [RequireComponent(typeof(PlayerInputHandler))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : Entity
    {
        private PlayerStateFactory _factory;

        public PlayerInputHandler InputHandler { get; private set; }

        [SerializeField] private float moveSpeed = 3f;

        private Rigidbody2D _rigidbody2D;
        private Vector2 _moveInput;

        public float MoveSpeed
        {
            get => moveSpeed;
            set => moveSpeed = value;
        }

        public IdleState IdleState { get; private set; }
        public MoveState MoveState { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            InputHandler = GetComponent<PlayerInputHandler>();
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _factory = new PlayerStateFactory(this, StateMachine);

            IdleState = _factory.Create<IdleState>("Idle");
            MoveState = _factory.Create<MoveState>("Move");
        }

        protected override void Start()
        {
            base.Start();

            StateMachine.Initialize(IdleState);

            // if (!Managers.GameManager.Instance.InventoryRuntimeData.HasValidData)
            //     SetupEquipment();
            //
            // ((PlayerStats)Stats)?.LoadFromRuntimeData();
        }
    }
}