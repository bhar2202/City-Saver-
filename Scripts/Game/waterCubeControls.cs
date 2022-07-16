using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterCubeControls : MonoBehaviour
{

    public GameObject waterHose;

    /*
     * Process of how water cube will work
     * 
     * 1. when spacebar is pressed instantiate clone at the water hose (do this in gameController script)
     * 2. add force to water cube in direction water cube is facing
     * 3. delete water cube if collision occurs with building or ground
     * 
     * 
     * In addition:
     *  - change water size if the hose size upgade is purchased
     *  
     */

    // Start is called before the first frame update
    void Start()
    {
        //move to water hose
        gameObject.transform.position = waterHose.transform.position;
        gameObject.transform.rotation = waterHose.transform.rotation;

        //add force to gameObject 
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(1.0f, 1.0f, 0.0f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        //destroy this object if collision occurs with building or ground
        if(collision.collider.tag == "Building" )
        {
            Destroy(gameObject);
        }
    }
}
