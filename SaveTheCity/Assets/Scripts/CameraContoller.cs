using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    
     
    public GameObject player;
    Vector3 initialCameraPos;

    private float mouseSensitivy = 1.0f;
    private Camera _mainCamera;

    public float xPos;

    // Start is called before the first frame update
    void Start()
    {
       initialCameraPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        transform.position = player.transform.position + initialCameraPos;


        xPos = Input.GetAxis("Mouse X");

        Vector3 rotationLR = transform.localEulerAngles;
        rotationLR.y += xPos * mouseSensitivy;
        transform.rotation = Quaternion.AngleAxis(rotationLR.y, Vector3.up);



    }

   
}
