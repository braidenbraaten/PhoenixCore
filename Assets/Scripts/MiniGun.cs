using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGun : MonoBehaviour {
    //will be spun around as the minigun is fired 
    public GameObject barrel;
    public GameObject bulletPrefab;

    //the rate at which the barrel will spin 
    public float spinSpeed;
    private float maxSpinSpeed;
    private float minSpinSpeed;

    //the one that the other fireRate will reset to when 0
    public float fireRateOriginal;
    public float fireRate;
    

    //the time it takes to go from 0 to spin speed
    public float windUpTime;
    public int numOfBullets;
    public float BarrelRadius;
    private float maxBarrelRadius;
    private float minBarrelRadius;

	// Use this for initialization

	void Start () {
        maxSpinSpeed = spinSpeed;
        minSpinSpeed = 1f;
       

        maxBarrelRadius = BarrelRadius;
        minBarrelRadius = .2f;
		
	}
	
	// Update is called once per frame
	void Update () {

        //change this later to a key on the vive 
        if (Input.GetKey(KeyCode.Y))
        {
            //SpawnBullets();

            spinBarrell();
            shrinkBarrel();
        }
        else
        {
            expandBarrel();
        }

        
	}


    //encapsulated so the barrel doesn't care about input
    void spinBarrell()
    {
        SpawnDifferently(fireRate);
    }


    void expandBarrel()
    {
       BarrelRadius =  Mathf.Lerp(BarrelRadius, maxBarrelRadius, Time.deltaTime * 1f);
        spinSpeed = Mathf.Lerp(spinSpeed, minSpinSpeed, Time.deltaTime * 1f);
    }

    void shrinkBarrel()
    {
        BarrelRadius = Mathf.Lerp(BarrelRadius, minBarrelRadius, Time.deltaTime * 1f);
        spinSpeed = Mathf.Lerp(spinSpeed, maxSpinSpeed, Time.deltaTime * 1f);
    }

    //should take the bullets that were made, and put them in the 
    //correct positions


    //angle = 0;
    // add to the angle after the time interval
    //and spawn the bullet to the correct angle

    private float barrel_angle = 0;
    public void SpawnDifferently(float RateOfFire)
    {
        fireRate -= Time.deltaTime;
        barrel_angle += spinSpeed  * Time.deltaTime;
        

        if (barrel_angle >= 360)
        {
            barrel_angle = 1;
        }

        if (fireRate <= fireRateOriginal)
        {
            BuildBullets(1, BarrelRadius, barrel_angle);
            BuildBullets(1, BarrelRadius, barrel_angle + 180);
            fireRate = fireRateOriginal;
        }
    }


    public void SpawnBullets()
    {


        float angle;

        if (numOfBullets != 0) { angle = 360 / numOfBullets; }
        else
        {
            angle = 360 / (numOfBullets + 1);
        }

        for (int i = 0; i < numOfBullets; i++)
        {
            BuildBullets(i, BarrelRadius, angle);
        }


    }


    //should spawn the bullets around the barrel center
    public void BuildBullets(int i, float l_radius, float l_angle)
    {
        GameObject go = Instantiate(bulletPrefab, transform.position + new Vector3(Mathf.Rad2Deg * Mathf.Cos(i * l_angle), Mathf.Rad2Deg * Mathf.Sin(i * l_angle), 0.0f) * .05f * l_radius, transform.rotation);
        
        //the bullet layer is layer 10 
        go.layer = 10;
        go.tag = "Bullet";
        Destroy(go, 5);
    }
}
