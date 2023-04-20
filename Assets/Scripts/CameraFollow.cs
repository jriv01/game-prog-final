using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerPosition;
    public int zDistance = 10;

    // Update is called once per frame
    void Update()
    {
        GetComponent<Transform>().position = new Vector3(playerPosition.position.x,
                                                         playerPosition.position.y,
                                                         -zDistance);        
    }
}
