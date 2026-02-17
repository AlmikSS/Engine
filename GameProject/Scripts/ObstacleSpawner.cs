using Engine.API;
using Engine.Entities;
using Engine.Entities.Components;
using Engine.SceneManagement;
using Engine.Types;

namespace GameProject.Scripts;

public class ObstacleSpawner : GameBehaviour
{
    private bool _canSpawn = true;
    private int _currentTimer;
    private int _timer;
    private int _maxTimer = 250;
    private int _minTimer = 70;
    
    protected override void OnTick()
    {
        if (!_canSpawn)
            return;
        
        _canSpawn = false;
        _timer = Random.Shared.Next(_minTimer, _maxTimer);
        Spawn();
    }
    
    protected override void OnFixedTick()
    {
        if (_canSpawn)
            return;
        
        _currentTimer++;
        if (_currentTimer < _timer)
            return;
        
        _currentTimer = 0;
        _canSpawn = true;
    }

    private void Spawn()
    {
        var upDown = Random.Shared.Next(0, 2);
        var Position = Vector2.Zero;

        if (upDown == 1)
        {
            Position = new Vector2(100, Random.Shared.Next(150, 250));
        }
        else
        {
            Position = new Vector2(100, Random.Shared.Next(-250, -150));
        }
        
        var box = EntityFactory.Create(SceneManager.CurrentScene);
        box.Transform.Scale = new Vector2(0.3f, 1.5f);
        box.Transform.Position = Position;
        var renderer = box.AddComponent<SpriteRenderer>();
        renderer.SetTexture("C:\\Users\\2008A\\Documents\\RiderProjects\\Engine\\GameProject\\Assets\\Box.png");
        renderer.Layer = 1;
        var rb = box.AddComponent<Rigidbody2D>();
        rb.UseGravity = false;
        var collider = box.AddComponent<BoxCollider2D>();
        collider.Size = new Vector2(80, 400);
        //collider.IsTrigger = true;
        box.AddComponent<MoveLeftComponent>();
    }
}