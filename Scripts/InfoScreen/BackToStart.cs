using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToStart : MonoBehaviour
{


    // Start is called before the first frame update
    public void goToStart()
    {
        SceneManager.LoadScene(0);
    }
}
