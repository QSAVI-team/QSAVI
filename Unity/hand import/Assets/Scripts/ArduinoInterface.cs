using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoInterface : MonoBehaviour {

    // ------------------------- CONSTANT SERIAL PORT DATA -------------------------

    [Header("COM Port")]
    public string setCOMPort = "COM3";
    public int setCOMBaud = 115200;

    public static string COM_PORT;
    public static int COM_BAUD;

    // ------------------------- CONSTANT TIMING DATA -------------------------

    [Header("Timing")]
    public float setSerialPortRefreshPeriod = 0.01f; // Seconds
    public float setSerialPortRequestDelayPeriod = 0.0005f; // Seconds

    public static float SERIAL_PORT_REFRESH_PERIOD;
    public static float SERIAL_PORT_REQUEST_DELAY_PERIOD;
    public static float cal = 0;

    public static Vector2 w = new Vector2 (0, 0) ;
    public static Vector2 x = new Vector2 (0, 0);
    public static Vector2 y = new Vector2(0, 0);
    public static Vector2 z = new Vector2(0, 0);
    //public static float[,] fingers = new float[4, 2]; 

    public static Vector2 thumb = new Vector2(0, 0);
    public static Vector2 index = new Vector2(0, 0);
    public static Vector2 middle = new Vector2(0, 0);
    public static Vector2 pinky = new Vector2(0, 0);
  

    public static int palm0_elbow4 = 0;
 
    // ------------------------- SERIAL PORT -------------------------

    private SerialPort serialPort;

	// ------------------------- DATA -------------------------

	public static string currentArduinoDataPacket = "";

	// ------------------------- AWAKE -------------------------

	void Awake() {

		// Serial port
		COM_PORT = setCOMPort;
		COM_BAUD = setCOMBaud;

		// Timing
		SERIAL_PORT_REFRESH_PERIOD = setSerialPortRefreshPeriod;
		SERIAL_PORT_REQUEST_DELAY_PERIOD = setSerialPortRequestDelayPeriod;

	}

	// ------------------------- START -------------------------

	void Start () {

		// Setup COM port
		string comPortName = ArduinoInterface.COM_PORT;
		serialPort = new SerialPort (comPortName, ArduinoInterface.COM_BAUD);
		serialPort.Open ();

		// Start getting data
		if (serialPort.IsOpen) {
			StartCoroutine ("getSerialData");
			Debug.Log ("Opened " + comPortName);
		} else {
			Debug.LogError ("Unable to open " + comPortName);
		}

      


    }

	// ------------------------- GET SERIAL PORT DATA -------------------------

	IEnumerator getSerialData() {

		yield return new WaitForSeconds (SERIAL_PORT_REFRESH_PERIOD);

		if (serialPort != null) {

			// Request data
			Debug.Log ("Requesting data...");
			serialPort.Write ("1");
       
			yield return new WaitForSeconds (SERIAL_PORT_REQUEST_DELAY_PERIOD);
    
            // Read the current data packet
            currentArduinoDataPacket = serialPort.ReadLine ();
            string[] DataPacket = currentArduinoDataPacket.Split(',');

            Debug.Log ("Data: " + currentArduinoDataPacket);
            Debug.Log("Requesting data");
            // Separate DataPAcket into variables
            cal = float.Parse(DataPacket[0]);
            w = new Vector2 (float.Parse(DataPacket[1]), float.Parse(DataPacket[5]));
            x = new Vector2 (float.Parse(DataPacket[2]), float.Parse(DataPacket[6]));
            y = new Vector2 (float.Parse(DataPacket[3]), float.Parse(DataPacket[7]));  
            z = new Vector2 (float.Parse(DataPacket[4]), float.Parse(DataPacket[8]));
            //float [,] fingers = new float [4, 2] {{float.Parse(DataPacket[9])  , float.Parse(DataPacket[10])},  // thumb
            // { float.Parse(DataPacket[11]) , float.Parse(DataPacket[12]) },// index
            // { float.Parse(DataPacket[13]) , float.Parse(DataPacket[14]) },  // middle
            // { float.Parse(DataPacket[15]) , float.Parse(DataPacket[16]) }};  // pinky
            thumb = new Vector2(float.Parse(DataPacket[9]), float.Parse(DataPacket[10]));
            index = new Vector2(float.Parse(DataPacket[11]), float.Parse(DataPacket[12]));
            middle = new Vector2(float.Parse(DataPacket[13]), float.Parse(DataPacket[14]));
            pinky = new Vector2(float.Parse(DataPacket[15]), float.Parse(DataPacket[16]));


        /*
        if (error == 1) { Debug.Log("  Fifo 1 "); }
        else if (error == 2) { Debug.Log("fifo 2 "); }
        else if (error == 3) { Debug.Log("DMP Initialization failed "); }
        else if (error == 4) { Debug.Log("fifo 2 "); }
        else if (error == 5) { Debug.Log("fifo 2 "); }
        else if (error == 6) { Debug.Log("fifo 2 "); };
        */

        Debug.Log("Data Retrieved");
            // Continue to get more data
            StartCoroutine ("getSerialData");
            

        }

	}

}
