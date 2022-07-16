using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    public GameObject loadPanel;
    public GameObject wantToDeletePanel;
    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;
    public Text deleteText;
    public int gameLoad;

    // Start is called before the first frame update
    void Start()
    {
        //deactivates the load and delete panels
        loadPanel.SetActive(false);
        wantToDeletePanel.SetActive(false);
        updateLoadInfo();
    }

    //call this method when one of the game load buttons are pressed

   
    public void loadGameButtonPressed(int n)
    {
        PlayerPrefs.SetInt("Game Load", n);
    }

    
    public void updateLoadInfo()
    {
        for(int i = 1; i <= 4; i++)
        {
            String s = "/gamesave" + i + ".save";
            if (File.Exists(Application.persistentDataPath + s))
            {
                //loads file and game data
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + s, FileMode.Open);
                Save save = (Save)bf.Deserialize(file);
                file.Close();

                updateText("[" + save.date + "]\n" + "Day " + save.currentDay, i);
            } else
            {
                updateText("[Empty]", i);
            }
        }
    }

    private void updateText(string s, int n)
    {
        //set the date of the game loads on the text below the load buttons
        
            switch (n)
            {
                case 1:
                    text1.text = s;
                    break;
                case 2:
                    text2.text = s;
                    break;
                case 3:
                    text3.text = s;
                    break;
                case 4:
                    text4.text = s;
                    break;
            }
        
    }

    public void BringUpLoadPanel()
    {
        loadPanel.SetActive(true);
    }

    public void CloseLoadPanel()
    {
        loadPanel.SetActive(false);
    }

    //brings up the delete panel with specified file to delete 
    //
    // n - file number in which to ask to delete
    public void BringUpDeletePanel(int n)
    {
        wantToDeletePanel.SetActive(true);
        gameLoad = n;
        deleteText.text = "Are you Sure you want to delete file " + n + "?";
    }

    public void CloseDeletePanel()
    {
        wantToDeletePanel.SetActive(false);
    }

    //crete a save object to save the player and game data
    public Save CreateDefaultSaveObject()
    {
        Save save = new Save
        {

            // retests game data in file
            money = 0,
            wheelUpgradeBought = false,
            hoseUpgradeBought = false,
            fuelUpgradeBought = false,

            buildingsSaved = 0,

            date = "Empty"
        };

        return save;
    }
 
    //int n refers to the world number
    public void restGameData()
    {
        Save save = CreateDefaultSaveObject();

        BinaryFormatter bf = new BinaryFormatter();

        //right now it only saves to one spot, eventually I want to have 4 spots
        string s = "/gamesave" + gameLoad + ".save";
        print(s);
        FileStream file = File.Create(Application.persistentDataPath + s);
        bf.Serialize(file, save);
        file.Close();
        print("Game Saved");

    }
}
