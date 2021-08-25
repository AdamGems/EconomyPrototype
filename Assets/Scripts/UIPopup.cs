using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIPopup : MonoBehaviour
{
    public static UIPopup CurrentOpenPopup;
    public PopupType Type;

    [Header("Display Elements")]
    public GameObject root;
    public Transform itemDisplayContainer;
    public ItemDisplay ItemDisplayPrefab;
    List<ItemDisplay> ItemDisplayInstances;
    public ItemDisplay SelectedItemDetailedDisplay;
    ItemDisplay LastSelectedItemDisplay;
    public Button ConfirmSelectionButton;
    public UIPopupTabButton[] TabButtons;

    public ItemDisplay CostDisplayPrefab;
    public Transform CostsContainer;
    public ItemDisplay RewardDisplayPrefab;
    public Transform RewardsContainer;

    public enum PopupType { Reward, Store, Factory, Jobs, Map, Inventory }

    [Header("Store Settings")]
    public ItemDefinition.Type ItemTypeShown;
    public ItemDefinition.Tag ItemTagsShown;

    [Header("Job & Factory Settings")]
    public ItemDefinition[] RecipesOffered;
    public ItemDefinition[] RecipeQueue;
    public Slider RecipeProgressSlider;

    public void Awake()
    {
        ItemDisplayInstances = new List<ItemDisplay>();
        TabButtons = GetComponentsInChildren<UIPopupTabButton>();
        ItemDisplayPrefab.gameObject.SetActive(false);
        CostDisplayPrefab?.gameObject.SetActive(false);
        RewardDisplayPrefab?.gameObject.SetActive(false);

        if (ConfirmSelectionButton != null)
            ConfirmSelectionButton.interactable = false;

    }

    public void Start()
    {
        Close();
    }

    public void Close()
    {
        root.SetActive(false);
    }

    public void Open()
    {
        CurrentOpenPopup?.Close();
        CurrentOpenPopup = this;
        root.SetActive(true);
        ClearDisplays();

        switch (Type)
        {
            case PopupType.Reward:
                break;
            case PopupType.Store:
                ShowStore();
                break;
            case PopupType.Factory:
                ShowFactory();
                break;
            case PopupType.Map:
                break;
            case PopupType.Inventory:
                break;
            case PopupType.Jobs:
                ShowJobs();
                break;
        }
    }

    void ClearDisplays()
    {
        foreach (ItemDisplay display in ItemDisplayInstances)
            Destroy(display.gameObject);
        ItemDisplayInstances.Clear();
        ClearCostRewardDisplays();
    }

    void ClearCostRewardDisplays()
    {
        if (CostsContainer != null)
            foreach (ItemDisplay display in CostsContainer.GetComponentsInChildren<ItemDisplay>())
                Destroy(display.gameObject);
        if (RewardsContainer != null)
            foreach (ItemDisplay display in RewardsContainer.GetComponentsInChildren<ItemDisplay>())
                Destroy(display.gameObject);
    }

    void CreateItemDisplays(IEnumerable<ItemDefinition> itemDefinitions)
    {
        foreach (ItemDefinition itemDefinition in itemDefinitions)
        {
            ItemDisplay itemDisplayInstance = Instantiate(ItemDisplayPrefab);
            itemDisplayInstance.gameObject.SetActive(true);
            itemDisplayInstance.transform.SetParent(itemDisplayContainer);
            itemDisplayInstance.LoadItemDefinition(itemDefinition);
            ItemDisplayInstances.Add(itemDisplayInstance);
        }
        UpdateInteractability();
    }

    List<ItemDisplay> CreateItemDisplays(ItemQuantity[] Quantities, ItemDisplay DisplayPrefab, Transform Container)
    {
        DisplayPrefab.gameObject.SetActive(false);
        List<ItemDisplay> Displays = new List<ItemDisplay>();
        foreach (ItemQuantity itemQuantity in Quantities)
        {
            ItemDisplay itemDisplayInstance = Instantiate(DisplayPrefab);
            itemDisplayInstance.gameObject.SetActive(true);
            itemDisplayInstance.transform.SetParent(Container);
            itemDisplayInstance.LoadItemDefinition(itemQuantity.itemDefinition);
            if (itemDisplayInstance.QuantityText != null)
                itemDisplayInstance.QuantityText.text = itemQuantity.ItemAmount.ToString();
            Displays.Add(itemDisplayInstance);
        }

        UpdateInteractability();
        return Displays;
    }

    void UpdateInteractability()
    {
        //Update Displays Selectability
        /*switch (Type)
        {
            case PopupType.Store:
                foreach (ItemDisplay display in ItemDisplayInstances)
                    display.GetComponent<Button>().interactable = GameManager.instance.Player.CanAfford(display.loadedItem.storeCost);
                break;
            case PopupType.Factory:
                foreach (ItemDisplay display in ItemDisplayInstances)
                    display.GetComponent<Button>().interactable = GameManager.instance.Player.CanAfford(display.loadedItem.Ingredients);
                break;
            case PopupType.Jobs:
                foreach (ItemDisplay display in ItemDisplayInstances)
                    display.GetComponent<Button>().interactable = GameManager.instance.Player.CanAfford(display.loadedItem.Ingredients);
                break;
        }*/

        //Update Confirmation Button Selectbility
        bool canAfford = false;
        switch (Type)
        {
            case PopupType.Reward:
                canAfford = true;
                break;
            case PopupType.Store:
                canAfford = LastSelectedItemDisplay != null && GameManager.instance.Player.CanAfford(LastSelectedItemDisplay.loadedItem.storeCost);
                break;
            case PopupType.Factory:
                canAfford = LastSelectedItemDisplay != null && GameManager.instance.Player.CanAfford(LastSelectedItemDisplay.loadedItem.Ingredients);
                break;
            case PopupType.Jobs:
                canAfford = LastSelectedItemDisplay != null && GameManager.instance.Player.CanAfford(LastSelectedItemDisplay.loadedItem.Ingredients);
                break;
        }
        if (ConfirmSelectionButton != null)
            ConfirmSelectionButton.interactable = canAfford;
    }

    public void ShowStore()
    {
        CreateItemDisplays(GameManager.instance.itemManager.ItemDefinitions.Where(x => x.type == ItemTypeShown));
    }

    public void ShowFactory()
    {
        CreateItemDisplays(RecipesOffered);
    }

    public void ShowJobs()
    {
        CreateItemDisplays(RecipesOffered);
    }


    public void SelectItem(ItemDisplay itemDisplay)
    {
        if (LastSelectedItemDisplay != null)
            LastSelectedItemDisplay.ShowSelectionIndicator(false);
        itemDisplay.ShowSelectionIndicator(true);
        LastSelectedItemDisplay = itemDisplay;

        SelectedItemDetailedDisplay.LoadItemDefinition(itemDisplay.loadedItem);
        UpdateInteractability();

        //Cost Reward Displays
        ClearCostRewardDisplays();

        if (CostsContainer != null)
            CreateItemDisplays(itemDisplay.loadedItem.Ingredients, CostDisplayPrefab, CostsContainer);

        if (RewardsContainer != null)
        {
            CreateItemDisplays(itemDisplay.loadedItem.BuildRewards, RewardDisplayPrefab, RewardsContainer);
            if(Type == PopupType.Factory) //could delete this if we don't have a special RecipeResultAmount
                CreateItemDisplays(new ItemQuantity[] { new ItemQuantity(itemDisplay.loadedItem, itemDisplay.loadedItem.RecipeResultAmount) },
                    RewardDisplayPrefab, RewardsContainer);
        }
    }

    public void ConfirmSelection()
    {
        switch (Type)
        {
            case PopupType.Reward:
                Close();
                break;
            case PopupType.Store:
                ConfirmStoreSelection();
                break;
            case PopupType.Factory:
                ConfirmFactorySelection();
                break;
            case PopupType.Jobs:
                ConfirmJobSelection();
                break;
            case PopupType.Inventory:
                break;
        }

        UpdateInteractability();
    }

    void ConfirmStoreSelection()
    {
        GameManager.instance.Player.Pay(LastSelectedItemDisplay.loadedItem.storeCost);
        GameManager.instance.Player.Gain(LastSelectedItemDisplay.loadedItem, LastSelectedItemDisplay.loadedItem.purchaseQuantity);
    }

    void ConfirmFactorySelection()
    {
        foreach (ItemQuantity cost in LastSelectedItemDisplay.loadedItem.Ingredients)
            GameManager.instance.Player.Pay(cost);

        GameManager.instance.Player.Gain(LastSelectedItemDisplay.loadedItem, LastSelectedItemDisplay.loadedItem.RecipeResultAmount);

        foreach (ItemQuantity bonus in LastSelectedItemDisplay.loadedItem.BuildRewards)
            GameManager.instance.Player.Gain(bonus);
    }

    void ConfirmJobSelection()
    {
        foreach (ItemQuantity cost in LastSelectedItemDisplay.loadedItem.Ingredients)
            GameManager.instance.Player.Pay(cost);

        foreach (ItemQuantity bonus in LastSelectedItemDisplay.loadedItem.BuildRewards)
            GameManager.instance.Player.Gain(bonus);
    }

    public void SellSelectedItem()
    {

    }

}