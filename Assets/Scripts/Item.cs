using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    public enum InteractionType
    {
        None,
        PickUp,
        Examine
    }

    private const int ITEM_LAYER = 8; //static

    [SerializeField]
    private string descriptionText;

    public string DescriptionText => descriptionText;

    [SerializeField]
    private InteractionType type;
    
    [SerializeField]
    private UnityEvent itemEvent;

    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        gameObject.layer = ITEM_LAYER;
    }

    public void Interact()
    {
        switch(type)
        {
            case InteractionType.PickUp:
                Debug.Log("Picked item");
                FindObjectOfType<InventorySystem>().PickUp(gameObject);
                gameObject.SetActive(false);
                break;
            case InteractionType.Examine:
                /*
                 * Display item information
                 */
                Debug.Log("Examining item");
                FindObjectOfType<InteractionSystem>().ExamineItem(this);
                break;
            case InteractionType.None:
                Debug.Log("None");
                break;
            default:
                Debug.LogWarning("Undefined item type");
                break;
        }

        itemEvent.Invoke();
    }

    private void Awake()
    {

    }
}

public class Cherry : Item
{ 
    
}
