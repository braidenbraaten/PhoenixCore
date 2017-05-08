using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//things we need the player to be able to do in here 
/*
*
* 
*/
public class Player : MonoBehaviour {
    GameManager gameManager;
    Movement movement;
    //GameObject player;
    public int m_health = 100;
    public Vector3 m_position { get; private set; }

    //This will be the GameObject that the player will be attached to

    //movement 

	// Use this for initialization
	void Start () {
        
        
	}
	
	// Update is called once per frame
	void Update () {
        m_position = this.transform.position;

        
        if (m_health <= 0.0f)
        {
            Debug.Log("Died from low Health");
            gameManager.lost.Invoke();
        }


	}
}
