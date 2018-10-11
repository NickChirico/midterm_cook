using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
	// This script is attached to any "choppable" food objects the player interacts with. 
	// It doesn't do/call anything on its own, but methods are called from the player class 
	
	
	public GameObject ChoppedFood;
	public int ChopAmount;

	
	// This "chops" the current food (gameobject) by instantiating a certain number of objects in its place and then destroying itself.
	// This is called in the player_movement class; in the UpdateClick method
	public void Chop()
	{
		//Debug.Log("CHOPPED");
		CreateChoppedFood(ChopAmount);
		Destroy(this.gameObject);
	}

	
	// This just iterates through a for loop for "amount" number of times, instantiating a new object each time. 
	// Example: if a particular ingredient should get cut into THREE pieces, this loops 3 times
	private void CreateChoppedFood(int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			Instantiate(ChoppedFood, this.transform.position, this.transform.rotation);
		}
	}
}
