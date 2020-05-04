using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {
   // public AudioSource source;
    public bool startGame = true;

    void Start()
    {
      //  source = GetComponent<AudioSource>();
       // source.Play();
    }
    public void PlayGame()
    {
        MemoryScript.SaveToSavesFile();
        if (startGame)
            SceneManager.LoadScene(MemoryScript.GetCurrentLevel());
        else
            SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        MemoryScript.SaveToSavesFile();
        Application.Quit();
    }
}
