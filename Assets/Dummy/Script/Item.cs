using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace dummy
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Item : MonoBehaviour
    {

        public enum InteractionType
        {
            NONE,
            PickUp,
            Examine,
        }

        [SerializeField]
        private InteractionType type;

        [Header("Examine")]
        [SerializeField]
        private string descriptionText;

        public string DescriptionText => descriptionText;

        [Header("Custom Event")]
        public UnityEvent customEvent;

        private const int ITEM_LAYER = 8;

        private void Reset()
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.layer = ITEM_LAYER;
        }

        public void Interact()
        {
            switch(type)
            {
                case InteractionType.NONE:
                    break;
                case InteractionType.PickUp:
                    Debug.Log("PICK UP");
                    FindObjectOfType<InventorySystem>().PickUp(gameObject); // Find object with type
                    gameObject.SetActive(false);
                    break;
                case InteractionType.Examine:
                    /*
                        Display some images
                    */
                    Debug.Log("EXAMINE");
                    FindObjectOfType<InteractionSystem>().ExamineItem(this);
                    break;
                default:
                    Debug.Log("NULL ITEM");
                    break;
            }

            customEvent.Invoke();
        }
    }
}