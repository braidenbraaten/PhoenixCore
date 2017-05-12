using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneProto : MonoBehaviour {
    Animator ani;
	// Use this for initialization
	void Start () {
        ani = GetComponent<Animator>();
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (ani != null)
            {
                ani.SetBool("ShotDown", true);
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
