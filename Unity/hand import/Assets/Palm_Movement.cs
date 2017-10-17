using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Palm_Movement : MonoBehaviour {
    public float speed;

    private float amountToMove;

    SerialPort sp = new SerialPort("COM3", 115200);
	// Use this for initialization
	void Start ()
    {
        sp.Open();
        sp.ReadTimeout = 1;
	}
	
	// Update is called once per frame
	void Update () {
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
	}

    void MoveObject ( int x, int y, int z)
        transform.Translate(Vector3.left * amountToMove, Space.World);
        transform.Translate(Vector3.);

}
