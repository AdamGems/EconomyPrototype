using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public ItemDefinition[] ItemDefinitions;

    public void Initialize()
    {
        ItemDefinitions = Resources.LoadAll<ItemDefinition>("Item Definitions");
    }
}
