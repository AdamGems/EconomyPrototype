using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PlayerState : MonoBehaviour
{
    public Dictionary<ItemDefinition, int> OwnedItems;
    public List<Feature.ID> UnlockedFeatures;
    public string PlayerName;
    public int XP;
    public int Level;

    public void Initialize()
    {
        OwnedItems = new Dictionary<ItemDefinition, int>();
        foreach (ItemDefinition definition in GameManager.instance.itemManager.ItemDefinitions)
            OwnedItems.Add(definition, 0);

        foreach(ItemQuantity itemQuantity in GameManager.instance.PlayerConstants.StartingItems)
        {
            OwnedItems[itemQuantity.itemDefinition] += itemQuantity.ItemAmount;
        }
    }


    public bool CanAfford(ItemDefinition item, int cost)
    {
        if (OwnedItems[item] < cost)
            return false;
        return true;
    }

    public bool CanAfford(ItemQuantity Quantity)
    {
        return CanAfford(Quantity.itemDefinition, Quantity.ItemAmount);
    }

    public bool CanAfford(ItemQuantity[] Quantities)
    {
        foreach(ItemQuantity quantity in Quantities)
        {
            if (!CanAfford(quantity))
                return false;
        }
        return true;
    }

    public void Pay(ItemDefinition item, int cost)
    {
        OwnedItems[item] -= cost;
    }

    public void Pay (ItemQuantity Quantity)
    {
        Pay(Quantity.itemDefinition, Quantity.ItemAmount);
    }

    public void Gain (ItemDefinition item, int amount)
    {
        OwnedItems[item] += amount;
    }

    public void Gain(ItemQuantity Quantity)
    {
        Gain(Quantity.itemDefinition, Quantity.ItemAmount);
    }
}
