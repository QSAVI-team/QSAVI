using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;



public class Rotate_Finger : MonoBehaviour{   
   // ------------------------- TIMING -------------------------

    //float serialPortRefreshPeriod;
    //float serialPortRequestDelayPeriod;
    public float fingMin = 0f;
    public float fingMax = 0f;
    public int potMin = 0;
    public int potMax = 0;
    public int F = 0;
    public int Base0knuckle1 = 0;
    float[,] POT = new float [2,2];  //[3,2]
    float test = 0;

    // public int potval = 0;  // only to use for testing


    // ------------------------- START -------------------------

    // Use this for initialization
    void Start()    {
        StartCoroutine("ProcessFingerRotation");
    }

    // ------------------------- GET ArduinoInterface.cs DATA -------------------------
    
    IEnumerator ProcessFingerRotation()
    {
        yield return new WaitForSeconds(ArduinoInterface.SERIAL_PORT_REFRESH_PERIOD);
        //yield return baudRate;  // only use for testing 
        // Assume data will be valid
        bool isValidData = true;

        // If the data is valid, process it
        if (isValidData) {
            float TC = ArduinoInterface.thumbcurl;
            float TL = ArduinoInterface.thumblateral;
            float IB = ArduinoInterface.index;
            float IK = ArduinoInterface.mid;
            //float MB = ArduinoInterface.midbasePOT;
            // float MK = ArduinoInterface.midknucklePOT;

            float[,] POT = new float[2, 2] { { TC, TL }, { IB, IK } };//, { MB, MK } };
          
          
            if (Base0knuckle1 == 0)
            { potMax = 750;
            }
            else
            { potMax = 500;// not sure what this value should be 
            }


            //Debug.Log("Pot data aquired");
              //test = TC; transform.localRotation = Quaternion.Euler(0,test*360/potMax , 0);

            transform.localRotation = Quaternion.Euler(0,POT[F,Base0knuckle1]*360/potMax , 0);


            // float fingerRotation = ((test-potMin) / potMax)*(fingMax - fingMin) +fingMin; //relate potentiometer reading to rotation, potentiometer goes from 0 - 676
            //transform.eulerAngles = new Vector3(x, y,z);// new Vector3(0, fingerRotation, 0);

        }

        StartCoroutine("ProcessFingerRotation");
    }

    
    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, 0.1f * transform.right, Color.red);
        Debug.DrawRay(transform.position, 0.1f * transform.up, Color.green);
        Debug.DrawRay(transform.position, 0.1f * transform.forward, Color.blue);

    }
}


    