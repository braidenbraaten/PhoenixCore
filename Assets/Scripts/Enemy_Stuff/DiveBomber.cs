using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is the most common enemy type in the game (ie. low level ship)
public class DiveBomber : BaseEnemy {
    private int leftToDestroyCounter;
    public Vector3 addedForce;

    [Tooltip("The rigidnody of the Airplane Voxel")]
    public GameObject airplaneVoxel;
    Rigidbody r;
    //public GameObject prefab;
	// Use this for initialization
	void Start () {
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
        
        //this should wait to destroy until the death animation is finished
        Destroy(gameObject, 2f);
        
    }
}
