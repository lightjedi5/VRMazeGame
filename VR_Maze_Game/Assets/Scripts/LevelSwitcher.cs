using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour {

    public string destination;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player") {
            if (destination.ToLower() == "quit" )
            {

                Application.Quit();
            }
            SceneManager.LoadScene(destination);
        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}
