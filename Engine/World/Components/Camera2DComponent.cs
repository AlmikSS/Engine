using RayCamera2D = Raylib_cs.Camera2D;
using RayVec2 = System.Numerics.Vector2;

namespace Engine.World.Components
{
    public sealed class Camera2DComponent : Component
    {
        public float Zoom { get; set; } = 1f;
        public float RotationDeg { get; set; } = 0f;
        public RayVec2 Offset { get; set; } = RayVec2.Zero;

        internal RayCamera2D BuildCamera(float worldX, float worldY)
        {
            return new RayCamera2D
            {
                Target = new RayVec2(worldX, worldY),
                Offset = Offset,
                Rotation = RotationDeg,
                Zoom = Zoom
            };
        }
    }
}