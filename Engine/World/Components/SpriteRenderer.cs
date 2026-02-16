using Engine.Types;
using Raylib_cs;

namespace Engine.World.Components
{
    public sealed class SpriteRenderer : Component
    {
        public string TexturePath { get; private set; } = string.Empty;

        public int Layer { get; set; } = 0;
        public int OrderInLayer { get; set; } = 0;
        public bool Visible { get; set; } = true;

        public float RotationDeg { get; set; } = 0f;
        public Vector2 PivotNormalized { get; set; } = new Vector2(0.5f, 0.5f);
        public Color Tint { get; set; } = Color.White;

        public Rectangle? SourceRect { get; set; }

        public void SetTexture(string texturePath)
        {
            TexturePath = texturePath ?? string.Empty;
        }
    }
}