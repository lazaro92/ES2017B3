using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour {
	// player Prefab from inspector
	public GameObject player;
    // array of chickens in every team
    public GameObject[][] chickens;

    // current chicken to move (index of chickens)
    private int teamCounter;
    private int chickenCounter;

    //chicken numbers
    public int numTeams = 2; //hardcoded for now
    public int[] chickensPerTeam = { Globals.numChickens, Globals.numChickens }; //static number of chicken per team, for now


    private PlayerController playerController;
	private CameraFollow camFollow;


	// Use this for initialization
	void Start () {
        teamCounter = 0;
        chickenCounter = 0;

        chickens = new GameObject[numTeams][];
        for (var j = 0; j < numTeams; j++)
        {
            chickens[j] = new GameObject[chickensPerTeam[j]];
            for (var i = 0; i < chickens[j].Length; i++)
            {
                chickens[j][i] = Instantiate(player, new Vector3(-7.82f + i, -3.0f, 0), Quaternion.identity);
            }
        }
        playerController = chickens[0][0].GetComponent<PlayerController>();
        playerController.setMovement(true);

        camFollow = Camera.main.GetComponent<CameraFollow>();
    }
	
	// Update is called once per frame
	void Update () {
        int currentTeam, currentChicken;

        if (Input.GetKeyDown(KeyCode.N)) //Code to change turn
        {
            playerController.setMovement(false); //release the current chicken

            currentTeam = ++teamCounter % chickens.Length; //get the current team
            if (currentTeam == 0) 
                chickenCounter++;
            currentChicken = chickenCounter % chickens[currentTeam].Length; //get the current chicken index

            
            GameObject chicken = chickens[currentTeam][currentChicken]; //select the theoretical chicken to put into play
            //now, to avoid dead chickens
            int carry = 0;
            while (chicken.GetComponent<PlayerController>().health <= 0) //if and while the chicken we selected is dead
            {
                carry++; //select the next chicken 
                chicken = chickens[currentTeam][chickenCounter + carry % chickens[currentTeam].Length]; //(without affecting the global chickenCount)
            } if (carry > 0) //if we've had to look for alive chickens
            { //swap the alive chicken with the dead one (  to avoid reaching the same alive chicken multiple times e.g AA(X)XXXAAXA  )
                chickens[currentTeam][chickenCounter + carry % chickens[currentTeam].Length] = chickens[currentTeam][currentChicken];
                chickens[currentTeam][currentChicken] = chicken;
            }
            playerController = chicken.GetComponent<PlayerController>();
            playerController.setMovement(true);
            camFollow.setFollower(currentTeam, currentChicken);
        }
    }
}
