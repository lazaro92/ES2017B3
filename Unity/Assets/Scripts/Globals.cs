using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour {

	//CONSTANTS
	public static int HEALTH = 100;//change if needed
	public static int TIME_PER_TURN = 10;
	//Pistol
	public const int PISTOL_DAMAGE = 25;
	public const int PISTOL_AMMO = 20; //If limitedAmmo is true
	public static int SHOTS_PER_TURN = 2;

	
	//Pickaxe
	public const int PICKAXE_DAMAGE = 50;

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

	public static List<int> points = new List<int>();


	//TURN CONTROL
	public static bool changeTurn = false;
	public static bool skipTurn = false;
	public static int accPoints = 0;
	public static int remainingShots = SHOTS_PER_TURN;
	public static int remainingGrenades = GRENADE_THROW_PER_TURN;

	// Sound
	public static bool enabledSound = true;
}
