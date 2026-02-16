using Engine.Core;
using Engine.Rending;
using Engine.Types;
using Engine.World;
using Engine.World.Components;
using Raylib_cs;

public static class Program
{
    public static void Main()
    {
        var scene = new Scene();

        var cameraEntity = EntityFactory.Create();
        cameraEntity.AddComponent<Camera2DComponent>();
        scene.AddEntity(cameraEntity);

        var player = EntityFactory.Create();
        player.Transform.Position = Vector2.Zero;
        player.Transform.Scale = new Vector2(0.1f, 0.1f);
        player.AddComponent<SpriteRenderer>();

        if (player.TryGetComponent<SpriteRenderer>(out var sr))
        {
            sr.SetTexture("Engine/TestAssets/Heart.png");
            sr.Layer = 1;
        }

        scene.AddEntity(player);

        EngineLoop.Run(scene, new RenderSettings
        {
            Width = 1280,
            Height = 720,
            Title = "GameProject",
            TargetFps = 60,
            VSync = true,
            ClearColor = Color.DarkBlue
        });
    }
}

