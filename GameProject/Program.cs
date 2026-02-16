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
        player.AddComponent<MoveComponent>();

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
    private Rigidbody2D _rb;

    protected override void OnInitialize()
    {
        base.OnInitialize();
        
        if (!Entity.TryGetComponent(out Rigidbody2D rigidbody))
            return;
        
        _rb = rigidbody;
    }

    protected override void OnFixedTick()
    {
        base.OnFixedTick();
        
        if (_rb == null)
            return;

        if (InputSystem.IsPressed(KeyCode.W))
        {
            _rb.Velocity = new Vector2(_rb.Velocity.X, 100);
        }
        if (InputSystem.IsPressed(KeyCode.S))
        {
            _rb.Velocity = new Vector2(_rb.Velocity.X, -100);
        }
        if (InputSystem.IsPressed(KeyCode.A))
        {
            _rb.Velocity = new Vector2(-100, _rb.Velocity.Y);
        }
        if (InputSystem.IsPressed(KeyCode.D))
        {
            _rb.Velocity = new Vector2(100, _rb.Velocity.Y);
        }
    }
}

