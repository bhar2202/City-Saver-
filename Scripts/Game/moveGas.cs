using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveGas : MonoBehaviour
{
    public float verticalSpeed;
    public float spinSpeed;
    public float spinPos;
    public float gasAmt = 0.2f;

    // Update is called once per frame
    void Update()
    {
        //move the gas tank up and down certain heights and constantly turn the gas clockwise

        if(gameObject.transform.position.y >= 8.0f || gameObject.transform.position.y <= 2.0f)
        {
            verticalSpeed *= -1;
        }
        spinPos += spinSpeed;

        gameObject.transform.position += new Vector3(0.0f, verticalSpeed, 0.0f);
        gameObject.transform.rotation = Quaternion.Euler(0.0f, spinPos + spinSpeed, 0.0f);
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "player")
        {
            float gasUsed = PlayerPrefs.GetFloat("Gas Used") - gasAmt;
            if(gasUsed < 0.0f)
            {
                gasUsed = 0.0f;
            }
            PlayerPrefs.SetFloat("Gas Used", gasUsed);
            Destroy(gameObject);
        }
    }

}
