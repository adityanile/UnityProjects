using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionMarker : MonoBehaviour
{
    public GameObject positionMarker;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SpawnMarker();
        }

    }

    void SpawnMarker()
    { 
      Instantiate(positionMarker, player.transform.position, positionMarker.transform.rotation);
    }

}
