using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dummy
{
    public class InteractionSystem : MonoBehaviour
    {
        private const string DETECTION_OBJECT = "interactionCheck";

        //Detection point, radius, layer
        private Transform detectionPoint;
        private LayerMask detectionLayer;
        private const float detectionRadius = 0.2f;

        private GameObject detectObject;
        private List<GameObject> pickedItems = new List<GameObject>();

        // Start is called before the first frame update
        void Awake()
        {
            detectionLayer = LayerMask.GetMask("Item");
            detectionPoint = transform.Find(DETECTION_OBJECT);
        }

        // Update is called once per frame
        void Update()
        {
            if (InteractInput() && DetectObject())
            {
                detectObject.GetComponent<Item>().Interact();
            }
        }

        private bool InteractInput() => Input.GetKeyDown(KeyCode.E);

        private bool DetectObject()
        {
            var detect = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer);
            
            if(detect == null)
            {
                detectObject = null;
                return false;
            }
            else
            {
                 detectObject = detect.gameObject;
                return true;
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (detectionPoint == null) return;

            Gizmos.color = Color.black;
            Gizmos.DrawSphere(detectionPoint.position, detectionRadius);
        }

        public void PickUpItem(GameObject item)
        {
            pickedItems.Add(item);
        }
    }

}