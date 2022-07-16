using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StatController : MonoBehaviour
{
    public void goToHome()
    {
        SceneManager.LoadScene(0);
    }

    public void goToUpgrades()
    {
        SceneManager.LoadScene(4);
    }
}
