using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour {

	// ------------------------- SERIAL PORT -------------------------

	[Header("COM Port")]
	public string setCOMPort = "COM3";
	public int setCOMBaud = 115200;

    public static string COM_PORT;
	public static int COM_BAUD;

	// ------------------------- TIMING -------------------------

	[Header("Timing")]
	public float setSerialPortRefreshPeriod = 0.01f; // Seconds
	public float setSerialPortRequestDelayPeriod = 0.0005f; // Seconds

	public static float SERIAL_PORT_REFRESH_PERIOD;
	public static float SERIAL_PORT_REQUEST_DELAY_PERIOD;

	void Awake() {

		// Serial port
		COM_PORT = setCOMPort;
		COM_BAUD = setCOMBaud;

		// Timing
		SERIAL_PORT_REFRESH_PERIOD = setSerialPortRefreshPeriod;
		SERIAL_PORT_REQUEST_DELAY_PERIOD = setSerialPortRequestDelayPeriod;

	}

}
