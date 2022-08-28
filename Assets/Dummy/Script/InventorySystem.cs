using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;
using UnityEngine.UI;

namespace dummy
{
    public class InventorySystem : MonoBehaviour
    {
        private List<GameObject> items;
        
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
            inventoryUi.gameObject.SetActive(true);
        }

        private void UpdateUi()
        {
            HideAll();
            var leng = itemImages.Length;

            foreach(var i in Enumerable.Range(0, leng))
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
        
        }
    }
}