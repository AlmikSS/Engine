using Engine.Core;
using Engine.Physics;
using Engine.Types;

namespace Engine.Entities.Components
{
    public sealed class Rigidbody2D : Component
    {
        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; private set; }
        public float Mass { get; private set; } = 1;

        private Vector2 _force;
        
        internal override void OnSceneCreated()
        {
            PhysicsEngine.Register(this);
        }

        internal void PhysicsTick(PhysicsSettings settings)
        {
            Velocity += settings.Gravity * Time.FixedDeltaTime;
            Position += Velocity * Time.FixedDeltaTime;
        }

        internal override void Tick()
        {
            base.Tick();
            Owner.Transform.Position = Position;
        }
    }
}