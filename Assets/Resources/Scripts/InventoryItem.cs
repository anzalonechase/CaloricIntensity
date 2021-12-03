using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InventoryItem : ScriptableObject
{
    public string name;
    public int count;
    public InventoryItem(string name, int count)
    {
        this.name = name;
        this.count = count;
    }
}
