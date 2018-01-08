using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGauge : MonoBehaviour {

	 public RectTransform yellowPannel;
    public RectTransform orangePannel;
    public RectTransform redPannel;

    private int maxPowerLevel = 30000;
    //[Range(0,30000)]
    private float powerLevel = 0;
    private int dropSpeed = 5*60;//5 per frame, 300 per second

    private Vector3 dropVector;
    private const int unitsPerBar = 10000;

    private PlayerModel player;

    public delegate void PowerlessAction(bool isPowerless);
    public static event PowerlessAction OnPowerless;

    void Awake()
    {
        player = FindObjectOfType<PlayerModel>();
        if (!player) { Debug.LogError("No player found", this); }
    }

    void Start ()
    {
        redPannel.localScale = new Vector3(0, 1, 0);
        orangePannel.localScale = new Vector3(0, 1, 0);
        yellowPannel.localScale = new Vector3(0, 1, 0);

        dropVector = new Vector3(dropSpeed / (float)unitsPerBar, 0, 0);
        //Debug.Log(dropVector.x.ToString("F4"));
    }
	
	void Update ()
    {
        if (powerLevel != 0)
        {
            DecreasePower(dropSpeed * Time.deltaTime);
        }

		//Provisional
		if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Joystick1Button4)) {
            DecreasePower(unitsPerBar);
        }
		if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Joystick1Button5)) {
            IncreasePower(unitsPerBar);
        }
    }

    void OnEnable()
    {
        //Enemy.OnHit += IncreasePower;
    }

    void OnDisable()
    {
        //Enemy.OnHit -= IncreasePower;
    }

    public float GetPowerLevel()
    {
        return powerLevel;
    }

    void IncreasePower(int units)
    {
        float previousLevel = powerLevel;
        powerLevel += units;
        if (powerLevel > maxPowerLevel) { powerLevel = maxPowerLevel; }

        if (previousLevel <= 2 * unitsPerBar)
        {
            if (powerLevel > 2 * unitsPerBar)//Si estás en ssj1 o ssj2 y pasas a ssj3
            {
                //player._changeToPowerState = 3;
                player.SetPowerState(PlayerModel.PowerStates.Brutal);//si va a impulsarse queremos cambiar el estado ya, no podemos esperar al próximo frame
				//provisional
				player.actionState.RefreshPowerState();
			}
            else if (previousLevel <= unitsPerBar && powerLevel > unitsPerBar)//Si estás en ssj1 y pasas a ssj2
            {
                //player._changeToPowerState = 2;
                player.SetPowerState(PlayerModel.PowerStates.Furiosito);
				//provisional
				player.actionState.RefreshPowerState();
            }
        }

        if (previousLevel == 0 && powerLevel > 0)
        {
            if (OnPowerless != null)
            {
                OnPowerless(false);
            }
        }

        if (powerLevel > 2 * unitsPerBar)
        {
            //Poner barra naranja a 100 y situar roja
            orangePannel.localScale = new Vector3(1,1,0);
            redPannel.localScale = new Vector3((powerLevel % unitsPerBar)/unitsPerBar, 1, 0);
        }

        if (powerLevel > unitsPerBar)
        {
            //Poner barra amarilla a 100
            yellowPannel.localScale = new Vector3(1, 1, 0);

            if (powerLevel <= 2 * unitsPerBar)
            {
                //Situar barra naranja
                orangePannel.localScale = new Vector3((powerLevel % unitsPerBar) / unitsPerBar, 1, 0);
            }
        }
        else
        {
            //Situar barra amarilla
            yellowPannel.localScale = new Vector3((powerLevel % unitsPerBar) / unitsPerBar, 1, 0);
        }

        
    }

    void DecreasePower(float units)
    {
        float previousLevel = powerLevel;
        powerLevel -= units;
        if (powerLevel < 0) { powerLevel = 0; }

        if (previousLevel > unitsPerBar)
        {
            if (powerLevel <= unitsPerBar)//Si estás en ssj3 o ssj2 y pasas a ssj1
            {
                //player._changeToPowerState = 1;
                player.SetPowerState(PlayerModel.PowerStates.Basic);
				//provisional
				player.actionState.RefreshPowerState();
            }
            else if (previousLevel > 2 * unitsPerBar && powerLevel <= 2 * unitsPerBar)//Si estás en ssj3 y pasas a ssj2
            {
                //player._changeToPowerState = 2;
                player.SetPowerState(PlayerModel.PowerStates.Furiosito);
				//provisional
				player.actionState.RefreshPowerState();
            }
        }
        if (previousLevel > 0 && powerLevel == 0)
        {
            if (OnPowerless != null)
            {
                OnPowerless(true);
            }
        }

        if (powerLevel > 2 * unitsPerBar)
        {
            //Situar roja
            redPannel.localScale = new Vector3((powerLevel % unitsPerBar) / unitsPerBar, 1, 0);
        }
        else
        {
            //Poner barra roja a 0
            redPannel.localScale = new Vector3(0, 1, 0);

            if (powerLevel > unitsPerBar)
            {
                //Situar barra naranja
                orangePannel.localScale = new Vector3((powerLevel % unitsPerBar) / unitsPerBar, 1, 0);
            }
            else
            {
                //Barra naranja a 0
                orangePannel.localScale = new Vector3(0, 1, 0);
                //Situar barra amarilla
                yellowPannel.localScale = new Vector3((powerLevel % unitsPerBar) / unitsPerBar, 1, 0);
            }
        }

    }

    public void ReleaseEnergy(int units)
    {
        DecreasePower(units);
    }
}
