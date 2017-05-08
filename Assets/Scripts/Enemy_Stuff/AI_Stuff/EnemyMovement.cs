using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class will be used for the enemy's AI stuff
public class EnemyMovement : MonoBehaviour {

    //zig = 5-4, block = 3-2, wave = 1
    public enum PATTERN {ZIG, BLOCK, WAVE}; public PATTERN myPattern;

    //default zigzag movement that will be possible for all ships
    public bool useWaypointSystem = true;
    //make sure to hook this up to the mothership in the scene
    public MotherShip m;
    
	// Use this for initialization
	void Start () {
        myPattern = PATTERN.ZIG;
        m = GameObject.FindGameObjectWithTag("motherShip").GetComponent<MotherShip>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //should be the base pattern that the enemy will follow 
    public virtual void ZigZag_Pattern()
    {

    }


    public virtual void Block_Pattern()
    {

    }

    //is the most aggressive pattern so far, it should be used on defcon 1 & 2 
    public virtual void wave_Pattern()
    {

    }


    //
    public void receivePattern()
    {

    }
}
