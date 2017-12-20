using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGauge : MonoBehaviour {

    public RectTransform _yellowPannel;
    public RectTransform _orangePannel;
    public RectTransform _redPannel;

    private int _maxPowerLevel = 30000;
    //[Range(0,30000)]
    private float _powerLevel = 0;
    private int _dropSpeed = 5*60;//5 per frame, 300 per second

    private Vector3 _dropVector;
    private const int _unitsPerBar = 10000;

    private PlayerModel _player;
  
    public delegate void PowerlessAction(bool isPowerless);
    public static event PowerlessAction OnPowerless;

    void Awake()
    {
        _player = FindObjectOfType<PlayerModel>();
        if (!_player) { Debug.LogError("No player found", this); }

      
    }

    void Start ()
    {
        _redPannel.localScale = new Vector3(0, 1, 0);
        _orangePannel.localScale = new Vector3(0, 1, 0);
        _yellowPannel.localScale = new Vector3(0, 1, 0);

        _dropVector = new Vector3(_dropSpeed / (float)_unitsPerBar, 0, 0);
        //Debug.Log(_dropVector.x.ToString("F4"));
    }
	
	void Update ()
    {
        if (_powerLevel != 0)
        {
            DecreasePower(_dropSpeed * Time.deltaTime);
        }
    }

    void OnEnable()
    {
       
    }

    void OnDisable()
    {
       
    }

    public float GetPowerLevel()
    {
        return _powerLevel;
    }

    void IncreasePower(int units)
    {
        float previousLevel = _powerLevel;
        _powerLevel += units;
        if (_powerLevel > _maxPowerLevel) { _powerLevel = _maxPowerLevel; }

        if (previousLevel <= 2 * _unitsPerBar)
        {
            if (_powerLevel > 2 * _unitsPerBar)//Si estás en ssj1 o ssj2 y pasas a ssj3
            {
                //_player._changeToPowerState = 3;
                _player.SetPowerState(_player._ssj3PowerState);//si va a impulsarse queremos cambiar el estado ya, no podemos esperar al próximo frame
            }
            else if (previousLevel <= _unitsPerBar && _powerLevel > _unitsPerBar)//Si estás en ssj1 y pasas a ssj2
            {
                //_player._changeToPowerState = 2;
                _player.SetPowerState(_player._ssj2PowerState);
            }
        }

        if (previousLevel == 0 && _powerLevel > 0)
        {
            if (OnPowerless != null)
            {
                OnPowerless(false);
            }
        }

        if (_powerLevel > 2 * _unitsPerBar)
        {
            //Poner barra naranja a 100 y situar roja
            _orangePannel.localScale = new Vector3(1,1,0);
            _redPannel.localScale = new Vector3((_powerLevel % _unitsPerBar)/_unitsPerBar, 1, 0);
        }

        if (_powerLevel > _unitsPerBar)
        {
            //Poner barra amarilla a 100
            _yellowPannel.localScale = new Vector3(1, 1, 0);

            if (_powerLevel <= 2 * _unitsPerBar)
            {
                //Situar barra naranja
                _orangePannel.localScale = new Vector3((_powerLevel % _unitsPerBar) / _unitsPerBar, 1, 0);
            }
        }
        else
        {
            //Situar barra amarilla
            _yellowPannel.localScale = new Vector3((_powerLevel % _unitsPerBar) / _unitsPerBar, 1, 0);
        }

        
    }

    void DecreasePower(float units)
    {
        float previousLevel = _powerLevel;
        _powerLevel -= units;
        if (_powerLevel < 0) { _powerLevel = 0; }

        if (previousLevel > _unitsPerBar)
        {
            if (_powerLevel <= _unitsPerBar)//Si estás en ssj3 o ssj2 y pasas a ssj1
            {
                _player._changeToPowerState = 1;
                //_player.SetPowerState(_player._ssj1PowerState);
            }
            else if (previousLevel > 2 * _unitsPerBar && _powerLevel <= 2 * _unitsPerBar)//Si estás en ssj3 y pasas a ssj2
            {
                _player._changeToPowerState = 2;
                //_player.SetPowerState(_player._ssj2PowerState);
            }
        }
        if (previousLevel > 0 && _powerLevel == 0)
        {
            if (OnPowerless != null)
            {
                OnPowerless(true);
            }
        }

        if (_powerLevel > 2 * _unitsPerBar)
        {
            //Situar roja
            _redPannel.localScale = new Vector3((_powerLevel % _unitsPerBar) / _unitsPerBar, 1, 0);
        }
        else
        {
            //Poner barra roja a 0
            _redPannel.localScale = new Vector3(0, 1, 0);

            if (_powerLevel > _unitsPerBar)
            {
                //Situar barra naranja
                _orangePannel.localScale = new Vector3((_powerLevel % _unitsPerBar) / _unitsPerBar, 1, 0);
            }
            else
            {
                //Barra naranja a 0
                _orangePannel.localScale = new Vector3(0, 1, 0);
                //Situar barra amarilla
                _yellowPannel.localScale = new Vector3((_powerLevel % _unitsPerBar) / _unitsPerBar, 1, 0);
            }
        }

    }

    public void ReleaseEnergy(int units)
    {
        DecreasePower(units);
    }
}
