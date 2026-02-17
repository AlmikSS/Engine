using Engine.Types;

namespace Engine.Entities.Components
{
    public class BoxCollider2D : Collider2D
    {
        public Vector2 Size = new(1f, 1f);
        public Vector2 Offset = Vector2.Zero;

        public override Aabb GetAabb()
        {
            var center = Owner.Transform.Position + Offset;
            var halfX = Size.X * 0.5f;
            var halfY = Size.Y * 0.5f;
            return new Aabb(
                center.X - halfX,
                center.Y - halfY,
                center.X + halfX,
                center.Y + halfY);
        }
    }
}