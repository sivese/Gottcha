using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    private const string DETECTION_OBJECT = "interactionSystem";
    private const string ITEM_LAYER = "Item";

    private const float DETECTION_RADIUS = 0.2f;

    private Transform detectionPoint;
    private LayerMask detectionLayer;

    private List<GameObject> pickedItems = new List<GameObject>();
    private GameObject detectedObject;

    private void Awake()
    {
        detectionPoint = transform.Find(DETECTION_OBJECT);
        detectionLayer = LayerMask.GetMask(ITEM_LAYER);
    }

    private void Update()
    {
        if (InteractInput() && DetectItem())
        {
            detectedObject.GetComponent<Item>().Interact();
        }
    }
    
    private bool InteractInput() => Input.GetKeyDown(KeyCode.E);

    private bool DetectItem()
    {
        var detect = Physics2D.OverlapCircle(detectionPoint.position, DETECTION_RADIUS, detectionLayer);
        
        if(detect == null)
        {
            detectedObject = null;
            return false;
        }
        else 
        {
            detectedObject = detect.gameObject;
            return true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (detectionPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(detectionPoint.position, DETECTION_RADIUS);
    }
}

