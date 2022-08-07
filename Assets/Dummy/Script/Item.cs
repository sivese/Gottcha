using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                    FindObjectOfType<InteractionSystem>().PickUpItem(gameObject);
                    gameObject.SetActive(false);
                    break;
                case InteractionType.Examine:
                    Debug.Log("EXAMINE");
                    break;
                default:
                    Debug.Log("NULL ITEM");
                    break;
            }
        }
    }
}