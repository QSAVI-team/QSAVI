using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;



public class Rotate_Finger : MonoBehaviour
{   // ------------------------- SERIAL PORT -------------------------

    //SerialPort sp = new SerialPort(portName,baudRate); // fill in com port
    SerialPort serialPort;
    // ------------------------- TIMING -------------------------

    float serialPortRefreshPeriod;
    float serialPortRequestDelayPeriod;
    public float min = 0;
    public float max = 0;
    public int potMin = 0;
    public int potMax = 676;
    public int baudRate = 0;
    public string portName = "";


    // Use this for initialization

    float range = max - min;


    // ------------------------- START -------------------------

    // horizontal hand position, want to update later to being down
    // Use this for initialization
    void Start()
    {
        StartCoroutine("ProcessRotation");
    }

    // ------------------------- GET SERIAL PORT DATA -------------------------

    IEnumerator ProcessFingerRotation()
    {

        yield return new WaitForSeconds(ArduinoInterface.SERIAL_PORT_REFRESH_PERIOD);

        // Assume data will be valid
        bool isValidData = true;

        // Get the data
        string currentArduinoDataPacket = ArduinoInterface.currentArduinoDataPacket;
        // Split the line at each comma (this is how the code currently formats the output)
        string[] vec3 = currentArduinoDataPacket.Split(',');

        // Check that there are all 4 parts
        if (vec3.Length < 4)
        {
            isValidData = false;
        }

        // If the data is valid, process it
        if (isValidData)
        {

            float w = float.Parse(vec3[0]) * (negativeW ? -1f : 1f);
            float x = float.Parse(vec3[1]) * (negativeX ? -1f : 1f);
            float y = float.Parse(vec3[2]) * (negativeY ? -1f : 1f);
            float z = float.Parse(vec3[3]) * (negativeZ ? -1f : 1f);


            yield return new WaitForSeconds(serialPortRefreshPeriod);

            if (serialPort != null)
            {
                // Request data
                Debug.Log("Requesting data...");
                serialPort.Write("1");
                yield return new WaitForSeconds(serialPortRequestDelayPeriod);

                // Assume data will be valid
                bool isValidData = true;

                string line = sp.ReadLine();//Read line output from arduino code
                                            //Debug.Log(line);
                string[] vec3 = line.Split(',');//split the line at each tab (this is how the code currently formats the output)


                if (vec3[0] != "" && vec3[1] != "" && vec3[2] != "" && vec3[3] != "" && vec3[4] != "") //check that no values are blank
                {
                    float potVal = vec3[4]; // set the fourth element of vec3 to 
                    float fingerRotation = ((potval / potMax)(range)) + min; //relate potentiometer reading to rotation, potentiometer goes from 0 - 676
                    transform.eulerAngles = new Vector3(0, fingerRotation, 0);
                }

                // figure out how to make fingerRotation to actually move the finger
            }

            StartCoroutine("ProcessFingerRotation");
        }
    }
}

