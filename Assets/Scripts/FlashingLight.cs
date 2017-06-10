using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//will be used to make the lights on the ship look like they are firing in order

public class FlashingLight : MonoBehaviour {
    public List<Light> SaucerLights;
    public float waitTimer;
    private float resetTimer;
    //the current light that we are on
    private int lightIndex;


	// Use this for initialization
	void Start () {
        resetTimer = waitTimer;
        lightIndex = 0;

        for (int i = 0; i < SaucerLights.Count; i++)
        {
            SaucerLights[i].enabled = false;
        }

	}
	
	// Update is called once per frame
	void Update () {

        waitTimer -= Time.deltaTime;

        if (waitTimer <= 0)
        {

            lightIndex += 1;
            if (lightIndex > SaucerLights.Count - 1)
            {
                lightIndex = 0; 
            }

            SaucerLights[lightIndex].enabled = true;

            if (lightIndex == 0)
            {
                SaucerLights[3].enabled = false;
            }
            else
            {
                SaucerLights[lightIndex - 1].enabled = false;
            }


            waitTimer = resetTimer;

        }
		
	}
}
