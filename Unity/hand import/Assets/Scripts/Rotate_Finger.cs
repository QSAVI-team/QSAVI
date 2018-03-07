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
    public int potMax = 676;
    public int baudRate = 0;
    public string portName = "";
    public int Datapacket = 0;
   // public int potval = 0;  // only to use for testing

 
    // ------------------------- START -------------------------

    // Use this for initialization
    void Start()    {
        StartCoroutine("ProcessFingerRotation");
    }

    // ------------------------- GET SERIAL PORT DATA -------------------------
    
    IEnumerator ProcessFingerRotation()
    {
        yield return new WaitForSeconds(ArduinoInterface.SERIAL_PORT_REFRESH_PERIOD);
        //yield return baudRate;  // only use for testing 
        // Assume data will be valid
        bool isValidData = true;

     

        // If the data is valid, process it
            if (isValidData)  {
            float POTBASE = ArduinoInterface.indexbasePOT;
            float POTKUCKLE = ArduinoInterface.indexkucklePOT;
            float test = ArduinoInterface.xpalm;
            //float potval = float.Parse(vec3[Datapacket]);
            float fingerRotation = ((test-potMin) / potMax)*(fingMax - fingMin) +fingMin; //relate potentiometer reading to rotation, potentiometer goes from 0 - 676
            transform.eulerAngles = new Vector3(0, potMax, 0);// new Vector3(0, fingerRotation, 0);
                                                                      // figure out how to make fingerRotation to actually move the finger
            }

        StartCoroutine("ProcessFingerRotation");
    }
}


