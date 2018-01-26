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
{
    // Read serial port
    SerialPort sp = new SerialPort(portName,baudRate); // fill in com port

}
// Use this for initialization
void Start()
{
    sp.Open();
}
// Update is called once per frame
void Update()
{
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
