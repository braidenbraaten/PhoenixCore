using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

//should be two turret cannons / or one? maybe allow the player to upgrade over time?
public class Weapons : MonoBehaviour {
	[Header("CONTROL TYPE")]
	public bool usingVive;
	public bool SingleTriggerOnly;

	[Header("Prefabs")]
	public GameObject kineticPrefab;
	public GameObject laserPrefab;
	public GameObject rocketPrefab;
	public GameObject hellfirePrefab;
    public GameObject miniGunBulletPrefab;

	
	//public SteamVR_TrackedObject.EIndex[0] leftController;
	//public SteamVR_TrackedObject rightController;

	[Header("Compile Time")]
	public string kineticProjectileTag;
	public float BulletDestroyTime;
	//the single delay and timer will be used if it is singleTriggerOnly
	public float singleDelay;
	float singleTimer;

	public enum BULLET_TYPE { KINETIC, LASER, ROCKET, HELLFIRE, MINI}

	#region Cannon Stats

	[System.Serializable]
	public struct CannonStats
	{
		public GameObject Cannon;
		public SpringJoint joint;
		public GameObject bulletSpawnPos;
		public GameObject bullet;
		public BULLET_TYPE bullet_type;
		[HideInInspector]
		public BULLET_TYPE prevBulletType;
		public ParticleSystem particle;
		[HideInInspector]
		public ParticleSystem.MainModule pm;
		
		[HideInInspector]
		public Rigidbody r;
		public float bounceBackAmt;
		public float impulseSpeed;

		public float fireDelay;
		[HideInInspector]
		public float timer;

		public UnityEvent Fire;
		public AudioClip soundEffect;
	}
	[Header("Left Cannon Stats")]
	public CannonStats left;
	[Header("Right Cannon Stats")]
	public CannonStats right;
	#endregion

	//so that way we can set the weapons parent to the player object
	public Player m_p;

	AudioSource audioPosition;
	public AudioClip m_sound;
	// Use this for initialization

	void Start () {
		CannonSetup();

	}
	
  

	// Update is called once per frame
	void Update () {

		playerInputChecks();
		GunChecks();
		
	}

	void CannonSetup()
	{
		singleTimer = singleDelay;

		#region Left Cannon
		
		
		//left.joint.anchor = 
		left.Fire.AddListener(leftCannonFired);
		left.pm = left.particle.main;
		left.pm.simulationSpace = ParticleSystemSimulationSpace.World;
		left.r = left.Cannon.GetComponent<Rigidbody>();
		left.timer = left.fireDelay; right.timer = right.fireDelay;
		#endregion

		#region Right Cannon

		right.Fire.AddListener(rightCannonFired);
		right.r = right.Cannon.GetComponent<Rigidbody>();
		#endregion
	}

	void playerInputChecks()
	{
		if (usingVive)
		{
			//if (leftController.triggerPressed)
			//{
			//    left.timer -= Time.deltaTime;
			//}

			//if (rightController.triggerPressed)
			//{
			//    right.timer -= Time.deltaTime;
			//}


		}
		else
		{
			if (SingleTriggerOnly == false)
			{
				if (Input.GetKey(KeyCode.Mouse0))
				{
					left.timer -= Time.deltaTime;
				}

				if (Input.GetKey(KeyCode.Mouse1))
				{
					right.timer -= Time.deltaTime;
				}
			}
			else
			{
				if (Input.GetKey(KeyCode.Mouse0))
				{
					singleTimer -= Time.deltaTime;
					
				}
			}
		}

	}



	void GunChecks()
	{

		#region timers

	  
		if (left.timer <= 0.0f)
		{
			left.Fire.Invoke();

			left.timer = left.fireDelay;

			//left.particle.Stop();

		}

		if (right.timer <= 0.0f)
		{
			right.Fire.Invoke();

			right.timer = right.fireDelay;
		}

		if (singleTimer <= 0.0f)
		{
			left.Fire.Invoke();
			right.Fire.Invoke();

			singleTimer = singleDelay;
		}
		#endregion

		#region Bullet Types


		
			switch (left.bullet_type)
			{
				case BULLET_TYPE.KINETIC:
					left.bullet = kineticPrefab;
					//left.particle = gunFireParticles;
					break;
				case BULLET_TYPE.LASER:
					left.bullet = laserPrefab;
					//left.particle = gunFireParticles;
					break;
				case BULLET_TYPE.ROCKET:
					left.bullet = rocketPrefab;
					//left.particle = gunFireParticles;
					break;
				case
					BULLET_TYPE.HELLFIRE:
					left.bullet = hellfirePrefab;
					break;
                case
                    BULLET_TYPE.MINI:
                    left.bullet = miniGunBulletPrefab;
                    break;
				default:
					break;
			}
		

		
			switch (right.bullet_type)
			{
				case BULLET_TYPE.KINETIC:
					right.bullet = kineticPrefab;
					//right.particle = gunFireParticles;
					break;
				case BULLET_TYPE.LASER:
					right.bullet = laserPrefab;
					//right.particle = gunFireParticles;
					break;
				case BULLET_TYPE.ROCKET:
					right.bullet = rocketPrefab;
					//right.particle = gunFireParticles;
					break;
				case BULLET_TYPE.HELLFIRE:
					right.bullet = hellfirePrefab;
					break;
                case BULLET_TYPE.MINI:
                    right.bullet = miniGunBulletPrefab;
                break;
				default:
					break;
			}
		
		#endregion
		//always make sure that these are being set after the switch statements
		//left.prevBulletType = left.bullet_type;
		//right.prevBulletType = right.bullet_type;
	}

	public void KineticBulletFired(bool leftCannon)
	{
		GameObject local_bullet; Rigidbody r;

		#region bullet Instantiation
		if (leftCannon) {local_bullet = Instantiate(left.bullet, left.bulletSpawnPos.transform.position, left.Cannon.transform.localRotation);}
		else {local_bullet = Instantiate(right.bullet, right.bulletSpawnPos.transform.position, right.Cannon.transform.rotation);}
		#endregion

		local_bullet.transform.Rotate(90, 0, 0);
		local_bullet.tag = "Bullet";
		
		r = local_bullet.GetComponent<Rigidbody>();
	   

		if (leftCannon)
		{
			r.AddRelativeForce(new Vector3(0, left.impulseSpeed, 0), ForceMode.Impulse);
		}
		else
		{
			r.AddRelativeForce(new Vector3(0, right.impulseSpeed, 0), ForceMode.Impulse);
		}
		Destroy(local_bullet, BulletDestroyTime);


		//return local_bullet;
	}

	public void HellfireBulletFired(bool leftCannon)
	{
		GameObject local_bullet; Rigidbody r;

		#region bullet Instantiation

		if (leftCannon) { local_bullet = Instantiate(left.bullet, left.bulletSpawnPos.transform.position, Quaternion.identity);
			local_bullet.transform.Rotate(left.Cannon.transform.eulerAngles + new Vector3(0,0,0));
		}


		else { local_bullet = Instantiate(right.bullet, right.bulletSpawnPos.transform.position, Quaternion.identity); }

		#endregion

		//local_bullet.transform.Rotate(40,0,0);

		r = local_bullet.GetComponent<Rigidbody>();

		Destroy(local_bullet, BulletDestroyTime * 4f);
		//return local_bullet;
	}

    public void miniGunFired(bool LeftCannon)
    {
        GameObject local_bullet; Rigidbody r;

        if (LeftCannon)
        {
            local_bullet = Instantiate(left.bullet, left.bulletSpawnPos.transform.position, Quaternion.identity);
            local_bullet.transform.Rotate(left.Cannon.transform.eulerAngles + new Vector3(0, 0, 0));
        }
        else { local_bullet = Instantiate(right.bullet, right.bulletSpawnPos.transform.position, Quaternion.identity); }
        r = local_bullet.GetComponent<Rigidbody>();

        Destroy(local_bullet, BulletDestroyTime * 4f);


    }

	public void leftCannonFired()
	{
		
		left.r.AddRelativeForce(new Vector3(0,0,-left.bounceBackAmt), ForceMode.Impulse);
		left.particle.Play();
	   

		switch (left.bullet_type)
		{
			case BULLET_TYPE.KINETIC:
				KineticBulletFired(true);
				break;
			case BULLET_TYPE.LASER:
				break;
			case BULLET_TYPE.ROCKET:
				break;
			case BULLET_TYPE.HELLFIRE:
				HellfireBulletFired(true);
				break;
            case BULLET_TYPE.MINI:
                miniGunFired(true);
                break;
			default:
				break;
		}
		 //  MAY NEED TO CHANGE THE INSTANCE ROTATION TO TURRET FORWARD LATER
		 //local_bullet.tag = kineticProjectileTag;
		 //r = local_bullet.GetComponent<Rigidbody>();
		 //r.useGravity = false;

		//r.AddForce(new Vector3(0,0,left.impulseSpeed), ForceMode.Impulse);
		//Destroy(local_bullet, BulletDestroyTime);
	}



	public void rightCannonFired()
	{
		//GameObject local_bullet; Rigidbody r;
		right.r.AddForce(new Vector3(0,0,-right.bounceBackAmt), ForceMode.Impulse);
		right.particle.Play();
		

		switch (right.bullet_type)
		{
			case BULLET_TYPE.KINETIC:
				KineticBulletFired(false);
				break;
			case BULLET_TYPE.LASER:
				break;
			case BULLET_TYPE.ROCKET:
				break;
			case BULLET_TYPE.HELLFIRE:
				HellfireBulletFired(false);
				break;

            case BULLET_TYPE.MINI:
                miniGunFired(false);
                break;
			default:
				break;
		}
		//local_bullet.tag = kineticProjectileTag;
		//r = local_bullet.GetComponent<Rigidbody>();
		////r.useGravity = false;
		//local_bullet.transform.Rotate(90, 0, 0);
		//r.AddForce(new Vector3(0, 0, right.impulseSpeed), ForceMode.Impulse);
		//Destroy(local_bullet, BulletDestroyTime);
	}




	
}

/*      IDEAS FOR WEAPON STUFF
 * - Have gun jams that the player will have to fix / allow enemies to target the player's turrets and he would have to fix them 
 * in order to get them operational again?
 * -add in delay for weapon firing timing ?
 */
