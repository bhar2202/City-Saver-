using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterControls : MonoBehaviour
{
    
    public float hoseMovementSpeed;
    public ParticleSystem ps;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void startWater()
    {
        ps.Play();
        gameObject.SetActive(true);
    }

    public void endWater()
    {
        ps.Stop();
    }



    
}
