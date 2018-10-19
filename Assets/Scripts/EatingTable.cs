using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EatingTable : MonoBehaviour {

    // This script gets attached to the table with the plate - the "Victory table"
    // It's only purpose is to check if it collides with the finished food product, and if it does, you win.

    public GameObject VictoryPanel;
    private Player_Controller player;
    private Manager manager;

    private void Start()
    {
        player = FindObjectOfType<Player_Controller>();
        manager = FindObjectOfType<Manager>();
        VictoryPanel.SetActive(false);
    }

    private void OnCollisionEnter(Collision coll)
    {
        // If the final product comes in contact with the this table
        if (coll.transform.gameObject.GetComponent<Item_all>().GetName().Equals("A Delightful Meal"))
        {
            //Debug.Log("YOU WIN!!!!");

            VictoryPanel.SetActive(true);
            player.CanMove = false;

            manager.StopMusic();
            AudioSource VictorySound = this.GetComponent<AudioSource>();
            VictorySound.Play();
        }
    }
}
