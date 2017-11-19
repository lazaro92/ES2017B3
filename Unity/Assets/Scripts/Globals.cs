using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour {

	//CONSTANST
	public const int HEALTHVALUE = 100;//change if needed
	//Pistol
	public const int PISTOL_DAMAGE = 50;
	public const int PISTOL_AMMO = 10; //If limitedAmmo is true
	//Pickaxe
	public const int PICKAXE_DAMAGE = 100;
	//STATIC
	public static int numChickens = 2; //initial chickens to gameplay
	public static int numFlags = 2; //initial chickens to gameplay
	public static bool limitedAmmo = true;
	public static int health = 200;
    internal static readonly int MAXTEAMS = 2;
    internal static bool changeTurn = false;
}
