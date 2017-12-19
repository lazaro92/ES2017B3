using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalText : MonoBehaviour
{
	void Start()
	{
		Text TextPoints1, TextPoints2, TextFlags1, TextFlags2, TextDeath1, TextDeath2;
		TextPoints1 = GameObject.Find("Points1").GetComponent<Text>();
		TextPoints2 = GameObject.Find("Points2").GetComponent<Text>();
		TextFlags1 = GameObject.Find("Flags1").GetComponent<Text>();
		TextFlags2 = GameObject.Find("Flags2").GetComponent<Text>();
		TextDeath1 = GameObject.Find("Death1").GetComponent<Text>();
		TextDeath2 = GameObject.Find("Death2").GetComponent<Text>();
		Text TextWinner = GameObject.Find("TextWinner").GetComponent<Text>();

		if (TextPoints1 != null)
			TextPoints1.text = (GameStart.bar1.fillAmount * 100).ToString();
		if (TextPoints2 != null)
			TextPoints2.text = (GameStart.bar2.fillAmount * 100).ToString();

		if (TextFlags1 != null)
			TextFlags1.text = Globals.Flags1.ToString();
		if (TextFlags2 != null)
			TextFlags2.text = Globals.Flags2.ToString();

		if (TextDeath1 != null)
			TextDeath1.text = Globals.Death1.ToString();
		if (TextDeath2 != null)
			TextDeath2.text = Globals.Death2.ToString();

		if (GameStart.bar1.fillAmount > GameStart.bar2.fillAmount)
			TextWinner.text = "TEAM 1";
		else
			TextWinner.text = "TEAM 2";
	}


	public static void updatePoints(int team, int increment)
	{

		if (team == 0)
		{
			Globals.Points1 += increment;
			/*TextPoints1 = GameObject.Find("Points1").GetComponent<Text>();
			TextPoints1.text = (int.Parse(TextPoints1.text) + increment).ToString();*/
		}
		else
		{
			Globals.Points2 += increment;
			/*TextPoints2 = GameObject.Find("Points2").GetComponent<Text>();
			TextPoints2.text = (int.Parse(TextPoints2.text) + increment).ToString();*/
		}
	}

	public static void updateFlags(int team, int increment)
	{

		if (team == 0)
		{
			Globals.Flags1 += increment;
			/*TextFlags1 = GameObject.Find("Flags1").GetComponent<Text>();
			TextFlags1.text = (int.Parse(TextFlags1.text) + increment).ToString();*/
		}
		else
		{
			Globals.Flags2 += increment;
			/*TextFlags2 = GameObject.Find("Flags2").GetComponent<Text>();
			TextFlags2.text = (int.Parse(TextFlags2.text) + increment).ToString();*/
		}
	}

	public static void updateDeaths(int team, int increment)
	{
		if (team == 0)
		{
			Globals.Death1 += increment;
			/*TextDeath1 = GameObject.Find("Death1").GetComponent<Text>();
			TextDeath1.text = (int.Parse(TextDeath1.text) + increment).ToString();*/

		}
		else
		{
			Globals.Death2 += increment;
			/*TextDeath2 = GameObject.Find("Death2").GetComponent<Text>();
			TextDeath2.text = (int.Parse(TextDeath2.text) + increment).ToString();*/

		}
	}
	
}