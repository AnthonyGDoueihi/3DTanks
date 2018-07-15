using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetUI : MonoBehaviour {

    public Sprite moving;
    public Sprite firing;
    public Sprite reloading;

    Image graphic;
    PlayerTankController player;

    TankAimingComponent.FiringState state;

	// Use this for initialization
	void Start () {
        graphic = GetComponent<Image>();
        player = FindObjectOfType<PlayerTankController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
            state = player.GetComponent<TankAimingComponent>().GetFiringState();
        }
        if (state == TankAimingComponent.FiringState.Reloading)
        {
            graphic.sprite = reloading;
        }
        else if (state == TankAimingComponent.FiringState.Moving)
        {
            graphic.sprite = moving;
        }
        else
        {
            graphic.sprite = firing;
        }

    }
}
