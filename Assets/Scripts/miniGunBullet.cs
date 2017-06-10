using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniGunBullet : MonoBehaviour {

    private Rigidbody r;
	// Use this for initialization
	void Start () {

        if (GetComponent<Rigidbody>() != null)
        {
            r = GetComponent<Rigidbody>();
        }
        else
        {
            r = gameObject.AddComponent<Rigidbody>();
        }


        //tag = "Bullet";
        //Destroy(this.gameObject, 5);

	}
	
	// Update is called once per frame
	void Update () {
        r.AddRelativeForce(new Vector3(0, 0, .7f), ForceMode.Impulse);
	}
}
