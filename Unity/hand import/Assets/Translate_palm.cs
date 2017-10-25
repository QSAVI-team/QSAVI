using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Translate_palm : MonoBehaviour
{

    SerialPort sp = new SerialPort("COM4", 115200);
    float[] lastPosition = { 0, 0, 0 };
    // Use this for initialization
    void Start()
    {
        sp.Open();
    }

    // Update is called once per frame
    void Update()
    {
        string line = sp.ReadLine();//Read line output from arduino code
        Debug.Log(line);
        string[] vec3 = line.Split(',');//split the line at each tab (this is how the code currently formats the output)
        Debug.Log(vec3);

        if (vec3[0] != "" && vec3[1] != "" && vec3[2] != "")//check that no values are blank
        {
            transform.Translate(                           //parse each value from a string to a float
                float.Parse(vec3[0]) - lastPosition[0],
                float.Parse(vec3[1]) - lastPosition[1],
                float.Parse(vec3[2]) - lastPosition[2],
                Space.Self
                );
            lastPosition[0] = float.Parse(vec3[0]);//set new values for the most recent rotation
            lastPosition[1] = float.Parse(vec3[1]);
            lastPosition[2] = float.Parse(vec3[2]);
            sp.BaseStream.Flush();
        }
    }
}
