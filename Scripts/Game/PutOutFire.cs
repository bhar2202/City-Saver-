using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PutOutFire : MonoBehaviour
{

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Water")
    //    {
    //        PlayerPrefs.SetInt("Buildings Saved", PlayerPrefs.GetInt("Buildings Saved") + 1);
    //        Destroy(gameObject);
    //    }
    //}

    private void OnParticleCollision(GameObject other)
    {
        print("water Collision");
        if(other.tag == "Water")
        {
            
            PlayerPrefs.SetInt("Buildings Saved", PlayerPrefs.GetInt("Buildings Saved") + 1);
            Destroy(gameObject);
        }
    }
}
