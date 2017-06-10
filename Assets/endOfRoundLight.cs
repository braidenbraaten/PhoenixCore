using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endOfRoundLight : MonoBehaviour {

    //the light that we will use to indicate the end of the round
    Light l;
    public float flashesPerSecond;
    private float flashesInit;

    //if true, at the end of the round it will flash the light
    public bool RoundFlash;
	// Use this for initialization
	void Start () {
        l = GetComponent<Light>();
        flashesInit = flashesPerSecond;
	}
	
	// Update is called once per frame
	void Update () {


        if (RoundFlash)
        {
            flashesPerSecond -= Time.deltaTime;

            if (flashesPerSecond <= 0)
            {
                flashesPerSecond = flashesInit;
                l.enabled = !l.enabled;
            }
        }
        else
        {
            l.enabled = false;
        }

		
	}
}
