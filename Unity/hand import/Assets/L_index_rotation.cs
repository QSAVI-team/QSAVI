using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_index_rotation : MonoBehaviour {
    private object myRotation;

    public object zValue { get; private set; } // not sure if this is the right declaration 
    public object yValue { get; private set; }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (myRotation == null)
        {
            myRotation = gameObject.GetComponent<r>();
            if (myRotation == null)
            {
                myRotation = gameObject.AddComponent<Rotation>();
            }
        }
        if (myRotation != null)
        {
            myRotation.Euler.z = zValue;
            myRotation.Euler.y = yValue;
        }


    }
}
