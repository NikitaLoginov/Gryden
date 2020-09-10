using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CoordinatesFinderTest : MonoBehaviour
{
    GameObject[] _obstacles;
    void Start()
    {
        _obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        for (int i = 0; i < _obstacles.Length; i++)
        {
            Debug.Log("Obstacle coordinates: "+_obstacles[i].transform.position);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
