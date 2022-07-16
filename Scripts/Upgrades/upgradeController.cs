using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class upgradeController : MonoBehaviour
{

    public GameObject AchivementsPanel;
    public int money;
    public int costForSpeedUpgrade;
    public int costForFuelUpgrade;
    public int costForHoseUpgrade;
    public Text moneyText;
    public Text dayText;
    public Button wheelButton;
    public Button hoseButton;
    public Button fuelButton;
    public Sprite wheelIMG;
    public Sprite hoseIMG;
    public Sprite fuelIMG;
    public Sprite wheelUpgradeIMG;
    public Sprite hoseUpgradeIMG;
    public Sprite fuelUpgradeIMG;
    void Start()
    {
        AchivementsPanel.SetActive(false);

        //sets game data to defaults if new game
        if (PlayerPrefs.GetString("New Game").Equals("True"))
        {   
            PlayerPrefs.SetInt("Current Day", 0);
            PlayerPrefs.SetInt("Money", 0);
            PlayerPrefs.SetString("Speed Upgrade Bought", "False");
            PlayerPrefs.SetString("Fuel Upgrade Bought", "False");
            PlayerPrefs.SetString("Hose Upgrade Bought", "False");
           
        } else
        {
            //otherwise load up gamd data
            loadUpgradesAndMoney();
        }
    }

    void Update()
    {
        moneyText.text = "$" + PlayerPrefs.GetInt("Money");
        dayText.text = "Day " + PlayerPrefs.GetInt("Current Day");
    }

    private void loadUpgradesAndMoney()
    {
        //Debug.Log(PlayerPrefs.GetInt("Game Load"));

        string s = "/gamesave" + PlayerPrefs.GetInt("Game Load") + ".save";
        //check if load file exists 
        if (File.Exists(Application.persistentDataPath + s))
        {
            //loads file and game data
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + s, FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            //updates player money
            PlayerPrefs.SetInt("Money", save.money);
            money = save.money;

            //updates what upgrades have been purchaced
            PlayerPrefs.SetString("Speed Upgrade Bought", save.wheelUpgradeBought + "");
            PlayerPrefs.SetString("Fuel Upgrade Bought", save.fuelUpgradeBought + "");
            PlayerPrefs.SetString("Hose Upgrade Bought", save.hoseUpgradeBought + "");
            PlayerPrefs.SetInt("Current Day", save.currentDay);
            

            if (save.wheelUpgradeBought)
            {
                wheelButton.GetComponent<Image>().sprite = wheelUpgradeIMG;
            }

            if (save.hoseUpgradeBought)
            {
                hoseButton.GetComponent<Image>().sprite = hoseUpgradeIMG;
            }

            if (save.fuelUpgradeBought)
            {
                fuelButton.GetComponent<Image>().sprite = wheelUpgradeIMG;
            }
        }
    }

    //crete a save object to save the player and game data
    public Save CreateSaveObject()
    {
        Save save = new Save
        {

            // updates values in save object
            money = PlayerPrefs.GetInt("Money"),
            wheelUpgradeBought = PlayerPrefs.GetString("Speed Upgrade Bought").Equals("True"),
            hoseUpgradeBought = PlayerPrefs.GetString("Hose Upgrade Bought").Equals("True"),
            fuelUpgradeBought = PlayerPrefs.GetString("Fuel Upgrade Bought").Equals("True"),
            buildingsSaved = PlayerPrefs.GetInt("Buildings Saved"),
            currentDay = PlayerPrefs.GetInt("Current Day"),

            //sets other game data to the save object
            date = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
        };

        return save;
    }

    //saves the players position through seritalization 
    //int n refers to the world number
    public void SaveGame()
    {
        Save save = CreateSaveObject();

        BinaryFormatter bf = new BinaryFormatter();

        //right now it only saves to one spot, eventually I want to have 4 spots
        string s = "/gamesave" + PlayerPrefs.GetInt("Game Load") + ".save";
        print(s);
        FileStream file = File.Create(Application.persistentDataPath + s);
        bf.Serialize(file, save);
        file.Close();
        print("Game Saved");

    }

    public void goToHome()
    {
        SceneManager.LoadScene(0);
    }

    public void startNewDay()
    {
        SceneManager.LoadScene(3);
    }

    /// <summary>
    /// Buys the speed upgrade if the player has enough money
    /// </summary>
    public void buyMoreSpeed()
    {
        print(money + " " + costForSpeedUpgrade + " " + (money >= costForSpeedUpgrade) + " " + PlayerPrefs.GetString("Speed Upgrade Bought").Equals("False"));

        //check if the player has enough money and hasn't already bought the speed upgrade
        if (money >= costForSpeedUpgrade && PlayerPrefs.GetString("Speed Upgrade Bought").Equals("False"))
        {
            //decrease player money
            money -= costForSpeedUpgrade;



            //update button
            wheelButton.GetComponent<Image>().sprite = wheelUpgradeIMG;

            //update money and upgrade bought data
            PlayerPrefs.SetInt("Money", money);
            PlayerPrefs.SetString("Speed Upgrade Bought", "True");
            
        }

    }

    /// <summary>
    /// Buys the bigger hose upgrade if the player has enough money
    /// </summary>
    public void buyBiggerHose()
    {
        //check if the player has enough money and hasn't already bought the speed upgrade
        if (money >= costForHoseUpgrade && PlayerPrefs.GetString("Hose Upgrade Bought").Equals("False"))
        {
            //decrease player money
            money -= costForSpeedUpgrade;

            //update button
            hoseButton.GetComponent<Image>().sprite = hoseUpgradeIMG;

            //update money and upgrade bought data
            PlayerPrefs.SetInt("Money", money);
            PlayerPrefs.SetString("Hose Upgrade Bought", "True");
        }
    }

    /// <summary>
    /// Buys the bigger fuel tank upgrade if the player has enough money
    /// </summary>
    public void buyBiggerFuelTank()
    {
        //check if the player has enough money and hasn't already bought the speed upgrade
        if (money >= costForFuelUpgrade && PlayerPrefs.GetString("Fuel Upgrade Bought").Equals("False"))
        {
            //decrease player money
            money -= costForSpeedUpgrade;

            //update button
            fuelButton.GetComponent<Image>().sprite = fuelUpgradeIMG;

            //update money and upgrade bought data
            PlayerPrefs.SetInt("Money", money);
            PlayerPrefs.SetString("Fuel Upgrade Bought", "True");
        }
    }

    public void bringUpAchivements()
    {
        AchivementsPanel.SetActive(true);
    }

    public void closeAchivements()
    {
        AchivementsPanel.SetActive(false);
    }
}
