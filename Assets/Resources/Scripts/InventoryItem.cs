using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InventoryItem
{
    public string name;
    public int count;
    public Sprite icon;
    public InventoryItem(string name, int count)
    {
        this.name = name;
        this.count += count;
    }
}
