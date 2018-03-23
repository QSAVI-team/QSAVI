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
    public int F = 0;
    public string portName = "";
    public int Datapacket = 0;
    float POTBASE = 0;
    float POTKNUCKLE=0;
    
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
            float POTBASE = ArduinoInterface.thumbCurlPOT;
            /*   some reason it doesnt like when you define POTBASE inside the if statement (debug log will read but wont send to transform 
            if (F == 0)
            {
                float POTBASE = ArduinoInterface.thumbCurlPOT;
                float POTKNUCKLE = ArduinoInterface.thumbLateralPOT;
                Debug.Log(POTBASE);
            }
            else if (F == 1)
            {
                float POTBASE = ArduinoInterface.indexbasePOT;
                float POTKNUCKLE = ArduinoInterface.indexknucklePOT;
            }
            else if (F == 2)
            {
                float POTBASE = ArduinoInterface.midbasePOT;
                float POTKNUCKLE = ArduinoInterface.midknucklePOT;
            }
               
               */
            

            //Debug.Log("Pot data aquired");
          

            transform.localRotation = Quaternion.Euler(0,POTBASE*360/750 , 0);


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


    