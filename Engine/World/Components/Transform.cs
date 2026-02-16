using Engine.Types;

namespace Engine.World.Components
{
    public sealed class Transform : Component
    {
        public Vector2 Position = Vector2.Zero;
        public Vector2 Scale = Vector2.One;
    }
}