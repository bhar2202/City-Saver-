using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelAndSaveController : MonoBehaviour
{
    public GameObject PausedButtonPanel;
    public GameObject PausedPanel;
    public GameObject PausedSettingsPanel;
    public GameObject wantToSavePanel;
    public GameObject SaveToFilePanel;
    public GameObject StatPanel;
    public GameObject gameEndPanel;

    // Start is called before the first frame update (when the game loads)
    void Start()
    {
        PausedButtonPanel.SetActive(true);
        PausedPanel.SetActive(false);
        PausedSettingsPanel.SetActive(false);
        wantToSavePanel.SetActive(false);
        SaveToFilePanel.SetActive(false);
        StatPanel.SetActive(true);
        gameEndPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
    
    void Update()
    {
        //Debug.Log(PlayerPrefs.GetString("Game State"));
        //Debug.Log(PlayerPrefs.GetString("Game State").Equals("End"));
        //check if the game has ended then call the bringUpStatPanelMethod
        if (PlayerPrefs.GetString("Game State").Equals("End"))
        {
            bringUpEndGamePanel();
        }
    }

    public void pausedButtonPressed()
    {
        PausedButtonPanel.SetActive(false);
        PausedPanel.SetActive(true);

        //freezes the game
        Time.timeScale = 0.0f;
    }

    public void continueButtonPressed()
    {
        PausedButtonPanel.SetActive(true);
        PausedPanel.SetActive(false);

        //unfreezes the game
        Time.timeScale = 1.0f;
    }

    public void settingsButtonPressed()
    {
        PausedSettingsPanel.SetActive(true);
        PausedPanel.SetActive(false);
    }

    public void settingsBackButtonPressed()
    {
        PausedSettingsPanel.SetActive(false);
        PausedPanel.SetActive(true);
    }

    public void BringUpWantToSave()
    {
        wantToSavePanel.SetActive(true);
    }

    public void CloseWantToSave()
    {
        wantToSavePanel.SetActive(false);
    }

    public void BringUpSaveToFile()
    {
        SaveToFilePanel.SetActive(true);
    }

    public void CloseSaveToFile()
    {
        SaveToFilePanel.SetActive(false);
    }


    

    public void goToStats()
    {
        SceneManager.LoadScene(5);
        Time.timeScale = 1.0f;
    }

    public void goToUpgrades()
    {
        SceneManager.LoadScene(4);
        Time.timeScale = 1.0f;
    }

    public void BackToStart()
    {
        //moves to the start screen
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    public void bringUpEndGamePanel()
    {
        //TODO Bring up the end game Panel
        gameEndPanel.SetActive(true);
        Time.timeScale = 0.0f;
    }



}
