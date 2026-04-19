using UnityEngine;

namespace BitWaveLabs.EchosOfTheVale.Core
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    
    public class Entity : MonoBehaviour
    {
        protected StateMachine.StateMachine StateMachine;
        
        public Animator Animator  { get; private set; }
        public Rigidbody2D Rigidbody { get; private set; }
        public Direction FacingDirection { get; private set; } = Direction.Down;

        protected virtual void Awake()
        {
            //Stats = new EntityStats();
            
            Animator = GetComponentInChildren<Animator>();
            Rigidbody = GetComponent<Rigidbody2D>();
            //EntityFx = GetComponentInChildren<EntityFx>();
            
            StateMachine = new StateMachine.StateMachine();
        }
        
        protected virtual void Start()
        {
            // InitializeStatsFromData();
            // SetupHealth();
            // Health?.Initialize();
        }
        
        protected virtual void Update()
        {
            StateMachine.UpdateActiveState();
        }
        
        public void SetVelocity(Vector2 velocity)
        {
            Rigidbody.linearVelocity = velocity;
            
            if (velocity == Vector2.zero)
                return;
            
            if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
                FacingDirection = velocity.x > 0 ? Direction.Right : Direction.Left;
            else
                FacingDirection = velocity.y > 0 ? Direction.Up : Direction.Down;
        }
    }
}