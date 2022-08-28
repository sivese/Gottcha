using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

namespace dummy
{
    public class InteractionSystem : MonoBehaviour
    {
        private const string DETECTION_OBJECT = "interactionCheck";

        //Detection point, radius, layer
        private Transform detectionPoint;
        private LayerMask detectionLayer;
        private const float detectionRadius = 0.2f;

        /* Examine systems */
        private Transform   uiRoot;
        private GameObject  examineWindow;
        private TMP_Text    examineText;
        private Image       examineImage;

        private bool examining = false;
        public bool IsExamining => examining;

        private GameObject detectObject;
        private List<GameObject> pickedItems = new List<GameObject>();

        // Start is called before the first frame update
        void Awake()
        {
            detectionLayer = LayerMask.GetMask("Item");
            detectionPoint = transform.Find(DETECTION_OBJECT);

            InitUiIsntance();
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

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(detectionPoint.position, detectionRadius);
        }

        private void InitUiIsntance()
        {
            uiRoot = GameObject
                .Find("/canvas")
                .transform;

            examineWindow   = uiRoot
                .Find("examine_ui")
                .gameObject;
            
            examineImage = examineWindow
                .transform
                .Find("item_image")
                .GetComponent<Image>();

            examineText = examineWindow
                .transform
                .Find("item_description")
                .GetComponent<TMP_Text>();

            if (examineText == null) Debug.Log("null text!");
        }

        public void ExamineItem(Item item)
        {
            if(examining)
            {
                examineWindow.SetActive(false);
                examining = false;
                return;
            }

            examineImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
            examineText.text = item.DescriptionText;
            examineWindow.SetActive(true);

            examining = true;
        }

        //public bool Freeze()
        //{

        //}
    }

}