using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ItemDisplay : MonoBehaviour
{
    public ItemDefinition loadedItem;

    public Image ItemImage;
    public Text ItemName;
    public Text ItemDescription;
    public Text ItemCostText;
    public Image ItemCostIcon;
    public Text ItemOwnedQuantity;
    public Text ItemOwnedMaxQuantity;
    public GameObject SelectionIndicator;


    public Text QuantityText;


    public void LoadItemDefinition(ItemDefinition itemDefinition)
    {
        loadedItem = itemDefinition;

        if (ItemImage != null)
            ItemImage.sprite = itemDefinition.DisplaySprite;

        if (ItemName != null)
            ItemName.text = itemDefinition.DisplayName;

        if (ItemDescription != null)
            ItemDescription.text = itemDefinition.DisplayDescription;

        if (ItemCostText != null)
            ItemCostText.text = itemDefinition.storeCost.ItemAmount.ToString();

        if (ItemCostIcon != null)
            ItemCostIcon.sprite = itemDefinition.storeCost.itemDefinition.DisplaySprite;

        if (ItemOwnedQuantity != null)
            ItemOwnedQuantity.text = GameManager.instance.Player.OwnedItems[itemDefinition].ToString();

        if(ItemOwnedMaxQuantity != null)
            ItemOwnedMaxQuantity.text = itemDefinition.MaxQuantityOwnable.ToString();

        ShowSelectionIndicator(false);
    }

    public void ShowSelectionIndicator(bool show)
    {
        if (SelectionIndicator != false)
            SelectionIndicator.gameObject.SetActive(show);
    }


    public void OnTap()
    {
        GetComponentInParent<UIPopup>().SelectItem(this);
    }

}
