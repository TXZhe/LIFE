using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

	public static GameControl instance;         //A reference to our game control script so we can access it statically.
	public GameObject gameOvertext;
	public bool gameOver = false;

	// Use this for initialization

	void Awake()
	{
		//If we don't currently have a game control...
		if (instance == null)
			//...set this one to be it...
			instance = this;
		//...otherwise...
		else if(instance != this)
			//...destroy this one because it is a duplicate.
			Destroy (gameObject);
	}

//	 Update is called once per frame
	void Update () 
	{   
		if (gameOver && Input.GetKeyDown(KeyCode.Space)) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		
		}

	}

	public void PlayerDied()
	{
		gameOvertext.SetActive (true);
		gameOver = true;
	}
}
