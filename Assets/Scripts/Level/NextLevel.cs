using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {

    [SerializeField] private string nextLevel;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    
    }

    public void NewLevel()
    {
        // Fadeout
        StartCoroutine(ChangeLevel());
    }

    IEnumerator ChangeLevel()
    {
        //Waits for the death animation to be done.
        yield return new WaitForSeconds(1.8f);

        float fadeTime = GameObject.Find("Level").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime + 1.8f);
        SceneManager.LoadScene(nextLevel);
    }
}
