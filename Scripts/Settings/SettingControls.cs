using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingControls : MonoBehaviour
{
    public Slider sl;

    

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("brightness", sl.value);
        
    }
}
