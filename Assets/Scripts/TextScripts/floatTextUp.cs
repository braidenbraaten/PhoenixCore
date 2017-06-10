using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class should be used to create the text that will show up
public class floatTextUp : MonoBehaviour {
    //the text that will be used in the textMesh 
    public string inputText = "example Text";
    public TextMesh textMeshPrefab;
    private TextMesh tm;
    public Vector3 spawnPos;
    //the amt of time it will take before it starts floating up
    public float waitTimeToFloat;
    public float deathTime;
    public float Speed;
    public Vector3 m_direction;
    public Rigidbody myRigid;
    private Vector3 myVel;
    // Use this for initialization
	void Start () {
        myVel = GetComponent<Rigidbody>().velocity;
        tm = GetComponent<TextMesh>();
        //tm = Instantiate(textMeshPrefab, spawnPos + transform.position, Quaternion.identity);
        //tm.text = inputText;
	}

    //floatTextUp(string text, Vector3 spawningPos, float waitTime, float moveSpeed, Vector3 dir )
    //{
    //    inputText = text; spawnPos = spawningPos; waitTimeToFloat = waitTime; Speed = moveSpeed; m_direction = dir;
    //}
    //floatTextUp()
    //{
    //    inputText = "Not Given";


    //}

    private Vector3 newPosition;


	// Update is called once per frame
	void Update () {
        waitTimeToFloat -= Time.deltaTime;
        
        if (waitTimeToFloat <= 0)
        {
             gameObject.transform.position += m_direction * Speed * Time.deltaTime;
            //newPosition.x = Mathf.SmoothStep(newPosition.x - 2,  newPosition.x + 2  , Time.deltaTime);

            gameObject.transform.position += Vector3.Normalize( new Vector3(Mathf.SmoothDamp(transform.position.x, newPosition.x, ref myVel.x, 2f), 0, 0)) * .2f;
            //tm.transform.position = newPosition;
            //Destroy(gameObject, deathTime);
        }
        else
        {
            newPosition = gameObject.transform.position + new Vector3(2f, 0, 0);
        }

        //tm.text = inputText;
	}



    public void ChangeText(string newText)
    {
        tm.text = newText;
    }
}
