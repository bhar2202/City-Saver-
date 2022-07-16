using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save 
{
    //player attribues that are saved locally
    public float playerXPos;
    public float playerZPos;
    public float playerYRotation;

    //upgrades that get saved locally
    public float maxSpeed;
    public float gasConsumptionRate;
    public float hoseSize;

    //upgrades that have been bought
    public bool wheelUpgradeBought;
    public bool hoseUpgradeBought;
    public bool fuelUpgradeBought;


    //in-game data that gets saved locally
    public int buildingsSaved;
    public string mostBuildingsSaved;
    public float GasUsed;
    public int minutesLeft;
    public int secondsLeft;
    
    //other game data that gets saved locally
    public string date;
    public int money;
    public int currentDay;
}
