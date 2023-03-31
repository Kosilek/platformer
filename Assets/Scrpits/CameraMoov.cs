using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoov : MonoBehaviour
{
    public Transform trackingObject;

    private void Update()
    {
        transform.position = new Vector3(trackingObject.position.x, trackingObject.position.y, transform.position.z);
    }
}