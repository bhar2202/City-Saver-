using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public void goToInfo()
    {
        SceneManager.LoadScene(1);
    }
}
