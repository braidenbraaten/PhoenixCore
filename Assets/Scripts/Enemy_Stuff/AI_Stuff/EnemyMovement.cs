using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class will be used for the enemy's AI stuff
public class EnemyMovement : MonoBehaviour {

	public GameObject attachedPlane;
	private Rigidbody planeRigid;
	public float testTimer;
	private float testTimerReset;

	//zig = 5-4, block = 3-2, wave = 1
	public enum PATTERN {ZIG, BLOCK, WAVE}; public PATTERN myPattern;

	//default zigzag movement that will be possible for all ships
	public bool useWaypointSystem = true;
	//make sure to hook this up to the mothership in the scene
	public MotherShip m;
	
	// Use this for initialization
	void Start () {
		myPattern      = PATTERN.ZIG;
		//m              = GameObject.FindGameObjectWithTag("motherShip").GetComponent<MotherShip>();
		planeRigid     = attachedPlane.GetComponent<Rigidbody>();
		testTimerReset = testTimer;
	}



	private void FixedUpdate()
	{

		//RandomTurn(20.0f, testTimer);
	}

	// Update is called once per frame
	void Update () {
		testTimer -= Time.deltaTime;



		if (testTimer <= 0)
		{
			testTimer = testTimerReset;
		}


	}

	//trying to get the plane to swerve around 
	//- add a random force around the given one 
	//-do it for a certain period of time? 
	void RandomTurn( float forceAmt, float waitTime)
	{
		if(waitTime <= 0.0f)
		planeRigid.AddExplosionForce(forceAmt, attachedPlane.transform.position - new Vector3(0,0,-2f), 3.0f);
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
