using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySystem : MonoBehaviour
{
    private readonly int INVEN_MAX_SIZE = 6;

    private List<GameObject> items;

    private Transform   inventoryRoot;
    private Image[]     itemImages;

    private TMP_Text    descriptionTitle;
    private Image       descriptionImage;
    private TMP_Text    descriptionText;

    public void PickUp(GameObject item)
    {
        items.Add(item);
    }

    private void Awake()
    {
        InitUiInstance();
    }

    private void Update()
    {

    }

    private void InitUiInstance()
    {
        inventoryRoot = GameObject
            .Find("Canvas")
            .transform
            .Find("inventory_ui");

        var descriptionRoot = inventoryRoot
            .Find("description");

        //descriptionTitle = ;
        //descriptionImage = ;
        //descriptionText = ;
    }
}
