using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
	// player Prefab from inspector
	public GameObject player;
	// array of chickens in every team

	// current chicken to move (index of chickens)
	private int teamCounter;
	public static int currentTeam, lastTeam;
	// Control cursor
	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;
	//chicken numbers

	public static int numTeams; //hardcoded for now
	public int[] chickensPerTeam; //static number of chicken per team, for now

	public static List<LinkedList<GameObject>> squads; //array of linkedlists, one per team
	public static LinkedListNode<GameObject>[] currentChickens; // last chicken of every team who played

	LinkedListNode<GameObject> nextChicken;


	private PlayerController playerController;
	//Cameras
	private CameraFollow camFollow;

	public Camera mainCamera;
	public Camera secondCamera;

	//Camera listeners
	AudioListener mainCameraAudioLis;
	AudioListener secondCameraAudioLis;

	private Canvas cnvCurrentTeam;

	public Sprite changeBlue;
	public Sprite changeRed;

	public static GameStart instance;

	// Use this for initialization
	void Start()
	{
		//Camera
		mainCameraAudioLis = mainCamera.GetComponent<AudioListener>();
		secondCameraAudioLis = secondCamera.GetComponent<AudioListener>();

		numTeams = 2;
		teamCounter = 0;
		chickensPerTeam = new int[] { Globals.numChickens, Globals.numChickens };
		currentChickens = new LinkedListNode<GameObject>[numTeams];
		squads = new List<LinkedList<GameObject>>();
		for (var team = 0; team < numTeams; team++)
		{ // for every team
			Globals.points.Add(0);
			squads.Add(new LinkedList<GameObject>());
			player.gameObject.tag = "team" + team;
			for (var i = 0; i < chickensPerTeam[team]; i++) // add the amount of chickens necessary
				squads[team].AddFirst((GameObject)Instantiate(player, new Vector3( (team == 0)? -7.82f +i : 11 + i , -1.0f, 0), Quaternion.identity));

			currentChickens[team] = squads[team].First;
		}
		playerController = squads[0].First.Value.GetComponent<PlayerController>(); // put the first chicken on play
		playerController.setMovement(true);


		ShowMainCamera ();
		camFollow = mainCamera.GetComponent<CameraFollow>();

		GameObject goCurrentTeam = GameObject.Find("TeamTurnInfo");
		cnvCurrentTeam = goCurrentTeam.GetComponent<Canvas>();

		lastTeam = currentTeam;
		// Active icon
		playerController.activateImage ();
		// Desactive icon
		StartCoroutine("waitSecondsDesactivate");

		//Cursor
		OnMouseEnter();
	}

	// Update is called once per frame
	void Update()
	{
		for (var team = 0; team < numTeams; team++)
		{
			if (Globals.points[team] > Globals.MAX_POINTS)
			{
				SceneManager.LoadScene("FinalScene");
			}
		}
		if (Globals.changeTurn) //Code to change turn
		{
			Globals.changeTurn = false; // deactivate flag

			playerController.setMovement(false); //release the current chicken

			currentTeam = ++teamCounter % numTeams; //get the current team

			if (lastTeam != currentTeam){
				lastTeam = currentTeam;
                StartCoroutine(waitSecondsInformTeam(currentTeam));

            }

			nextChicken = currentChickens[currentTeam].Next ?? squads[currentTeam].First;

			/* Assign the new chicken in play */
			playerController = nextChicken.Value.GetComponent<PlayerController>();
			playerController.setMovement(true);
			camFollow.setFollower(nextChicken.Value);

			currentChickens[currentTeam] = nextChicken; //set the current chicken as the last chicken who played for this team
			// Active icon
			playerController.activateImage ();
			// Desactive icon
			StartCoroutine("waitSecondsDesactivate");
		}
	}

	private IEnumerator waitSecondsInformTeam(int team)
    {
		ShowGeneralView();
		cnvCurrentTeam.enabled = true;

		Transform temp = cnvCurrentTeam.transform.Find("changeTeam");
		Image changeTeam = temp.GetComponent<Image>();

		if (team == 0) {
			changeTeam.sprite = changeBlue;
		}
		else {
			changeTeam.sprite = changeRed;
		}
		Time.timeScale = 0.00001f;
        
		float pauseEndTime = Time.realtimeSinceStartup + 2;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }

		Time.timeScale = 1f;
		cnvCurrentTeam.enabled = false;
		ShowMainCamera ();
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
			if (squads.Count <= 1) {
				SceneManager.LoadScene("FinalScene");
			}

			if (team < currentTeam) {
				currentTeam--;
			}
		}

	}

	public static float pointProportion(int team)
	{
		float proportion = (float) Globals.points[team] / Globals.MAX_POINTS;
		return proportion;
	}

	// Change cursor
	void OnMouseEnter()
	{
		Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
	}

	// Funció que retarda la desactivació de l'icona de jugador actual
	IEnumerator waitSecondsDesactivate(){
		float newTime =  Globals.TIME_PER_TURN;
		yield return new WaitForSeconds(newTime-1);
		playerController.desactivateImage ();
	}
		
	//Cameras
	private void ShowGeneralView() {
		mainCamera.enabled = false;
		mainCameraAudioLis.enabled = false;

		secondCamera.enabled = true;
		secondCameraAudioLis.enabled = true;
	}

	private void ShowMainCamera() {
		mainCamera.enabled = true;
		mainCameraAudioLis.enabled = true;

		secondCamera.enabled = false;
		secondCameraAudioLis.enabled = false;
	}

}
