using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is the most common enemy type in the game (ie. low level ship)
public class DiveBomber : BaseEnemy {

    public GameObject textUpPrefabObject;

    //the animator connected to the bomber
   // public Animator ani;
	public enum TURN{NONE, LEFT, RIGHT, UP, DOWN }public TURN turn;
	private int leftToDestroyCounter;
	public Vector3 addedForce;

	#region DivePhysicsInfo
		
	private Vector3 targetVelocity;
	private float targetDistance;
	private Vector3 steeringForce;
	float speed;
    //the amount of time it will wait before moving
    float TakeOffTimer;
    public float takeOffTimerMin, takeOffTimerMax;

    [HideInInspector]
    public bool completedTakeOff;

    private float takeOffTimerInit;
	public float maxSpeed;
	public Transform frontOfPlane;

	#endregion

	private List<GameObject> deployed;


	[Tooltip("The rigidnody of the Airplane Voxel")]
	public GameObject airplaneVoxel;
	Rigidbody r;
	Rigidbody voxelRigid;
	//public GameObject prefab;
	// Use this for initialization
	void Start () {
        TakeOffTimer = Random.Range(takeOffTimerMin, takeOffTimerMax);

        speed                = maxSpeed;
		deployed             = GameObject.FindObjectOfType<MotherShip>().deployedShips;
		leftToDestroyCounter = GameObject.FindObjectOfType<MotherShip>().m_shipsLeftToDestroy;
		r                    = GetComponent<Rigidbody>();
		voxelRigid           = airplaneVoxel.GetComponent<Rigidbody>();
		target                 = GameObject.FindObjectOfType<Player>();
		frontOfPlane           = airplaneVoxel.transform.Find("PlaneForward");
        takeOffTimerInit = TakeOffTimer;
        
        completedTakeOff = false;

        

		//m_movement.myPattern = EnemyMovement.PATTERN.ZIG;
		//target = GameObject.FindWithTag("Player").GetComponent<Player>();
		//r = airplaneVoxel.GetComponent<Rigidbody>();
	}
	void LateStart()
	{
		//flashingLightScript.PlaneObject = this.gameObject;
	}


	Quaternion newRotation;
	void FixedUpdate()
	{
		TurnDirection(turn);


		//ChaseTarget();
		//newRotation = Quaternion.LookRotation(;
		// r.rotation.Set(newRotation.x, newRotation.y, newRotation.z, newRotation.w);
	}
	
	// Update is called once per frame
	void Update () {


		PlaneChecks();

	}

	void OnCollisionEnter(Collision collision)
	{
		PlaneCollisionChecks(collision);
	}


    #region Checks

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, 40f);
    //}

    void PlaneChecks()
	{
        if (!completedTakeOff)
        {
            TakeOffTimer -= Time.deltaTime;
            if (TakeOffTimer <= 0f)
            {
                completedTakeOff = true;
                TakeOffTimer = takeOffTimerInit;

            }
        }

        if (completedTakeOff)
        {

            if (Vector3.Distance(transform.position, target.m_position) <= 60f)
            {
                ChaseTarget();
                SpinAround();
            }
            else
            {
                r.AddForce(new Vector3(0, 0, -.01f), ForceMode.Impulse);
            }

        }


        //ChaseTarget(); need them to chase the target once they are in range
        if (hasCrashed)
		{
			Debug.Log("Has CRASHED");
			animator.SetBool("Crashed", true);
			Death();
		}

		if (shotDown)
		{
			Debug.Log("HAS BEEN SHOT DOWN");
			animator.SetBool("ShotDown", true);
			Death();
		}

		if (hitPlayer)
		{
			Debug.Log("HAS HIT PLAYER");
			animator.SetBool("HitPlayer", true);
           
			Death();
		}
	}

	private void PlaneCollisionChecks(Collision collision)
	{



		if (collision.collider.tag == "Bullet")
		{
			shotDown = true;
		}

		if (collision.collider.tag == "Building")
		{
			hasCrashed = true;
		}

		if (collision.collider.tag == "Player")
		{
			hitPlayer = true;
		}
	}
    #endregion


    private float chaseRotationZed = 0.0f;
    //Should be called within a certain radius of the player, as a sort of handoff
    void SpinAround()
    {
        chaseRotationZed += 4f;

        if (chaseRotationZed >= 360)
        {
            chaseRotationZed = 0;
        }


        transform.Rotate(Vector3.forward, chaseRotationZed, Space.Self);

    }


    void ChaseTarget()
	{

        animator.SetBool("Diving", true);
		targetDistance = Vector3.Distance(target.transform.position ,transform.position);
		targetVelocity = Vector3.Normalize(target.transform.position - transform.position) * maxSpeed;
		steeringForce  = targetVelocity - r.velocity / targetDistance;

		transform.LookAt(transform.position + targetVelocity);
		r.AddForce(steeringForce);

        

	}


    GameObject floatingText;
    private floatTextUp text;
	//Make sure that this is not interfering with the animation Controller
	public override void Death()
	{
		leftToDestroyCounter -= 1;
		deployed.Remove(this.gameObject);
        //this should wait to destroy until the death animation is finished

        floatingText = Instantiate(textUpPrefabObject,transform.position, Quaternion.identity );
        //floatingText.transform.SetParent(null);
        Destroy(gameObject, 0f);
        
		
	}

    //the amount of time it will wait before it moves the text up
    //private float textUpTimer = 1f;
    //private float textDeathTimer = 5f;
    //private bool hasEnteredOnce = false;
    
    //public void DestroyText(ref TextMesh tm, float floatingTime)
    //{
    //    //textUpTimer = floatingTime;

    //    textUpTimer -= Time.deltaTime;
    //    textDeathTimer -= Time.deltaTime;

    //    if(textUpTimer <= 0)

    //    {
    //        pointTextObject.transform.position += new Vector3(0,10,0) * Time.deltaTime;

    //    }

    //    Destroy(tm, textDeathTimer);
        
        
    //}

	#region EffectedByDirection

	

	public void forceInDirection(TURN T, Vector3 Force)
	{
		switch (T)
		{
			case TURN.NONE:
				r.AddForce(airplaneVoxel.transform.forward * maxSpeed);
				break;
			case TURN.LEFT:
                r.AddForce(-airplaneVoxel.transform.right * maxSpeed);
				break;
			case TURN.RIGHT:
                r.AddForce(airplaneVoxel.transform.right * maxSpeed);
				break;
			case TURN.UP:
                r.AddForce(airplaneVoxel.transform.up * maxSpeed);
				break;
			case TURN.DOWN:
                r.AddForce(-airplaneVoxel.transform.up * maxSpeed);
				break;
			default:
				break;
		}
	}
	//FIGURE OUT WHY IT WON'T ACCEPT MULTIPLE SPRING-JOINTS
	//may need mutliple objects
	public SpringJoint left, right, up, down;
	private float turnLeftTime;
	public void TurnDirection(TURN T)
	{

		switch (T)
		{
			case TURN.NONE:
				left.spring  = 1;
				right.spring = 1;
				up.spring    = 1;
				down.spring  = 1;
				break;
			case TURN.RIGHT:
				//r.AddTorque(new Vector3(0,1f,0));
				left.spring  = 3;
				right.spring = 1;
				up.spring    = 1;
				down.spring  = 1;
				break;
			case TURN.LEFT:
				//transform.Rotate(new Vector3(0, -1f, 0));
				//r.AddRelativeTorque(new Vector3(0, -1f,0));
				//r.AddForceAtPosition(new Vector3(-1f,0,0) + (-transform.right), frontOfPlane.position);
				right.spring = 3;
				left.spring  = 1;
				up.spring    = 1;
				down.spring  = 1;
				break;
			case TURN.UP:
				up.spring    = 3;
				left.spring  = 1;
				right.spring = 1;
				down.spring  = 1;
				break;
			case TURN.DOWN:
				down.spring  = 3;
				up.spring    = 1;
				left.spring  = 1;
				right.spring = 1;
				break;
			default:
				break;
		}

		
	}
	#endregion
}
