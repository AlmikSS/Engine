using Engine.Entities;
using Engine.Entities.Components;
using Raylib_cs;
using RayRect = Raylib_cs.Rectangle;
using RayVec2 = System.Numerics.Vector2;

namespace Engine.Rending
{
    public static class Renderer
    {
        private static readonly Dictionary<string, Texture2D> _textureCache = new(StringComparer.OrdinalIgnoreCase);
        private static bool _initialized;
        private static RenderSettings _settings = new();

        public static bool ShouldClose => _initialized && Raylib.WindowShouldClose();

        public static void Initialize(RenderSettings? settings = null)
        {
            if (_initialized) return;

            _settings = settings ?? new RenderSettings();

            ConfigFlags flags = ConfigFlags.Msaa4xHint;
            if (_settings.VSync) flags |= ConfigFlags.VSyncHint;

            Raylib.SetConfigFlags(flags);
            Raylib.InitWindow(_settings.Width, _settings.Height, _settings.Title);
            Raylib.SetTargetFPS(_settings.TargetFps);

            _initialized = true;
        }

        public static void Render(Scene scene)
        {
            if (!_initialized)
                Initialize();

            var drawItems = CollectDrawItems(scene, out Camera2D camera);

            Raylib.BeginDrawing();
            Raylib.ClearBackground(_settings.ClearColor);
            Raylib.BeginMode2D(camera);

            foreach (var item in drawItems)
                Draw(item.entity, item.renderer);

            Raylib.EndMode2D();
            Raylib.EndDrawing();
        }

        public static void Shutdown()
        {
            if (!_initialized) return;

            foreach (var kv in _textureCache)
                Raylib.UnloadTexture(kv.Value);

            _textureCache.Clear();
            Raylib.CloseWindow();
            _initialized = false;
        }

        private static List<(Entity entity, SpriteRenderer renderer)> CollectDrawItems(Scene scene, out Camera2D camera)
        {
            camera = new Camera2D
            {
                Target = new RayVec2(0, 0),
                Offset = new RayVec2(_settings.Width * 0.5f, _settings.Height * 0.5f),
                Rotation = 0f,
                Zoom = 1f
            };

            var items = new List<(Entity, SpriteRenderer)>(128);

            foreach (var e in scene.ActiveEntities)
            {
                if (e.TryGetComponent<Camera2DComponent>(out var cam))
                {
                    camera = cam.BuildCamera(e.Transform.Position.X, -e.Transform.Position.Y);
                    if (cam.Offset == RayVec2.Zero)
                    {
                        camera.Offset = new RayVec2(_settings.Width * 0.5f, _settings.Height * 0.5f);
                    }
                }

                if (!e.TryGetComponent<SpriteRenderer>(out var sr)) continue;
                if (!sr.Visible || string.IsNullOrWhiteSpace(sr.TexturePath)) continue;

                items.Add((e, sr));
            }

            items.Sort(static (a, b) =>
            {
                int layer = a.Item2.Layer.CompareTo(b.Item2.Layer);
                if (layer != 0) return layer;
                return a.Item2.OrderInLayer.CompareTo(b.Item2.OrderInLayer);
            });

            return items;
        }

        private static void Draw(Entity entity, SpriteRenderer sr)
        {
            var texture = GetOrLoadTexture(sr.TexturePath);
            if (texture.Width <= 0 || texture.Height <= 0) return;

            var src = sr.SourceRect ?? new RayRect(0, 0, texture.Width, texture.Height);

            float sx = entity.Transform.Scale.X <= 0 ? 1f : entity.Transform.Scale.X;
            float sy = entity.Transform.Scale.Y <= 0 ? 1f : entity.Transform.Scale.Y;

            var dst = new RayRect(
                entity.Transform.Position.X,
                -entity.Transform.Position.Y,
                src.Width * sx,
                src.Height * sy
            );

            var origin = new RayVec2(dst.Width * sr.PivotNormalized.X, dst.Height * sr.PivotNormalized.Y);

            Raylib.DrawTexturePro(texture, src, dst, origin, sr.RotationDeg, sr.Tint);
        }

        private static Texture2D GetOrLoadTexture(string path)
        {
            if (_textureCache.TryGetValue(path, out var cached))
                return cached;

            var tex = Raylib.LoadTexture(path);
            _textureCache[path] = tex;
            return tex;
        }
    }
}