using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_all : MonoBehaviour
{
    // This script gets attached to every holdable item
    // It return the items name (defined in inspector) to show on the UI

    // it was the simplest solution i could think of to account for a problem i was having with getting the names
    // of all the objects that are interactable -- just attach this to every object lol and define public vars.
    // I don't have THAT many objects in the game, so it's not that big of a pain. If i had 1000 i wouldn't do this.

    public String Title;

    public String GetName()
    {
        return Title;
    }
}