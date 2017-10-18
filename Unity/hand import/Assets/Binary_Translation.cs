using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports; 

public class Binary_Translation : MonoBehaviour {

    public float speed;

    private float amountToMove;

    SerialPort sp = new SerialPort("COM3", 115200);
	// Use this for initialization
	void Start () {
        sp.Open();
        sp.ReadTimeout = 1;

	}
	
	// Update is called once per frame
	void Update () {
        amountToMove = speed * Time.deltaTime;
        if (sp.IsOpen)
        {
            try
            {
                MoveObject(sp.ReadByte()); // cant read byte by byte??? have to read vector

                print(sp.ReadByte());
            }
            catch (System.Exception)

            {

            }
        }

        Debug.Log(sp.ReadByte());


    }

    void MoveObject(int Direction)

    { if (Direction ==1)
        {
            transform.Translate(Vector3.left * amountToMove, Space.World);
        }
    if (Direction ==2 )
        {
            transform.Translate(Vector3.right * amountToMove, Space.World);
        }

    }
}
