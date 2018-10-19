using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    // This script is attached to a empty game object in the world.
    // It holds the commands for the UI button activity. 

    public GameObject StartPanel;
    private Player_Controller player;
    private AudioSource music;


    private void Start()
    {
        player = FindObjectOfType<Player_Controller>();
        music = this.GetComponent<AudioSource>();
        music.Play();
        StartPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Begin()
    {
        StartPanel.SetActive(false);
        player.CanMove = true;
    }

    public void StopMusic()
    {
        music.Stop();
    }
}
