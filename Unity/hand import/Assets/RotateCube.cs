using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class RotateCube : MonoBehaviour {
    //public string port=""; 
	SerialPort sp = new SerialPort("COM6", 115200);
    float[] lastRotation = { 0, 0, 0 };
    float[] caliRotation = { 0, 0, 0 }; // position pointing down at your side

	// Use this for initialization
	void Start () {
        sp.Open();
	}
	
	// Update is called once per frame
	void Update () {
        string line = sp.ReadLine();//Read line output from arduino code
        Debug.Log(line);
        string[] vec3 = line.Split(',');//split the line at each tab (this is how the code currently formats the output)
        Debug.Log(vec3);
/*
        if (vec3[0] == "0" && vec3[1] == "0" && vec3[2] == "0")
        {
            while (vec3[0] == "0" && vec3[1] == "0" && vec3[2] == "0")
            { }
        }
        caliRotation[0] = float.Parse(vec3[0]);
        caliRotation[1] = float.Parse(vec3[1]);
        caliRotation[2] = float.Parse(vec3[2]);
        */
        if (vec3[0] != "" && vec3[1] != "" && vec3[2] != "")//check that no values are blank
        {
            transform.Rotate(                           //parse each value from a string to a float
                float.Parse(vec3[0]) - lastRotation[0],// - caliRotation[0],
                float.Parse(vec3[1]) - lastRotation[1] ,//- caliRotation[1],
                float.Parse(vec3[2]) - lastRotation[2] ,//- caliRotation[2],
                Space.Self
                );
            lastRotation[0] = float.Parse(vec3[0]);//set new values for the most recent rotation
            lastRotation[1] = float.Parse(vec3[1]);
            lastRotation[2] = float.Parse(vec3[2]);
            sp.BaseStream.Flush();
        }
       
	}
}
