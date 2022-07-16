using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class GameControls : MonoBehaviour
{
    public GameObject player;
    public GameObject thirdPersonCamera;
    public GameObject firstPersonCamera;
    public GameObject wheel1;
    public GameObject wheel2;
    public GameObject wheel3;
    public GameObject wheel4;
    public GameObject wheel5;
    public GameObject wheel6;
    public GameObject hose;
    public Image gasBar;
    public Image speedBar;
    public Text buildingsSavedText;
    public Text timeText;
    public Rigidbody playerRB;
    public ParticleSystem water;

    public float velocity;
    public float maxSpeed = .2f;
    public const float maxVel = 100;
    public const float minVel = -10;
    public float Rspeed;
    public float accel;
    public float accelSpeed;
    public const float maxAccel = 0.5f;
    public const float minAccel = -0.5f;
    public float gameTime;
    public float gasConsumptionRate;
    public float hoseSize;
    

    private bool isMovingUp;
    private bool isMovingDown;
    private bool isMovingLeft;
    private bool isMovingRight;
    private bool isSprayingWater;
    private bool is3rdPerson;

    // Start is called before the first frame update
    void Start()
    {
        //IDEA - use playerPrefs to check whether or not the game is a new game or not


        //sets player position when they start a new game
        player.transform.position = new Vector3(-3.0f, 2.0f, 15.0f);
        player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        //set initial game data
        PlayerPrefs.SetInt("Buildings Saved", 0);
        PlayerPrefs.SetInt("Most Buildings Saved", 0);
        PlayerPrefs.SetFloat("Gas Used", 0.0f);
        PlayerPrefs.SetString("Game State", "Play");

        //sets all the moving booleans to false to keep the player stationary
        isMovingUp = false;
        isMovingDown = false;
        isMovingLeft = false;
        isMovingRight = false;

        //sets the varible that determines the state of the water hose
        isSprayingWater = false;

        //start timer (be mindful if load chages this)
        StartGameTimer();

        Debug.Log(PlayerPrefs.GetInt("Game Load"));

        print("Max speed before load" + maxSpeed);

        //loads the game based on what load button is pressed
        LoadGame(PlayerPrefs.GetInt("Game Load"));

        //apply unlocked upgrades
        if(PlayerPrefs.GetString("Speed Upgrade Bought").Equals("True"))
        {
            wheel1.transform.localScale = new Vector3(0.8f, 0.2f, 0.8f);
            wheel2.transform.localScale = new Vector3(0.8f, 0.2f, 0.8f);
            wheel3.transform.localScale = new Vector3(0.8f, 0.2f, 0.8f);
            wheel4.transform.localScale = new Vector3(0.8f, 0.2f, 0.8f);
            wheel5.transform.localScale = new Vector3(0.8f, 0.2f, 0.8f);
            wheel6.transform.localScale = new Vector3(0.8f, 0.2f, 0.8f);
            maxSpeed *= 2;
        }

        if (PlayerPrefs.GetString("Hose Upgrade Bought").Equals("True"))
        {
            hose.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
            water.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
        }

        if (PlayerPrefs.GetString("Fuel Upgrade Bought").Equals("True"))
        {
            gasConsumptionRate /= 2;
        }


            //set the camera to third person
            thirdPersonCamera.SetActive(true);
        firstPersonCamera.SetActive(false);
        is3rdPerson = true;

        print("Max speed " + maxSpeed);
    }

    //Switches the active camera from first person to third person and vice versa
    public void switchView()
    {
        if (!is3rdPerson)
        {
            thirdPersonCamera.SetActive(true);
            firstPersonCamera.SetActive(false);
            is3rdPerson = true;
        }
        else
        {
            thirdPersonCamera.SetActive(false);
            firstPersonCamera.SetActive(true);
            is3rdPerson = false;
        }
        
    }


    //Starts the game timer 
    private void StartGameTimer()
    {
        if (timeText.IsActive())
        {
            InvokeRepeating("UpdateTimer", 0.0f, 0.001f);
        }
    }

    private void UpdateTimer()
    {
        if (timeText.IsActive() && gameTime >= 0.0f)
        {
            gameTime -= Time.deltaTime;
            string minutes = Mathf.Floor(gameTime / 60).ToString("00");
            string seconds = (gameTime % 60).ToString("00");
            timeText.text = minutes + ":" + seconds ;
        }
        
        //game stops when time ends
        if(gameTime <= 0.0f && Time.timeScale != 0.0f)
        {

            Time.timeScale = 0.0f;

            endGame();
        }
    }

    private void FixedUpdate()
    {

        //updates the velocity of the player in respects to aceeleration
        updateAccelAndVel();

        ////move the player along the x axis if input is detected and gas is still in the tank
        //float moveY =  velocity * Time.deltaTime * -1;

       

        if(PlayerPrefs.GetFloat("Gas Used") < 1.0f &&( isMovingUp || isMovingDown || velocity != 0))
        {
            //sets the y velocity for given boolean move variable
            if (isMovingDown)
            {
                velocity = -maxSpeed;
            } else if (isMovingUp)
            {
                velocity = maxSpeed;
            }


            ////moves player along the right (y) axis
            //player.transform.position += player.transform.right * moveY;
            //playerRB.AddForce(new Vector3(0.0f, 0.0f, velocity));
            player.transform.Translate(new Vector3(velocity, 0.0f, 0.0f));

            print("gas consumption rate " + gasConsumptionRate);

            //player uses gas over time when moving 
            PlayerPrefs.SetFloat("Gas Used", PlayerPrefs.GetFloat("Gas Used") + gasConsumptionRate);
            
        }

        //move the player along the y axis if input is detected
        float turnY = Input.GetAxis("Horizontal") ;
        if (isMovingLeft)
        {
            turnY = -1;
        }
        else if (isMovingRight)
        {
            turnY = 1;
        }
        if (turnY != 0)
        {
            if (turnY < 0) 
            {
                player.transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f) * Rspeed * Time.deltaTime * -1,Space.Self);
            } else
            {
                player.transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f) * Rspeed * Time.deltaTime,Space.Self);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            isSprayingWater = true;
        } else
        {
            isSprayingWater = false;
        }

        
    }

    

    public void startSprayingWater()
    {
        isSprayingWater = true;
    }

    public void stopSprayingWater()
    {
        isSprayingWater = false;
    }



    public void moveUp()
    {
        isMovingUp = true;
    }

    public void stopMovingUp()
    {
        isMovingUp = false;
    }

    public void moveDown()
    {
        isMovingDown = true;
    }

    public void stopMovingDown()
    {
        isMovingDown = false;
    }

    public void moveLeft()
    {
        isMovingLeft = true;
    }

    public void stopMovingLeft()
    {
        isMovingLeft = false;
    }

    public void moveRight()
    {
        isMovingRight = true;
    }

    public void stopMovingRight()
    {
        isMovingRight = false;
    }


    void updateAccelAndVel()
    {
        //TODO ensure that the input can be done by a mobile device

        velocity = Input.GetAxis("Vertical") * 25 * Time.deltaTime;

        
    }

    // Update is called once per frame
    void Update()
    {

        //updates the UI elemets when the game is running

        gasBar.fillAmount = 1.0f - PlayerPrefs.GetFloat("Gas Used");

        //TODO create and if statement for nevative velocity
        speedBar.fillAmount = Mathf.Abs( velocity * 2);

        buildingsSavedText.text = "Buildings Saved: " + PlayerPrefs.GetInt("Buildings Saved");

        //updates time text




    }

    //crete a save object to save the player and game data
    public Save CreateSaveObject()
    {
        Save save = new Save
        {

            //sets the player data to the save object
            playerXPos = player.transform.position.x,
            playerZPos = player.transform.position.z,
            //save.playerYRotation = .transform.rotation.y;

            
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

    public void LoadGame(int n)
    {
        ////add in playerPrefs so that the game only loads when the load button is pressed
        //if(PlayerPrefs.GetString("New Game").Equals("false")) { 

        //save the game load (game #) in the player prefs
        PlayerPrefs.SetInt("Game Load", n);

        string s = "/gamesave" + n + ".save";
        //check if load file exists 
        if (File.Exists(Application.persistentDataPath + s))
        {
            //loads file and game data
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + s,FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            //updates player data
            player.transform.position = new Vector3(save.playerXPos, 2.0f, save.playerZPos);


            //TODO - update game data
            PlayerPrefs.SetInt("Money", save.money);
            PlayerPrefs.SetFloat("Hose Size", save.hoseSize);
            //PlayerPrefs.SetFloat("Max Speed", save.maxSpeed);
            PlayerPrefs.SetFloat("Gas Used", save.GasUsed);
            PlayerPrefs.SetFloat("Gas Consumption Rate", save.gasConsumptionRate);

            
            hoseSize = save.hoseSize;
            gasConsumptionRate = save.gasConsumptionRate;

            print("Game Loaded");

        } else
        {
            print("No Game Saved");
        }
    }
    public void endGame()
    {
        Debug.Log("end save: " + PlayerPrefs.GetInt("Game Load"));

        //increase the current day by 1
        PlayerPrefs.SetInt("Current Day", PlayerPrefs.GetInt("Current Day") + 1);


        Debug.Log("Day " + PlayerPrefs.GetInt("Current Day"));
        //calculate money made and add it to player money
        calculateMoney();

        //set the gamestate to "End"
        PlayerPrefs.SetString("Game State", "End");

        //save the game data
        SaveGame();
    }

    public void calculateMoney()
    {
        //add money based on the amount of buildings saved
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + PlayerPrefs.GetInt("Buildings Saved") * 5);
    }

}

