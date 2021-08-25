using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICurrencyCounter : MonoBehaviour
{
    public ItemDefinition CurrencyType;
    public Image IconImage;
    public Text QuantityText;

    // Start is called before the first frame update
    void Start()
    {
        IconImage.sprite = CurrencyType.DisplaySprite;
    }

    // Update is called once per frame
    void Update()
    {
        QuantityText.text = GameManager.instance.Player.OwnedItems[CurrencyType].ToString();

    }
}
