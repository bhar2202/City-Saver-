using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringUpExitPanel : MonoBehaviour
{
    public GameObject exitPanel;


    public void Start()
    {
        
        exitPanel.SetActive(false);
    }

    // Start is called before the first frame update
    public void bringUpPanel()
    {
        
        exitPanel.SetActive(true);
    }

    public void bringDwonPanel()
    {

        exitPanel.SetActive(false);
    }

}
