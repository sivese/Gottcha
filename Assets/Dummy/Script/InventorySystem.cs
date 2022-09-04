using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;
using UnityEngine.UI;

namespace dummy
{
    public class InventorySystem : MonoBehaviour
    {
        private List<GameObject> items = new();
        
        private bool isOpen;
        public bool IsOpen => isOpen;

        private Transform   inventoryUi;
        private Image[]     itemImages;

        private Transform   descriptionUi;
        private Image       descriptionImage;
        private Text        descriptionText;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                ToggleInventory();
            }
        }

        public void PickUp(GameObject item)
        {
            items.Add(item);
            UpdateUi();
        }

        private void ToggleInventory()
        {
            isOpen = !isOpen;
            inventoryUi.gameObject.SetActive(isOpen);
        }

        private void UpdateUi()
        {
            HideAll();
            var leng = itemImages.Length > items.Count ? items.Count : itemImages.Length;

            for(var i = 0; i < leng; i++)
            {
                itemImages[i].sprite = items[i]
                    .GetComponent<SpriteRenderer>()
                    .sprite;

                itemImages[i]
                    .gameObject
                    .SetActive(true);
            }
        }

        private void HideAll() 
        { 
            foreach (var i in itemImages) { i.gameObject.SetActive(false); } 
        }

        public void ShowDescription(int id)
        {
            descriptionImage.sprite = itemImages[id].sprite;
            descriptionText.text = items[id].name;

            descriptionImage.gameObject.SetActive(true);
            descriptionText.gameObject.SetActive(true);
        }

        public void HideDescription()
        {
            descriptionImage.gameObject.SetActive(false);
            descriptionText.gameObject.SetActive(false);
        }

        private void Awake()
        {
            inventoryUi = GameObject
                .Find("canvas")
                .transform
                .Find("inventory_ui");

            var panel = inventoryUi
                .Find("inventory_panel");

            var idx = 0;
            itemImages = new Image[panel.childCount];

            foreach (var ch in panel)
            {
                var go = ch as Transform;

                itemImages[idx] = go.GetChild(0).GetComponent<Image>();

                idx++;
            }
        }

        public void Consume(int id)
        {
            if (items[id].GetComponent<Item>().itemType == Item.ItemType.Consumable)
            {
                Debug.Log($"Consumed {items[id].name}");
                items[id].GetComponent<Item>().consumeEvent.Invoke();
                Destroy(items[id], 0.1f);
                items.RemoveAt(id);
            }
        }
    }
}