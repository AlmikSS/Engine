using Engine.Physics;
using Engine.Types;

namespace Engine.Entities.Components
{
    public abstract class Collider2D : Component
    {
        public bool IsTrigger;

        public event Action<Collider2D, CollisionInfo>? CollisionEnter;
        public event Action<Collider2D, CollisionInfo>? CollisionStay;
        public event Action<Collider2D>? CollisionExit;

        public Rigidbody2D? AttachedRigidbody
        {
            get
            {
                Owner.TryGetComponent<Rigidbody2D>(out var rb);
                return rb;
            }
        }
        
        internal void InvokeEnter(Collider2D other, CollisionInfo info) => CollisionEnter?.Invoke(other, info);
        internal void InvokeStay(Collider2D other, CollisionInfo info) => CollisionStay?.Invoke(other, info);
        internal void InvokeExit(Collider2D other) => CollisionExit?.Invoke(other);

        public abstract Aabb GetAabb();
    }
    
    public readonly record struct Aabb(float MinX, float MinY, float MaxX, float MaxY)
    {
        public float Width => MaxX - MinX;
        public float Height => MaxY - MinY;
        public Vector2 Center => new((MinX + MaxX) * 0.5f, (MinY + MaxY) * 0.5f);
    }
}