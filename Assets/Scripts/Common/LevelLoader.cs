using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public string WhichSceneToLoad;

    public Animator transition;

    public float transitionTime = 1f;


    public void LoadNextLevel () {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }   
    
    public void LoadNextLevel (bool Nothing) {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }   

    IEnumerator LoadLevel(int levelIndex) {

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    public void QuitGame () {
        StartCoroutine(GameQuitter());
    }   

    IEnumerator GameQuitter() {
        
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        Application.Quit();
    }
    
    public void LoadSpecificLevel () {
        SceneManager.LoadScene(WhichSceneToLoad);
    }   
    public void LoadSpecificLevel (string Level) {
        SceneManager.LoadScene(Level);
    }
       
}