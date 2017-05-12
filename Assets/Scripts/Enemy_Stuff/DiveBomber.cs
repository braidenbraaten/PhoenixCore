using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is the most common enemy type in the game (ie. low level ship)
public class DiveBomber : BaseEnemy {
    public enum TURN{NONE, LEFT, RIGHT, UP, DOWN }public TURN turn;
    private int leftToDestroyCounter;
    public Vector3 addedForce;

    private List<GameObject> deployed;


    [Tooltip("The rigidnody of the Airplane Voxel")]
    public GameObject airplaneVoxel;
    Rigidbody r;
    //public GameObject prefab;
	// Use this for initialization
	void Start () {
        deployed = GameObject.FindObjectOfType<MotherShip>().deployedShips;
        leftToDestroyCounter = GameObject.FindObjectOfType<MotherShip>().m_shipsLeftToDestroy;
        r = GetComponent<Rigidbody>();
        //m_movement.myPattern = EnemyMovement.PATTERN.ZIG;
        //target = GameObject.FindWithTag("Player").GetComponent<Player>();
        //r = airplaneVoxel.GetComponent<Rigidbody>();
	}
    void LateStart()
    {
        //flashingLightScript.PlaneObject = this.gameObject;
    }

    void FixedUpdate()
    {
        r.AddForce(new Vector3 (0,0, -addedForce.z), ForceMode.Force);
        

    }
	
	// Update is called once per frame
	void Update () {
        TurnDirection(turn);
        


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

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            shotDown = true;
        }


        if (collision.collider.tag == "Building")
        {
            hasCrashed = true;
        }
    }

    public override void Death()
    {
        leftToDestroyCounter -= 1;
        deployed.Remove(this.gameObject);
        //this should wait to destroy until the death animation is finished
        Destroy(gameObject, 2f);
        
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
                left.spring = 1;
                right.spring = 1;
                up.spring = 1;
                down.spring = 1;
                break;
            case TURN.LEFT:
                left.spring = 3;
                right.spring = 1;
                up.spring = 1;
                down.spring = 1;
                break;
            case TURN.RIGHT:
                right.spring = 3;
                left.spring = 1;
                up.spring = 1;
                down.spring = 1;
                break;
            case TURN.UP:
                up.spring = 3;
                left.spring = 1;
                right.spring = 1;
                down.spring = 1;
                break;
            case TURN.DOWN:
                down.spring = 3;
                up.spring = 1;
                left.spring = 1;
                right.spring = 1;
                break;
            default:
                break;
        }

        
    }
}
