using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour {
    // player Prefab from inspector
    public GameObject player;
    // array of chickens in every team

    // current chicken to move (index of chickens)
    private int teamCounter;
    public static int currentTeam;
    //chicken numbers
    public static int numTeams = 2; //hardcoded for now
    public int[] chickensPerTeam = { Globals.numChickens, Globals.numChickens }; //static number of chicken per team, for now
    public static LinkedList<GameObject>[] squads = new LinkedList<GameObject>[Globals.MAXTEAMS]; //array of linkedlists, one per team
    public static LinkedListNode<GameObject> [] lastChicken; // last chicken of every team who played
    LinkedListNode<GameObject> currentChicken;

    private PlayerController playerController;
	private CameraFollow camFollow;


	// Use this for initialization
	void Start () {
        teamCounter = 0;
        lastChicken = new LinkedListNode<GameObject>[numTeams];
        for (var team = 0; team < numTeams; team++) { // for every team
            squads[team] = new LinkedList<GameObject>();
            
            for (var i = 0; i < chickensPerTeam[team]; i++) // add the amount of chickens necessary
                squads[team].AddLast( (GameObject) Instantiate(player, new Vector3(-7.82f + i, -1.0f, 0), Quaternion.identity));
                
            lastChicken[team] = squads[team].First;
        }
        playerController = squads[0].First.Value.GetComponent<PlayerController>(); // put the first chicken on play
        playerController.setMovement(true);

        camFollow = Camera.main.GetComponent<CameraFollow>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.N) || Globals.changeTurn) //Code to change turn
        {
            Globals.changeTurn = false; // deactivate flag

            playerController.setMovement(false); //release the current chicken

            currentTeam = ++teamCounter % numTeams; //get the current team

            currentChicken = lastChicken[currentTeam].Next ?? squads[currentTeam].First;

            /* Assign the new chicken in play */
            playerController = currentChicken.Value.GetComponent<PlayerController>();
            playerController.setMovement(true);
            camFollow = Camera.main.GetComponent<CameraFollow>(); //ALERTA! he hagut de fer això pq sino no anava...
            camFollow.setFollower(currentChicken.Value);

            lastChicken[currentTeam] = currentChicken; //set the current chicken as the last chicken who played for this team
        }
    }

    public static void deleteChicken(GameObject chicken)
    {
        if (chicken == lastChicken[currentTeam].Value)
            lastChicken[currentTeam] = lastChicken[currentTeam].Previous ?? squads[currentTeam].Last; // apártate que vamos a hacer cosas muy feas...

        for (var team = 0; team < numTeams; team++)
            if (squads[team].Remove(chicken))
                break;
    }
}
