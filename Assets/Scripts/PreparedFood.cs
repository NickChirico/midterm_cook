using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparedFood : MonoBehaviour
{
    // This is the script attached to prepared food (chopped food etc.)

    private Pot CookingPot;

    private void Start()
    {
        CookingPot = FindObjectOfType<Pot>();
    }

    private void OnTriggerEnter(Collider coll)
    {
        // If it comes in contact with the cooking pot
        if (coll.transform.gameObject.CompareTag("Pot"))
        {
            //Debug.Log("Into Pot!");
            CookingPot.AddIngredient(this);
        }
    }
}