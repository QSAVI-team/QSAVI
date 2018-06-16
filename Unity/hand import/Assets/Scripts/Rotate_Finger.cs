using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;



public class Rotate_Finger : MonoBehaviour{   
   // ------------------------- TIMING -------------------------

    //float serialPortRefreshPeriod;
    //float serialPortRequestDelayPeriod;
   
    public int factor = 1;
   // public int finger = 0;
    //public int knuckle = 0;
    public int offset = -600;
    public int Base0knuckle1 = 0;
    public int t = 0;
    
    public int i = 0;
   
    public int m = 0;
    
    public int p = 0;
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
        /*
        bool isValidData = true;

        // If the data is valid, process it
        if (isValidData) {
          
            if (Base0knuckle1 == 0)
            { factor = 750;
            }
            else
            { factor = 500;// not sure what this value should be 
            }
            */

        //Debug.Log("Pot data aquired");
        //test = TC; transform.localRotation = Quaternion.Euler(0,test*360/potMax , 0);
        float thu = ArduinoInterface.thumb[Base0knuckle1] * t ;
        float ind = ArduinoInterface.index[Base0knuckle1] * i;
        float mid = ArduinoInterface.middle[Base0knuckle1] * m;
        float pin = ArduinoInterface.pinky[Base0knuckle1] * p;
        

        transform.localRotation = Quaternion.Euler(0, ( thu+ind+mid+pin )* factor +offset, 0);
            Debug.Log("blah");
            //Debug.Log(ArduinoInterface.fingers[1, 0]);

            // float fingerRotation = ((test-potMin) / potMax)*(fingMax - fingMin) +fingMin; //relate potentiometer reading to rotation, potentiometer goes from 0 - 676
            //transform.eulerAngles = new Vector3(x, y,z);// new Vector3(0, fingerRotation, 0);

        

        StartCoroutine("ProcessFingerRotation");
    }

    
    // Update is called once per frame
    void Update()
    {
      //  Debug.DrawRay(transform.position, 0.1f * transform.right, Color.red);
       // Debug.DrawRay(transform.position, 0.1f * transform.up, Color.green);
        //Debug.DrawRay(transform.position, 0.1f * transform.forward, Color.blue);

    }
}


    