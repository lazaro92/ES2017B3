using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {
    // player Prefab from inspector
    public GameObject player;
    // array of chickens in every team

    // current chicken to move (index of chickens)
    private int teamCounter;
    public static int currentTeam;
	// Control cursor
	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;
    //chicken numbers
    public static int numTeams; //hardcoded for now
    public int[] chickensPerTeam; //static number of chicken per team, for now
    public static List<LinkedList<GameObject>> squads; //array of linkedlists, one per team
    public static LinkedListNode<GameObject> [] currentChickens; // last chicken of every team who played
    LinkedListNode<GameObject> nextChicken;

    private PlayerController playerController;
	private CameraFollow camFollow;


	// Use this for initialization
	void Start () {
        numTeams = 2;
        teamCounter = 0;
        chickensPerTeam = new int[] { Globals.numChickens, Globals.numChickens };
        currentChickens = new LinkedListNode<GameObject>[numTeams];
        squads = new List<LinkedList<GameObject>>();
        for (var team = 0; team < numTeams; team++) { // for every team
            squads.Add(new LinkedList<GameObject>());
			player.gameObject.tag = "team"+team;
            for (var i = 0; i < chickensPerTeam[team]; i++) // add the amount of chickens necessary
                squads[team].AddFirst( (GameObject) Instantiate(player, new Vector3(-7.82f + i, -1.0f, 0), Quaternion.identity));
                
            currentChickens[team] = squads[team].First;
        }
        playerController = squads[0].First.Value.GetComponent<PlayerController>(); // put the first chicken on play
        playerController.setMovement(true);

        camFollow = Camera.main.GetComponent<CameraFollow>();

		//Cursor
		OnMouseEnter();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.N) || Globals.changeTurn) //Code to change turn
        {
            Globals.changeTurn = false; // deactivate flag

            playerController.setMovement(false); //release the current chicken

            currentTeam = ++teamCounter % numTeams; //get the current team

            nextChicken = currentChickens[currentTeam].Next ?? squads[currentTeam].First;

            /* Assign the new chicken in play */
            playerController = nextChicken.Value.GetComponent<PlayerController>();
            playerController.setMovement(true);
            camFollow.setFollower(nextChicken.Value);

            currentChickens[currentTeam] = nextChicken; //set the current chicken as the last chicken who played for this team
        }
    }

	public static void deleteChicken(GameObject chicken)
	{
		int team;

		for (team = 0; team < numTeams; team++)
			if (chicken == currentChickens[team].Value)
			{
				currentChickens[team] = currentChickens[team].Previous ?? squads[team].Last;
				squads[team].Remove(chicken);
				Globals.changeTurn = true;
				break;
			}
			else if (squads[team].Remove(chicken))
				break;

		if (squads[team].Count == 0)
		{
			numTeams--;
			squads.RemoveAt(team);
			if (squads.Count <= 1)
				SceneManager.LoadScene("FinalScene");
			if (team < currentTeam)
				currentTeam--;
		}

	}

	// Change cursor
	void OnMouseEnter()
	{
		Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
	}


}
