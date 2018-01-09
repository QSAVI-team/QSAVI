using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Rotate_Palm : MonoBehaviour
{
    //public string port=""; 
    SerialPort sp = new SerialPort("COM3", 115200);
    float[] caliRotation = { 0, 0, 0 }; // position pointing real hand down at your side
    float x =  0 ;          float y =  0 ;          float z =  0 ;
    float xo = 85.248f;     float yo = -52.267f;    float zo = -53.411f; // horizontal hand position, want to update later to being down
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
        

       // if (vec3[0] != "" && vec3[1] != "" && vec3[2] != "" && vec3[3] != "") //check that no values are blank
       // {
            if (vec3[0] == "2") // delimiter representing calibration
            {
                caliRotation[0] = float.Parse(vec3[1]);
                caliRotation[1] = float.Parse(vec3[2]);
                caliRotation[2] = float.Parse(vec3[3]);

                Debug.Log(caliRotation);

            transform.localEulerAngles = new Vector3(xo ,yo ,zo ); // set to original hand rotations if your attaching it to forearm set it to 0,0,0,
            /*  what we had before 
             *      transform.localEulerAngles=new Vector3(
                    float.Parse(vec3[1]) - caliRotation[0],             // 
                    float.Parse(vec3[2]) - caliRotation[1],
                    float.Parse(vec3[3]) - caliRotation[2]);
                */
            }
            else if (vec3[0] == "1") // delimiter representing rotation
            {

            x = float.Parse(vec3[1]) - caliRotation[0] + xo ;
            y = float.Parse(vec3[2]) - caliRotation[1] + yo ;
            z = float.Parse(vec3[3]) - caliRotation[2] + zo ;
               /* if (x > 360 || y > 360 || z > 360)
                {
                    Debug.Log("error euler larger than 360");
                    x = 359; y = 359; z = 359;
                }*/
            transform.localEulerAngles = new Vector3(x, y, z);                        //parse each value from a string to a float
            
            }
        //transform.Rotate()
        Debug.Log(vec3);
        /*
        else (vec3[0] == "0")// delimiter representing translation
        {
            transform.Translate(
                float.Parse(vec3[0]) - lastPosition[0], // - caliRotation[0],
                    float.Parse(vec3[1]) - lastPosition[1], //- caliRotation[1],
                    float.Parse(vec3[2]) - lastPosition[2], //- caliRotation[2],
                    Space.Self
                    );
            lastPosition[0] = float.Parse(vec3[0]); //set new values for the most recent rotation
            lastPosition[1] = float.Parse(vec3[1]);
            lastPosition[2] = float.Parse(vec3[2]);
            sp.BaseStream.Flush();
        }
         */

        // }
    }
}
