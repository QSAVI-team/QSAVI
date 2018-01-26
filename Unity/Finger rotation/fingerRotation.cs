using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public float min = 0;
public float max = 0;
public int potMin = 0;
public int potMax = 676;
public int baudRate = 0;
public string portName = "";
float range = max - min;

public class Rotate_Finger : MonoBehaviour
{   // ------------------------- SERIAL PORT -------------------------

    //SerialPort sp = new SerialPort(portName,baudRate); // fill in com port
    SerialPort serialPort;
    // ------------------------- TIMING -------------------------

    float serialPortRefreshPeriod;
    float serialPortRequestDelayPeriod;
}
// Use this for initialization
void Start()
{
    // Setup timing
    serialPortRefreshPeriod = Constants.SERIAL_PORT_REFRESH_PERIOD;
    serialPortRequestDelayPeriod = Constants.SERIAL_PORT_REQUEST_DELAY_PERIOD;

    // Setup COM port
    string comPortName = Constants.COM_PORT;
    serialPort = new SerialPort(comPortName, Constants.COM_BAUD);
    serialPort.Open();

    // Start getting data
    if (serialPort.IsOpen)
    {
        StartCoroutine("getSerialData");
        Debug.Log("Opened " + comPortName);
    }
    else
    {
        Debug.LogError("Unable to open " + comPortName);
    }
    
    
		// Setup timing
		serialPortRefreshPeriod = Constants.SERIAL_PORT_REFRESH_PERIOD;
		serialPortRequestDelayPeriod = Constants.SERIAL_PORT_REQUEST_DELAY_PERIOD;

		// Setup COM port
		string comPortName = Constants.COM_PORT;
		serialPort = new SerialPort (comPortName, Constants.COM_BAUD);
		serialPort.Open ();

		// Start getting data
		if (serialPort.IsOpen) {
			StartCoroutine ("getSerialData");
			Debug.Log ("Opened " + comPortName);
		} else {
			Debug.LogError ("Unable to open " + comPortName);
		}


    IEnumerator getSerialData()
    {

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
