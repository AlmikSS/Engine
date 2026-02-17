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

        public void AddForce(Vector2 force)
        {
            if (IsStatic)
                return;
            
            Velocity += force / Mass * Time.FixedDeltaTime;
        }

        internal void PhysicsTick(PhysicsSettings settings)
        {
            if (IsStatic)
                return;
            
            Velocity += settings.Gravity * Time.FixedDeltaTime;
            Owner.Transform.Position += Velocity * Time.FixedDeltaTime;
        }
    }
}