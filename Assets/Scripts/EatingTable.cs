using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EatingTable : MonoBehaviour {

    public GameObject VictoryPanel;
    
    private void OnCollisionEnter(Collision coll)
    {
        // If it comes in contact with the cooking pot
        if (coll.transform.gameObject.GetComponent<Item_all>().GetName().Equals("Cooked Food"))
        {
            Debug.Log("YOU WIN!!!!");
            //CookingPot.AddIngredient(this);

            VictoryPanel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
    }
}
