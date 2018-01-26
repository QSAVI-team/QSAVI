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

			// Continue to get more data
			StartCoroutine ("getSerialData");

		}

	}

}
