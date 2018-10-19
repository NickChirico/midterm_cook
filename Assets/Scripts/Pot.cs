using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Pot : MonoBehaviour
{
    // This script is attached to the cooking pot. It is mostly comprised of public methods called from other classes. 
    // It's purpose is to store the contents of the pot in an array (converted from a hashset) and compare it
    // to the "recipe" (a list of food items defined in the inspector)
    // If they match, it "cooks" the food, and creates the finished food product.

    public int RecipeSize;
    public PreparedFood[] RequiredIngredients_food;
    private string[] RequiredIngredients_string;

    private HashSet<PreparedFood> contents_food;
    private HashSet<string> contents_string;

    private bool CanCook = false;

    private AudioSource chime;

    public GameObject FinishedFood;
    public GameObject lid_placeholder;

    public GameObject CookButton_Off;
    public GameObject CookButton_On;


    void Start()
    {
        //RequiredIngredients = new PreparedFood[5];
        contents_string = new HashSet<string>();
        contents_food = new HashSet<PreparedFood>();

        RequiredIngredients_string = new string[RequiredIngredients_food.Length];

        for (int i = 0; i < RequiredIngredients_food.Length; i++)
        {
            RequiredIngredients_string[i] = RequiredIngredients_food[i].GetComponent<Item_all>().GetName();
        }

        CookButton_Off.SetActive(true);
        CookButton_On.SetActive(false);

        chime = this.GetComponent<AudioSource>();
    }


    public void AddIngredient(PreparedFood food)
    {
        contents_food.Add(food);
        contents_string.Add(food.GetComponent<Item_all>().GetName());
        //Debug.Log("Added " + food.GetComponent<Item_all>().GetName());
    }

    // Cook method is called when the player hits the stove button when the pot is ready.
    // Checks the contents of the pot and compares it to the recipe (defined in inspector)
    // If the contents are correct, it spawns the finished food. 
    public void Cook()
    {
        // Using hashsets instead of arrays because the contents of the pot have no definitive capacity. 
        // Using hashsets as a mutable list that is able to compare contents

        HashSet<PreparedFood> ReqIngredients = ConvertToHashSet(RequiredIngredients_food);
        HashSet<string> ReqIngredients_string = ConvertToHashSet(RequiredIngredients_string);


        // If the ingredients in the pot match the required recipe...
        // destroy the contents of the pot and spawn the finished food item

        if (ReqIngredients_string.SetEquals(contents_string))
        {
            foreach (PreparedFood f in contents_food)
            {
                Destroy(f.gameObject);

                contents_food = new HashSet<PreparedFood>();
                contents_string = new HashSet<string>();
            }

            chime.Play();

            SpawnFinishedFood();
            CanCook = false;
        }
        else
        {
            // Do nothing if the contents don't match the recipe

            //Debug.Log("Does not match recipe");
        }

    }

    // Converts a given array into a hashset with the same contents. 
    private HashSet<T> ConvertToHashSet<T>(IEnumerable<T> source)
    {
        return new HashSet<T>(source);
    }

    private void SpawnFinishedFood()
    {
        Instantiate(FinishedFood, this.transform.position, this.transform.rotation);
    }

    private void OnCollisionEnter(Collision coll)
    {
        string nameTemp = coll.gameObject.GetComponent<Item_all>().GetName();

        // If the pot lid collides with the pot, the pot lid snaps into its proper place,
        // and the ability to "cook" is enabled.
        if (nameTemp.Equals("Pot Lid"))
        {
            coll.gameObject.transform.position = lid_placeholder.transform.position;
            CanCook = true;

            CookButton_On.SetActive(true);
            CookButton_Off.SetActive(false);
            //Debug.Log("POT LID IN PLACE");
        }
    }

    private void OnCollisionExit(Collision coll)
    {
        // If you remove the pot lid, you can no longer "cook"

        string nameTemp = coll.gameObject.GetComponent<Item_all>().GetName();

        if (nameTemp.Equals("Pot Lid"))
        {
            CanCook = false;

            CookButton_Off.SetActive(true);
            CookButton_On.SetActive(false);
            //Debug.Log("POT LID IN PLACE");
        }
    }
}
