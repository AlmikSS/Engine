using Engine.API;
using Engine.Entities.Components;
using Engine.Input;
using Engine.Types;

namespace GameProject.Scripts;

public class PlayerLogic : GameBehaviour
{
    private Rigidbody2D _rb;
    private bool _isInit;
    private float _force = 12000f;

    protected override void OnInitialize()
    {
        base.OnInitialize();
        if (!Entity.TryGetComponent<Rigidbody2D>(out var rb))
            return;
        
        _rb = rb;
        _isInit = true;
    }

    protected override void OnTick()
    {
        if (!_isInit)
            return;
        
        if (InputSystem.IsStarted(KeyCode.Space))
        {
            if (_rb.IsStatic)
                _rb.IsStatic = false;
            
            _rb.AddForce(Vector2.Up * _force, true);
        }
    }
}