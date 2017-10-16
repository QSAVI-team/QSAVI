using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palm_Movement : MonoBehaviour {

    // Declares two 3D vectors for the arduino's position and rotation
    private Vector3 ArduinoPosition;
    private Vector3 ArduinoRotation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // Moves the object to the position and rotation of the arduino
        transform.Translate(ArduinoPosition - transform.localPosition);
        transform.Rotate(ArduinoRotation - transform.localEulerAngles);
	}
}
