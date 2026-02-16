using Engine.API;
using Engine.Core;
using Engine.Entities;
using Engine.Entities.Components;
using Engine.Input;
using Engine.Physics;
using Engine.Rending;
using Engine.Types;
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
        player.AddComponent<Rigidbody2D>();

        if (player.TryGetComponent<SpriteRenderer>(out var sr))
        {
            sr.SetTexture("C:/Users/2008A/Documents/RiderProjects/Engine/Engine/TestAssets/Heart.png");
            sr.Layer = 1;
        }

        scene.AddEntity(player);

        var physicsSettings = new PhysicsSettings
        {
            Gravity = new Vector2(0,  -9.8f),
        };
        
        var renderSettings = new RenderSettings
        {
            Width = 1280,
            Height = 720,
            Title = "GameProject",
            TargetFps = 60,
            VSync = true,
            ClearColor = Color.DarkBlue
        };

        EngineLoop.Run(scene, physicsSettings, renderSettings);
    }
}

public class MoveComponent : GameBehaviour
{
    protected override void OnTick()
    {
        base.OnTick();

        if (InputSystem.IsPressed(KeyCode.W))
            Transform.Position.Y += (100 * Time.DeltaTime);
        if (InputSystem.IsPressed(KeyCode.S))
            Transform.Position.Y -= (100 * Time.DeltaTime);
        if (InputSystem.IsPressed(KeyCode.A))
            Transform.Position.X -= (100 * Time.DeltaTime);
        if (InputSystem.IsPressed(KeyCode.D))
            Transform.Position.X += (100 * Time.DeltaTime);
    }
}

