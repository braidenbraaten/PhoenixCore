using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//this will be the enemy class that will spawn other's out of it
public class MotherShip : MonoBehaviour
{
    public GameManager GM;


    #region RoundStuff
    public int m_numOfWaves { private get; set; }
    public int m_currentWave { get; private set; }
    public int m_shipsLeftToDestroy { get; private set; }

    #endregion





    #region DEPLOYABLE_SHIP_STATS

    [System.Serializable]
    public struct ShipStats
    {
        [Header("Compile Time")]
        public GameObject prefab;
        public GameObject spawnPos;
        public int spawnCount;
        public string Tag;
        [Space]
        public float moveSpeed;
        public float radius;
        [Header("Physics Stuff")]
        public ForceMode forceType;
        public float resistance;
        public float impulseForceSpeed;


    }
    [Header("Bomber Stats")]
    public ShipStats b_stats;

    [Header("Fighter Stats")]
    public ShipStats f_stats;

    [Header("Captain Stats")]
    public ShipStats c_stats;



    #endregion


    [Header("Mother Ship Stats")]
    public int healthValue;
    //how fast will the mothership be moving
    public float m_moveSpeed;
    // (may need this if no coroutines)float build_resetTimer;
    //what difficulty of ships , and amt of them are being put out 
    //the lower the number, the more alert they are
    public enum DEFCON { ONE, TWO, THREE, FOUR, FIVE };
    [Header("Alert Level")]
    public DEFCON defcon;
    public DEFCON prevDefcon;
    [Header("Reaction to Alert Levels")]
    public UnityEvent[] m_defconReactions = new UnityEvent[5];

    //a list of gameObjects that are being "built"







    //the different hangar bays for the different enemy types
    [Header("Hangar Bays")]
    public List<GameObject> bomberBay;
    public List<GameObject> fighterBay;
    public List<GameObject> captainBay;
    // so the build function knows which type of ship it is building
    private enum HANGAR_TYPE { BOMBER, FIGHTER, CAPTAIN }

    [Header("Deployed Ships")]
    public List<GameObject> deployedShips = new List<GameObject>();
    //these will be used for when the defcon levels are used / changed
    

    // Use this for initialization
    void Start()
    {
        m_numOfWaves = GM.numberOfWaves;

        bomberBay = new List<GameObject>(b_stats.spawnCount);
        fighterBay = new List<GameObject>(f_stats.spawnCount);
        captainBay = new List<GameObject>(c_stats.spawnCount);

        //default defcon level (not red-alert)
        defcon = DEFCON.FIVE;
        prevDefcon = defcon;


        attachDefConMethods();

        //this should change to round based system

        SpawnShips(HANGAR_TYPE.BOMBER, b_stats.spawnCount, b_stats.radius);
        SpawnShips(HANGAR_TYPE.FIGHTER, f_stats.spawnCount, f_stats.radius);
        SpawnShips(HANGAR_TYPE.CAPTAIN, c_stats.spawnCount, c_stats.radius);
       
    }

   
    
    
    void Update()
    {
        if (prevDefcon != defcon)
            checkDefconLevel();

        prevDefcon = defcon;

        


    }

    //keeps the start clean, is in charge of connecting methods to events
    void attachDefConMethods()
    {
        m_defconReactions[0].AddListener(defcon_one);
        m_defconReactions[1].AddListener(defcon_two);
        m_defconReactions[2].AddListener(defcon_three);
        m_defconReactions[3].AddListener(defcon_four);
        m_defconReactions[4].AddListener(defcon_five);
    }



    //what will happen according to the defcon level
    void checkDefconLevel()
    {

        /*DEFCON LEVELS OF ALERTNESS*/
        switch (defcon)
        {
            case DEFCON.FIVE:
                m_defconReactions[4].Invoke();
                break;
            case DEFCON.FOUR:
                m_defconReactions[3].Invoke();
                break;
            case DEFCON.THREE:
                m_defconReactions[2].Invoke();
                break;
            case DEFCON.TWO:
                m_defconReactions[1].Invoke();
                break;
            case DEFCON.ONE:
                m_defconReactions[0].Invoke();
                break;
            default:
                break;
        }
    }


    //this will be used to launch the ships 
    public void launchShips(DEFCON defConLevel, List<GameObject> hangerbay)
    {

    }

    // REACTION ACTIONS PER DEFCON LEVEL \\
    #region DEFCON_REACTIONS

    #region ONE
    void defcon_one()
    {

    }

    #endregion

    #region TWO

    void defcon_two()
    {

    }
    #endregion

    #region THREE
    void defcon_three()
    {

    }
    #endregion

    #region FOUR
    void defcon_four()
    {

    }
    #endregion

    #region FIVE

    //least threatened level
    void defcon_five()
    {
        //used to change the pattern that the ships are currently
        //set to.
        foreach (GameObject g in deployedShips)
        {
            g.GetComponent<EnemyMovement>().myPattern = EnemyMovement.PATTERN.ZIG;

        }

    }
    #endregion

    #endregion


    //used to spawn new ships in the hangar bays

    //ships will spawn on the outside of the mothership, 
    void SpawnShips(HANGAR_TYPE T, int numOfShips, float radius)
    {
        float angle;
        //makes sure that there are no zero returns
        if (numOfShips != 0){ angle = 360 / numOfShips;}
        else{ angle = 360 / (numOfShips + 1);}
 
        for (int i = 0; i < numOfShips; ++i)
        {
            switch (T)
            {
                case HANGAR_TYPE.BOMBER:
                    build_bomber(i, angle, radius);
                    break;
                case HANGAR_TYPE.FIGHTER:
                    build_fighter(i, angle, radius);
                    break;
                case HANGAR_TYPE.CAPTAIN:
                    build_captain(i, angle, radius);
                    break;
                default:
                    Debug.Log("NO SHIP TYPE SELECTED, MAKE SURE IT HAS A HANGAR TYPE");
                    Debug.Break();
                    break;
            }
        }

    }

    void build_bomber(int i, float l_angle, float l_radius)
    {
        GameObject go = Instantiate(b_stats.prefab, b_stats.spawnPos.transform.position + new Vector3(Mathf.Rad2Deg * Mathf.Cos(i * l_angle), Mathf.Rad2Deg * Mathf.Sin(i * l_angle), 0.0f) * .05f * l_radius, Quaternion.identity);
        go.name = "Bomber_" + i;
        go.tag = b_stats.Tag;
        Rigidbody r =  go.AddComponent<Rigidbody>();
        BoxCollider b = go.AddComponent<BoxCollider>();
        DiveBomber db = go.AddComponent<DiveBomber>();


        db.target = GM.p_1;

        r.drag = b_stats.resistance;
        //      CHANGE THE NEGATIVE SIGN WHEN THEY FIX THE DIRECTION OF THE SHIP
        r.AddForce(new Vector3(0,0, b_stats.impulseForceSpeed * i), b_stats.forceType);
        r.isKinematic = false;
        r.useGravity = false;
        //b.size.Set()

        bomberBay.Add(go);
    }

    void build_fighter(int i, float l_angle, float l_radius)
    {
        GameObject go = Instantiate(f_stats.prefab, f_stats.spawnPos.transform.position + new Vector3(Mathf.Rad2Deg * Mathf.Cos(i * l_angle), Mathf.Rad2Deg * Mathf.Sin(i * l_angle), 0.0f) * .05f * l_radius, Quaternion.identity);
        go.name = "Fighter_" + i;
        Rigidbody r = go.AddComponent<Rigidbody>();
        BoxCollider b = go.AddComponent<BoxCollider>();
        r.isKinematic = false;
        r.useGravity = false;
        fighterBay.Add(go);
    }

    void build_captain(int i, float l_angle, float l_radius)
    {
        GameObject go = Instantiate(c_stats.prefab, c_stats.spawnPos.transform.position + new Vector3(Mathf.Rad2Deg * Mathf.Cos(i * l_angle), Mathf.Rad2Deg * Mathf.Sin(i * l_angle), 0.0f) * .05f * l_radius, Quaternion.identity);
        go.name = "Captain_" + i;
        Rigidbody r = go.AddComponent<Rigidbody>();
        BoxCollider b = go.AddComponent<BoxCollider>();
        r.isKinematic = false;
        r.useGravity = false;
        captainBay.Add(go);
    }


    
    


    

}
/*          COOL IDEAS FOR THE MOTHERSHIP
 * add some sort of visual indicator when the hangar bays are building ships
 * the longer it takes to kill the mothership, the faster the build speed will become
 *
 */

    //Clarifications
    /*
     *building ships will put them in the hangers, deploying will get them out 
     into the game world.
     * 
     */
      
//need to make it so 
/*
 * the defcon is only checked once if it is different than before
 * make it so the defcon level will return a list of positions for
 * smaller enemies to follow (ie. the pattern can change according to the defcon level
 *  
 */
