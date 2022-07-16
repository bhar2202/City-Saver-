using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateSettings : MonoBehaviour
{
    public Light lgt;

    // Update is called once per frame
    void Update()
    {
        lgt.intensity = PlayerPrefs.GetFloat("brightness");
    }
}
