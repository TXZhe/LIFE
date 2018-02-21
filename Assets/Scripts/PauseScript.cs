using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

    GameObject PauseMenue;
    bool paused;

    
    // Use this for initialization
	void Start () {
        paused = false;
        PauseMenue = GameObject.Find("PauseMenue");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }
        if(paused)
        {
            PauseMenue.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            PauseMenue.SetActive(false);
            Time.timeScale = 1;
        }
	}

    public void Resume()
    {
        paused = false;
    }

    public void Mainmenue()
    {
        SceneManager.LoadScene("mainmenue");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
