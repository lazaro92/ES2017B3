using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Globals : MonoBehaviour {

	//FINAL STATS
	public static int Points1 = 0, Points2 = 0, Flags1 = 0, Flags2 = 0, Death1 = 0, Death2 = 0;


	//CONSTANTS
	public static int HEALTH = 100;//change if needed
	public static int TIME_PER_TURN = 10;
	//Pistol
	public const int PISTOL_DAMAGE = 25;
	public const int PISTOL_AMMO = 20; //If limitedAmmo is true
	public static int SHOTS_PER_TURN = 2;

	
	//Pickaxe
	public const int PICKAXE_DAMAGE = 10;

	//GrenadeThrower
	public const int GRENADE_THROW_AMMO = 5; //If limitedAmmo is true
	public static int GRENADE_THROW_PER_TURN = 1;
	public static float GRENADE_THROW_FORCE = 3.5f;

	//Grenade
	public const int GRENADE_DAMAGE = 50;
	public const float GRENADE_DELAY = 2.3f;
	public const float GRENADE_RADIUS = 1.3f;

	//STATIC
	public static int numChickens = 2; //initial chickens to gameplay
	public static int numFlags = 2; //initial chickens to gameplay
	public static bool limitedAmmo = true;
	public static int MAX_POINTS = 7000; //points to win
	internal static readonly int MAXTEAMS = 2;

	//POINTS

	public static void updatePoints(string tag, float points)
	{
		int team;
		if (tag=="team0"){
			team = 1;
		}
		else{
			team = 0;
		}

		updatePoints(team, points);
	}

	public static void updatePoints(int team, float points)
    {
        GameStart.bar1.fillAmount += (team == 0) ? points : 0f;
        GameStart.bar2.fillAmount += (team == 1) ? points : 0f;

        FinalText.updatePoints(team, (int)points * 100);

        if (GameStart.bar2.fillAmount >= 1 || GameStart.bar1.fillAmount >= 1)
            SceneManager.LoadScene("FinalScene");
    }


	//TURN CONTROL
	public static bool changeTurn = false;
	public static bool skipTurn = false;
	public static int remainingShots = SHOTS_PER_TURN;
	public static int remainingGrenades = GRENADE_THROW_PER_TURN;

	// Sound
	public static bool enabledSound = true;

}
