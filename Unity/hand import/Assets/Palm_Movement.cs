using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Palm_Movement : MonoBehaviour {
    public float speed;

<<<<<<< HEAD
    private float amountToMove;

    SerialPort sp = new SerialPort("COM3", 115200);
=======
    // Declares two 3D vectors for the arduino's position and rotation
    private Vector3 ArduinoPosition;
    private Vector3 ArduinoRotation;

>>>>>>> ad1d9257815c57021b95892906ceb470ecf77086
	// Use this for initialization
	void Start ()
    {
        sp.Open();
        sp.ReadTimeout = 1;
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
        amountToMove = speed * Time.deltaTime;
        if (sp.IsOpen)
        {
            try
            { MoveObject(sp.ReadByte()); // cant read byte by byte??? have to read vector
               
                print(sp.ReadByte);
            }
            catch(System.Exception)

            {

            }
        }
=======

        // Moves the object to the position and rotation of the arduino
        transform.Translate(ArduinoPosition - transform.localPosition);
        transform.Rotate(ArduinoRotation - transform.localEulerAngles);
>>>>>>> ad1d9257815c57021b95892906ceb470ecf77086
	}

    void MoveObject ( int x, int y, int z)
        transform.Translate(Vector3.left * amountToMove, Space.World);
        transform.Translate(Vector3.);

}
