using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Pot : MonoBehaviour
{

    public int RecipeSize;
    public PreparedFood[] RequiredIngredients_food;
    private string[] RequiredIngredients_string;

    private HashSet<PreparedFood> contents_food;
    private HashSet<string> contents_string;

    //public HashSet<PreparedFood> RequiredIngredients;
    //public HashSet<PreparedFood> Ingredients;

    public GameObject FinishedFood;


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

        /*foreach (string s in RequiredIngredients_string)
        {
            Debug.Log(s);
        }*/
    }

    private void Update()
    {
        CheckContents();
    }

    public void AddIngredient(PreparedFood food)
    {
        contents_food.Add(food);
        contents_string.Add(food.GetComponent<Item_all>().GetName());
        //Debug.Log("Added " + food.GetComponent<Item_all>().GetName());
    }

    private void CheckContents()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            HashSet<PreparedFood> ReqIngredients = ConvertToHashSet(RequiredIngredients_food);
            HashSet<string> ReqIngredients_string = ConvertToHashSet(RequiredIngredients_string);


            if (ReqIngredients_string.SetEquals(contents_string))
            {
                Debug.Log("IT IS WORKING");
                foreach (PreparedFood f in contents_food)
                {
                    Debug.Log("Destroyed " + f.name);
                    Destroy(f.gameObject);
                }

                SpawnFinishedFood();
            }
            else
            {
                Debug.Log("nope");
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            /*foreach (PreparedFood f in contents_food)
            {
                Debug.Log(f.name);
            }*/

            foreach (string f in contents_string)
            {
                Debug.Log(f);
            }
            //Debug.Log(RequiredIngredients[0].name);
        }


    }

    private HashSet<T> ConvertToHashSet<T>(IEnumerable<T> source)
    {
        return new HashSet<T>(source);
    }

    private void SpawnFinishedFood()
    {
        Instantiate(FinishedFood, this.transform.position, this.transform.rotation);
    }
	
}
