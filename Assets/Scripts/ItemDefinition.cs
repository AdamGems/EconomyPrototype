using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDefinition : MonoBehaviour
{
    public Type type;
    public List<Tag> tags;
    public int LevelRequirement;

    [Header("Display Settings")]
    public string DisplayName;
    public Sprite DisplaySprite;
    public string DisplayDescription;
    
    [Header("Store Settings")]
    public ItemQuantity storeCost; //Store Cost
    public int purchaseQuantity = 1;
    public int MaxQuantityOwnable = 1000;

    [Header("Job & Recipe Settings")]
    public float BuildTime;
    public ItemQuantity[] Ingredients;
    public int RecipeResultAmount = 1;
    public ItemQuantity[] BuildRewards;

    public enum Type { Any = 0, Currency = 1, Food = 2, Character = 3, Location = 4, Equipment = 5, Job = 6}
    public enum Tag { Any }

    
}
