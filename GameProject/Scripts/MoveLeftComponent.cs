using Engine.API;
using Engine.Core;
using Engine.Entities.Components;
using Engine.Physics;
using Engine.SceneManagement;
using Engine.Types;

namespace GameProject.Scripts;

public class MoveLeftComponent : GameBehaviour
{
    private Collider2D _collider;
    private Rigidbody2D _rb;
    private float _speed = 10f;

    protected override void OnInitialize()
    {
        Entity.TryGetComponent(out _collider);
        Entity.TryGetComponent(out _rb);
        
        _collider.CollisionEnter += OnCollisionEnter;
    }

    private void OnCollisionEnter(Collider2D arg1, CollisionInfo arg2)
    {
        // var scene = Program.CreateScene();
        // SceneManager.LoadScene(scene);
    }

    protected override void OnTick()
    {
        _rb.AddForce(Vector2.Left * _speed);
    }
}