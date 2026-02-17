using Engine.Core;
using Engine.Physics;
using Engine.Types;

namespace Engine.Entities.Components
{
    public sealed class Rigidbody2D : Component
    {
        public Vector2 Velocity;
        public float Mass = 1f;
        public bool IsStatic = false;
        public bool UseGravity = true;

        public void AddForce(Vector2 force, bool resetVelocity = false)
        {
            if (IsStatic)
                return;
            
            if (resetVelocity)
                Velocity = Vector2.Zero;
            
            Velocity += force / Mass * Time.FixedDeltaTime;
        }

        internal void PhysicsTick(PhysicsSettings settings)
        {
            if (IsStatic)
                return;
            
            if (UseGravity)
                Velocity += settings.Gravity * Time.FixedDeltaTime;
            
            Owner.Transform.Position += Velocity * Time.FixedDeltaTime;
        }
    }
}