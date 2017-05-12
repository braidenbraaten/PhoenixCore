using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This should be used to cause a delay in the bullet's added force time
public class HellFireMissile : MonoBehaviour {

    Animator ani;

    public float FireDelay;
    [Tooltip("For third stage init.")]
    public float SecondDelay;
    public float speedToRotate;
    private float step;
    public Vector3 initialForce;
    private Vector3 savedAngle;
    public float AngleToReach;


    private List<GameObject> ActiveEnemies = new List<GameObject>();
    private List<GameObject> motherShipDeployed = new List<GameObject>();

    public Vector3 addedForce;
    [HideInInspector]
    public Rigidbody Rigid;

    [HideInInspector]
    public bool firstStageComplete = false;
    [HideInInspector]
    public bool secondStageComplete = false;
    //will be used to make sure the main engines are cut off
    public bool cutOffThrust = false;
    public bool foundTarget = false;
    public bool hasHitTarget = false;

	// Use this for initialization
	void Start () {
        ani = GetComponent<Animator>();
        //motherShipDeployed = GameObject.FindObjectOfType<MotherShip>().deployedShips;
        Rigid = GetComponent<Rigidbody>();
        
        //Rigid.AddRelativeForce(initialForce, ForceMode.Impulse);
	}
    //a distance set away from the cannon
    public Vector3 target; // = new Vector3(0,0,3);
    public Vector3 targetDir;
    //private Vector3 curr;
	// Update is called once per frame
	void Update () {
        ActiveEnemies = GameObject.FindObjectOfType<MotherShip>().deployedShips;
        //curr = transform.rotation.eulerAngles;
        //target = 
        //step = speedToRotate * Time.deltaTime;
        if (!firstStageComplete)
        {
            Rigid.AddRelativeForce(initialForce, ForceMode.Impulse);
            Rigid.useGravity = true;
            firstStageComplete = true;
            savedAngle = transform.rotation.eulerAngles;
            targetDir = target - transform.position;
        }

        
        step = speedToRotate * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        //Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);

        if (firstStageComplete)
        {
            FireDelay -= Time.deltaTime;

            Vector3.RotateTowards(savedAngle,  target, step, 0.0f );

        }

        if (secondStageComplete)
        {
            SecondDelay -= Time.deltaTime;

        }

        if (SecondDelay <= 0)
        {
            cutOffThrust = true;
        }


        if (!cutOffThrust)
        {
            if (FireDelay <= 0)
            {
                Debug.Log("ON SECOND STAGE");
                Rigid.useGravity = false;
                Rigid.AddRelativeForce(addedForce, ForceMode.Force);
                secondStageComplete = true;
            }
        }
        else
        {
            //Eat Cake
            //Rigid.useGravity = true;

            //This will cause the missile to find a new target
            if(closeEnemy == null) {foundTarget = false;}

            if (!hasHitTarget)
            {
                FindTarget();
                ChaseTarget();
            }
            else
            {
                
            }
        }

	}


    private float slowRadius = 5;
    private float t_dist = 0;
    public float maxSpeed;
    private float speed;
    private Vector3 targetVel;
    private Vector3 steeringForce;
    void ChaseTarget()
    {
        t_dist = Vector3.Distance(closeEnemy.transform.position, transform.position);
        if (t_dist > slowRadius)
        {
            speed = maxSpeed;
        }
        else
        {
            speed = maxSpeed * (t_dist / slowRadius);
        }

        //(closeEnemy.transform.position + closeEnemy.GetComponent<Rigidbody>().velocity)
        targetVel = Vector3.Normalize(closeEnemy.transform.position - transform.position) * speed;
        steeringForce = targetVel - Rigid.velocity;

        transform.LookAt(transform.position + targetVel);
        Rigid.AddForce(steeringForce);
        //        slowRadius = 40

        //maxSpeed = 100
        //dist = distance(tpos, mpos)
        //speed = maxSpeed * (dist > slow) ? 1 : dist / slowRadius
        //desiredVelocity = ((tpos + tvel) - mpos).normalized * speed

        //steeringForce = desiredVelocity - velocity

        //lookAt(mpos + desiredVelocity)
        //addForce(SteeringForce)



        //Rigid.AddRelativeForce(addedForce, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHitTarget)
        {

            ani.SetBool("HitTargetOnce", true);
        }
        else
        {
            if (collision.collider.gameObject.tag == "Bomber" || collision.collider.gameObject.tag == "Fighter" || collision.collider.gameObject.tag == "Captain")
            {
                ani.SetBool("HitTarget", true);
                ani.SetBool("HitTargetOnce", true);
                
                hasHitTarget = true;
                //add explosion effect here
            }
        }


        
        


        //Destroy(gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        ani.SetBool("HitTargetOnce", false);
    }

    //first value is checked, then second is checked against that to see
    //if it is samaller
    private Vector3 TargetVec = new Vector3(0,0,0);
    private Vector3 ClosestVec = new Vector3(0,0,0);
    private GameObject closeEnemy;
    private float ClosestDistance = 500.0f;
    private float dist = 0.0f;
    void FindTarget()
    {
        //Hotdog
        if (!foundTarget)
        {
            for (int i = 0; i < ActiveEnemies.Count; i++)
            {
                TargetVec = transform.position - ActiveEnemies[i].transform.position;
                dist = Vector3.Distance(transform.position, ActiveEnemies[i].transform.position);

                if (dist < ClosestDistance)
                {
                    ClosestDistance = dist;
                    ClosestVec = ActiveEnemies[i].transform.position;
                    closeEnemy = ActiveEnemies[i];
                }

            }

            TargetVec = ClosestVec;
            foundTarget = true;
            //Rigid.ResetInertiaTensor();
            //transform.Rotate(Vector3.RotateTowards(transform.position, TargetVec, step * 360, 0.0f));

            Debug.Log("Target Vector: " + TargetVec );
            Debug.Log("Target ID: " + closeEnemy.name);
        }
    }
}
