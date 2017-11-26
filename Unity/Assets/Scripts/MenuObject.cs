using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuObject : MonoBehaviour {

	public CursorMode cursorMode = CursorMode.Auto;

	// Use this for initialization
	void Start () {
		OnMouseExit (); // Cursor
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeScene(string name){
		SceneManager.LoadScene (name);

	}

	public void ExitGame(){
		Debug.Log("Exit");
		Application.Quit();
	}

	// Change cursor
	void OnMouseExit()
	{
		Cursor.SetCursor(null, Vector2.zero, cursorMode);
	}

}
