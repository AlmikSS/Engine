using Raylib_cs;

namespace Engine.Rending
{
    public sealed class RenderSettings
    {
        public int Width { get; init; } = 1280;
        public int Height { get; init; } = 720;
        public float PixelsPerUnit { get; init; } = 100f;
        public string Title { get; init; } = "Engine";
        public int TargetFps { get; init; } = 60;
        public bool VSync { get; init; } = true;
        public Color ClearColor { get; init; } = Color.Black;
        public bool DrawGizmos { get; init; } = false;
        public Color ColliderGizmoColor { get; init; } = Color.Lime;
    }
}