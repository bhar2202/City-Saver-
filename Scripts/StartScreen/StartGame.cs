using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGame : MonoBehaviour
{
    public GameObject loadFilePanel;

    

    public void startGame(Boolean isNew)
    {
        
        loadFilePanel.SetActive(false);
        PlayerPrefs.SetString("New Game", isNew + "");
        
        SceneManager.LoadScene(4);

        //SceneManager.LoadScene(3, LoadSceneMode.Single);
        //StartCoroutine(LoadNewScene());
    }

    
    

    
}
