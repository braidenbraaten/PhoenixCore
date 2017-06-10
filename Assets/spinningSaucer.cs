using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinningSaucer : MonoBehaviour {

    public float RotateSpeed;

    [HideInInspector]
    public float minRotateSpeed, maxRotateSpeed;
    private float m_angle;


	// Use this for initialization
	void Start () {
        maxRotateSpeed = 260;
        minRotateSpeed = RotateSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.Rotate(transform.up, m_angle + RotateSpeed * Time.deltaTime);
	}
}
