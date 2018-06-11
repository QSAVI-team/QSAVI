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

        float w = ArduinoInterface.wpalm * (negativeW ? -1f : 1f);
        float x = ArduinoInterface.xpalm * (negativeX ? -1f : 1f);
        float y = ArduinoInterface.ypalm * (negativeY ? -1f : 1f);
        float z = ArduinoInterface.zpalm * (negativeZ ? -1f : 1f);

  
        /*
		float w = float.Parse (vec3 [0]) * (negativeW ? -1f : 1f);
        float x = float.Parse (vec3 [1]) * (negativeX ? -1f : 1f);
		float y = float.Parse (vec3 [2]) * (negativeY ? -1f : 1f);
		float z = float.Parse (vec3 [3]) * (negativeZ ? -1f : 1f);
        
        ////// new Quaternion matrix/////
       
        double theta = Math.Acos(w) * 2;

        // quaternion rotation matrix multiply quaternion by this matrix to get out new oriented quaternion 
        float[,] R = new float[,]{ { 2*((float)theta* (float)theta + x*x)-1,2*(x*y-w*z), 2*(x*x+w*y) },   
                              {2*(x*y+w*z) ,2*(w*w+y*y)-1 , 2*(y*z-w*x)},
                            {2*(x*y-w*y), 2*(y*z+w*x) ,2*(w*w +z*z)} };

        */////////////////////////////////////////
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
		transform.localRotation =  centerQuaternion* inputQuaternion ;
       		transform.Rotate (offsetRotation, rotationSpace);

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

		/*
		if (vec3 [0] != "" && vec3 [1] != "" && vec3 [2] != "" && vec3 [3] != "") { //check that no values are blank
			if (vec3 [0] == "2") { // delimiter representing calibration
				caliRotation [0] = float.Parse (vec3 [1]);
				caliRotation [1] = float.Parse (vec3 [2]);
				caliRotation [2] = float.Parse (vec3 [3]);

				Debug.Log (caliRotation);

				transform.localEulerAngles = new Vector3 (xo, yo, zo); // set to original hand rotations if your attaching it to forearm set it to 0,0,0,
				// What we had before:
         *      //transform.localEulerAngles=new Vector3(
		//float.Parse(vec3[1]) - caliRotation[0],
		//float.Parse(vec3[2]) - caliRotation[1],
		//float.Parse(vec3[3]) - caliRotation[2]);

		} else if (vec3 [0] == "1") { // delimiter representing rotation

			x = float.Parse (vec3 [1]) - caliRotation [0] + xo;
			y = float.Parse (vec3 [2]) - caliRotation [1] + yo;
			z = float.Parse (vec3 [3]) - caliRotation [2] + zo;
			//if (x > 360 || y > 360 || z > 360) {
			//    Debug.Log("error euler larger than 360");
			//    x = 359; y = 359; z = 359;
			//}
			// transform.localEulerAngles = new Vector3(x, y, z);                        //parse each value from a string to a float

			Quaternion currentRotation = Quaternion.Euler (x, y, z);
			transform.localRotation = currentRotation;
		}
		//transform.Rotate()
		Debug.Log (vec3);

		//else (vec3[0] == "0") { // delimiter representing translation
		//    transform.Translate(
		//        float.Parse(vec3[0]) - lastPosition[0], // - caliRotation[0],
		//            float.Parse(vec3[1]) - lastPosition[1], //- caliRotation[1],
		//            float.Parse(vec3[2]) - lastPosition[2], //- caliRotation[2],
		//            Space.Self
		//            );
		//    lastPosition[0] = float.Parse(vec3[0]); //set new values for the most recent rotation
		//    lastPosition[1] = float.Parse(vec3[1]);
		//    lastPosition[2] = float.Parse(vec3[2]);
		//    sp.BaseStream.Flush();
		//}

	}
	*/

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
