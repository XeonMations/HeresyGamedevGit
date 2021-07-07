using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
this script is used to control the position of the camera, eliminating any jittering that comes with the
use of a rigidbody player controller.
*/

public class ParkourMoveCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraPosition;

    private void Update()
    {
        //move the position of the camera to the specified position on the player
        transform.position = cameraPosition.position;
    }
}
