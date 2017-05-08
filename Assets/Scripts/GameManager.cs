using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
//will be in charge of the win loss for the player 
public class GameManager : MonoBehaviour {
    public enum DIFFICULTY { EASY, NORMAL, HARDs }

    [Header("Events")]
    public UnityEvent lost;
    public UnityEvent won;
    public UnityEvent m_Start;
    public UnityEvent m_MainMenu;

    [Header("Player Settings")]
    //will be single player for now, maybe add more in later?
    public Player p_1;
    public DIFFICULTY difficulty;
    //Wave Settings


    [Header("Wave Settings")]
    [Tooltip("NOW Stands for Number Of Waves")]
    public int NOW_Easy;
    public int NOW_Normal;
    public int NOW_Hard;

    [HideInInspector]
    public int numberOfWaves;

    [Header("Score Settings")]
    public int game_Score;


    

	// Use this for initialization
	void Start () {
        DetermineWaveCount();

        won.AddListener(hasWon);
        lost.AddListener(hasLost);
        m_Start.AddListener(StartGame);
        m_MainMenu.AddListener(MainMenu);
	}
	
	// Update is called once per frame
	void Update () {
        checkState();
	}

    void DetermineWaveCount()
    {
        switch (difficulty)
        {
            case DIFFICULTY.EASY:
                numberOfWaves = NOW_Easy;
                break;
            case DIFFICULTY.NORMAL:
                numberOfWaves = NOW_Normal;
                break;
            case DIFFICULTY.HARDs:
                numberOfWaves = NOW_Hard;
                break;
            default:
                break;
        }
    }


    void checkState()
    {

    }

    //after the player goes to leave the main menu and starts the match
    void StartGame()
    {
        //after the Main menu
    }

    void MainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        //should be the first scene that the player will enter
    }

    void hasWon()
    {
       // if : & after start game
    }

    void hasLost()
    {
        // if : after start game
    }

    
}
