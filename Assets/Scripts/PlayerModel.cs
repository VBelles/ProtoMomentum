using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerModel : MonoBehaviour {

    //Hide in inspector (o no, da un poco igual)
    public bool _jumpButtonPressed = false;
    public bool _isLongJump = false;

    public int _facingDirection = 1;
    public Vector2 _snapArea = new Vector2(2.5f, 2.5f);

    public ActionState _currentActionState;
    public ActionState _airborneActionState;
    public ActionState _groundedActionState;

    public PowerState _currentPowerState;
    public PowerState _ssj1PowerState;
    public PowerState _ssj2PowerState;
    public PowerState _ssj3PowerState;

    [Range(0, 3), HideInInspector]
    public int _changeToPowerState = 0;

    private Rigidbody _rigidbody;
    private PowerGauge _powerGauge;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, new Vector3(_snapArea.x, _snapArea.y, 0));
    }

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _powerGauge = FindObjectOfType<PowerGauge>();
        if (!_powerGauge) { Debug.Log("No power gauge found"); }

        _groundedActionState = new GroundedActionState(this);
        _airborneActionState = new AirborneActionState(this);

        _ssj1PowerState = new Ssj1PowerState(this);
        _ssj2PowerState = new Ssj2PowerState(this);
        _ssj3PowerState = new Ssj3PowerState(this);
        SetPowerState(_ssj1PowerState);
    }

    void Start ()
    {
        SetActionState(_airborneActionState); 
    }
	
	void Update ()
    {
        if (_rigidbody.velocity.x != 0)
        {
            _facingDirection = (int)(_rigidbody.velocity.x / Mathf.Abs(_rigidbody.velocity.x));
        }
        _currentActionState.Tick();
        _currentPowerState.Tick();

        if (_changeToPowerState != 0)
        {
            switch (_changeToPowerState)
            {
                case 1:
                    SetPowerState(_ssj1PowerState);
                    break;
                case 2:
                    SetPowerState(_ssj2PowerState);
                    break;
                case 3:
                    SetPowerState(_ssj3PowerState);
                    break;
            }
            _currentActionState.RefreshPowerState();
        }

	}

    public void SetActionState(ActionState state)
    {
        if (_currentActionState != null)
        {
            _currentActionState.OnStateExit(state);
        }
        ActionState exitingState = _currentActionState;
        _currentActionState = state;
        gameObject.name = "Jugador - " + state.GetType().Name;

        if (_currentActionState != null)
        {
            _currentActionState.OnStateEnter(exitingState);
        }
    }

    public void SetPowerState(PowerState state)
    {      
        if (_currentPowerState != null)
        {
            _currentPowerState.OnStateExit(state);
        }

        PowerState exitingState = _currentPowerState;
        _currentPowerState = state;

        if (_currentPowerState != null)
        {
            _currentPowerState.OnStateEnter(exitingState);
        }
    }



    public void DirectionBindings(float x, float y)
    {
        _currentActionState.MovementInput(x, y);
    }

    public void OnJumpHighButton()
    {
        _currentActionState.OnJumpHighButton();
    }

    public void OnJumpLongButton()
    {
        _currentActionState.OnJumpLongButton();
    }

    public void OnReleaseEnergyButton()
    {
        _powerGauge.ReleaseEnergy(10000);
    }

    void OnCollisionEnter3D(Collision collision){
    
    }
  
}
