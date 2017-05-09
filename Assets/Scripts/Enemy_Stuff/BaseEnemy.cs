using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//all of the smaller ships besides the mothership should inherit this
public class BaseEnemy : MonoBehaviour {
    [HideInInspector]
    public EnemyMovement m_movement = new EnemyMovement();
    //the player is the target for the enemy
    public Player target;
    public Vector3 m_position  { get; private set; }

    //the value that the enemy is worth dead
    public int pointValue;
    public int m_health;
    //num of hitpoints it will take away from the player
    public int m_damageOutput;
    //energy value, the value they are worth captured
    public int energyValue;
	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
        m_position = this.transform.position;
	}


    public void setPosition(Vector3 inputVec)
    {
        m_position = inputVec;
    }

  

    
}
