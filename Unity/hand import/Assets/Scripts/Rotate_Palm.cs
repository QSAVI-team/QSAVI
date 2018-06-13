using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Palm : MonoBehaviour {

	// ------------------------- ORIENTAION -------------------------

	public enum OrientationMode {
		X_Y_Z,
		X_Z_Y,
		Y_X_Z,
		Y_Z_X,
		Z_X_Y,
		Z_Y_X
	};
	public OrientationMode orientaionMode = OrientationMode.X_Y_Z;

	public bool negativeX = false;
	public bool negativeY = false;
	public bool negativeZ = false;
	public bool negativeW = false;

	public bool invertX = false;
	public bool invertY = false;
	public bool invertZ = false;

	public Space rotationSpace = Space.Self;

	public Vector3 offsetRotation = new Vector3(90f, 0f, 90f);
	public Vector3 additionalRotaion = new Vector3(0f, 0f, 0f);
	Quaternion centerQuaternion = Quaternion.identity;
	public bool setCenterRotationNow = false;
    public int palm_0_elbow_1 = 0;


    float[] caliRotation = { 0, 0, 0 };

	// position pointing real hand down at your side
	float xo = 85.248f;
	float yo = -52.267f;
	float zo = -53.411f;

	// ------------------------- START -------------------------

	// horizontal hand position, want to update later to being down
	// Use this for initialization
	void Start () {
		StartCoroutine ("ProcessRotation");
	}

	// ------------------------- GET SERIAL PORT DATA -------------------------

	IEnumerator ProcessRotation() {

		yield return new WaitForSeconds (ArduinoInterface.SERIAL_PORT_REFRESH_PERIOD);

        float w = ArduinoInterface.w[palm_0_elbow_1] * (negativeW ? -1f : 1f);
        float x = ArduinoInterface.x[palm_0_elbow_1] * (negativeX ? -1f : 1f);
        float y = ArduinoInterface.y[palm_0_elbow_1] * (negativeY ? -1f : 1f);
        float z = ArduinoInterface.z[palm_0_elbow_1] * (negativeZ ? -1f : 1f);
       
       /////////////////////////////////////////
          // Construct input quaternion from read data
        Quaternion inputQuaternion = Quaternion.identity;

		if(orientaionMode == OrientationMode.X_Y_Z) {
			inputQuaternion = new Quaternion (x, y, z, w);
		} else if(orientaionMode == OrientationMode.X_Z_Y) {
			inputQuaternion = new Quaternion (x, z, y, w);
		} else if(orientaionMode == OrientationMode.Y_X_Z) {
			inputQuaternion = new Quaternion (y, x, z, w);
		} else if(orientaionMode == OrientationMode.Y_Z_X) {
			inputQuaternion = new Quaternion (y, z, x, w);
		} else if(orientaionMode == OrientationMode.Z_X_Y) {
			inputQuaternion = new Quaternion (z, x, y, w);
		} else if(orientaionMode == OrientationMode.Z_Y_X) {
			inputQuaternion = new Quaternion (z, y, x, w);
		}

		// TODO: flip the axis of the rotaion to match the sensor
		/*
		// Convert to euler angles and flip axis
		Vector3 inputAngles = inputQuaternion.eulerAngles;
		Vector3 resultAngles = new Vector3 (inputAngles.y, inputAngles.z, inputAngles.x);

		// Construct resultant quaternion
		Quaternion resultQuaternion = Quaternion.Euler (resultAngles);
		*/

		if (setCenterRotationNow) {
			setCenterRotationNow = false;
            centerQuaternion = inputQuaternion;
			centerQuaternion = Quaternion.Inverse (centerQuaternion);
		}

		Quaternion offsetQuaternion = Quaternion.Euler (offsetRotation);
        //inputQuaternion = inputQuaternion * offsetQuaternion;

        // Set the final orientaion
       
        transform.localRotation =  offsetQuaternion*(centerQuaternion* inputQuaternion) ;
      //  transform.Rotate(offsetRotation, rotationSpace);


        /*
		transform.RotateAround (transform.right, Mathf.Deg2Rad * additionalRotaion.x);
		transform.RotateAround (transform.up, Mathf.Deg2Rad * additionalRotaion.y);
		transform.RotateAround (transform.forward, Mathf.Deg2Rad * additionalRotaion.z);
		*/

        Vector3 currentEulerAngles = transform.localEulerAngles;
		//Vector3 currentEulerAngles = transform.InverseTransformDirection(inputQuaternion.eulerAngles);
		if (invertX) {
			currentEulerAngles.x = -currentEulerAngles.x;
			//transform.RotateAround (transform.right, Mathf.PI);
		}
		if (invertY) {
			currentEulerAngles.y = -currentEulerAngles.y;
			//transform.RotateAround (transform.up, Mathf.PI);
		}
		if (invertZ) {
			currentEulerAngles.z = -currentEulerAngles.z;
			//transform.RotateAround (transform.forward, Mathf.PI);
		}
		transform.localEulerAngles = currentEulerAngles;

	

	// Continue to get more data
	StartCoroutine ("ProcessRotation");

}

	// Update is called once per frame
	void Update ()	{
	
		Debug.DrawRay (transform.position, 0.1f * transform.right, Color.red);
		Debug.DrawRay (transform.position, 0.1f * transform.up, Color.green);
		Debug.DrawRay (transform.position, 0.1f * transform.forward, Color.blue);

	}

}
