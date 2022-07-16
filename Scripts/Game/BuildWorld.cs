
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWorld : MonoBehaviour
{
    public GameObject IRoad;
    public GameObject TRoad;
    public GameObject LRoad;
    public GameObject FourRoad;
    public GameObject[,] roadGrid;

    // Start is called before the first frame update
    void Start()
    {   //roadGrid = new GameObject[5,5];
        //generateRoadGrid();
        
    }


    /*
     *  Generating Random worlds has some rotation issues 
     *  so it's commented out
     * 
     */

    //generates the grid of roadpieces to create a road for the world
    //void generateRoadGrid()
    //{
    //    //note: roadGrid.length is the total number of elements in the grid
    //    for(int i = 0; i < 5; i++)
    //    {
    //        for(int j = 0; j < 5; j++)
    //        {
                    
    //            roadGrid[i, j] = pickRandomRoad();
                
    //            //test values so grid fits world
    //          Instantiate(roadGrid[i, j], new Vector3(0.0f + 30.0f * i, 1.5f, 0.0f + 30.0f * j), Quaternion.identity);
    //        }
    //    }
    //}

    ////picks a random road from the 4 road pieces
    //GameObject pickRandomRoad()
    //{
    //    int n = Random.Range(1, 4);
    //    switch (n)
    //    {
    //        case 1: return IRoad;
    //        case 2: return TRoad;
    //        case 3: return LRoad;
    //        case 4: return FourRoad;
    //        default: return null;
    //    }
    //}

    ////checks if the road is in a valid spot
    //void checkRoadPlacement()
    //{

    //}

    ////cretes a clone of the road objects at each grid element
    //void instantiateRoad()
    //{

    //}
}
