using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
[System.Serializable]
public class CurrencyQuantity
{
    public Currency.ID CurrencyType = Currency.ID.None;
    public int CurrencyAmount;
}*/

[System.Serializable]
public class ItemQuantity
{
    public ItemDefinition itemDefinition;
    public int ItemAmount;
}