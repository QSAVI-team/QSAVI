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
    int calibration = 0;
    float wpalm = 0;
    float xpalm = 0;
    float ypalm = 0;
    float zpalm = 0;
    float indexbasePOT = 0;
    float indexkucklePOT = 0;
    float thumbCurlPOT = 0;
    float thumbLateralPOT = 0;
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
            // Separate DataPAcket into variables
            calibration = int.Parse(DataPacket[0]);
            wpalm = float.Parse(DataPacket[1]);
            xpalm = float.Parse(DataPacket[2]); 
            ypalm = float.Parse(DataPacket[3]); 
            zpalm = float.Parse(DataPacket[4]); 
            indexbasePOT = float.Parse(DataPacket[5]); 
            indexkucklePOT = float.Parse(DataPacket[6]) ;
            thumbCurlPOT = float.Parse(DataPacket[7]); 
            thumbLateralPOT = float.Parse(DataPacket[8]) ;

            // Continue to get more data
            StartCoroutine ("getSerialData");
            

        }

	}

}
