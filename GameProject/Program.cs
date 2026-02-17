using Engine.Core;
using Engine.Entities;
using Engine.Entities.Components;
using Engine.Physics;
using Engine.Rending;
using Engine.Types;
using GameProject.Scripts;
using Raylib_cs;

namespace GameProject;

public class Program
{
    public static void Main()
    {
        var renderSettings = new RenderSettings
        {
            Width = 1280,
            Height = 720,
            PixelsPerUnit = 100,
            Title = "Test_Project-VGEngine",
            TargetFps = 60,
            VSync = true,
            ClearColor = Color.SkyBlue,
            DrawGizmos = true,
            ColliderGizmoColor = Color.Lime
        };
        
        var physicsSettings = new PhysicsSettings()
        {
            Gravity = new Vector2(0, -9.81f * 50),
        };

        var scene = CreateScene();
        EngineLoop.Run(scene, physicsSettings, renderSettings);
    }

    public static Scene CreateScene()
    {
        var scene = new Scene();
        var camera = EntityFactory.Create(scene);
        camera.AddComponent<Camera2DComponent>();
        
        var player = EntityFactory.Create(scene);
        player.Transform.Scale = new Vector2(0.15f, 0.15f);
        var playerRb = player.AddComponent<Rigidbody2D>();
        playerRb.Mass = 1;
        playerRb.IsStatic = true;
        var collider = player.AddComponent<BoxCollider2D>();
        collider.Size = new Vector2(100, 100);
        var spriteRenderer = player.AddComponent<SpriteRenderer>();
        spriteRenderer.SetTexture("C:\\Users\\2008A\\Documents\\RiderProjects\\Engine\\GameProject\\Assets\\Player.png");
        spriteRenderer.Layer = 1;
        player.AddComponent<PlayerLogic>();
        
        var downBorder = EntityFactory.Create(scene);
        downBorder.Transform.Position = new Vector2(0, -350);
        var downBorderRb = downBorder.AddComponent<Rigidbody2D>();
        downBorderRb.IsStatic = true;
        var downBorderCollider = downBorder.AddComponent<BoxCollider2D>();
        downBorderCollider.Size = new Vector2(1500, 10);
        
        var upBorder = EntityFactory.Create(scene);
        upBorder.Transform.Position = new Vector2(0, 350);
        var upBorderRb = upBorder.AddComponent<Rigidbody2D>();
        upBorderRb.IsStatic = true;
        var upBorderCollider = upBorder.AddComponent<BoxCollider2D>();
        upBorderCollider.Size = new Vector2(1500, 10);

        var obstacleSpawner = EntityFactory.Create(scene);
        obstacleSpawner.AddComponent<ObstacleSpawner>();
        return scene;
    }
}