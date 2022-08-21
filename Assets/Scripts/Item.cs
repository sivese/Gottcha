using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private InteractionType type;


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
                break;
            case InteractionType.Examine:
                Debug.Log("Examining item");
                break;
            case InteractionType.None:
                Debug.Log("None");
                break;
            default:
                Debug.LogWarning("Undefined item type");
                break;
        }
    }

    private void Awake()
    {

    }
}

public class Cherry : Item
{ 
    
}
