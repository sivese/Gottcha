using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public partial class InteractionSystem : MonoBehaviour
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

        InitUiInstance();
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

public partial class InteractionSystem
{
    private Transform   examineUiRoot;
    private Image       examineImage;
    private TMP_Text    descriptionText;

    private bool examining = false;
    
    public bool IsExamining => examining;

    private void InitUiInstance()
    {
        examineUiRoot = GameObject
            .Find("canvas")
            .transform
            .Find("examine_ui");

        examineImage = examineUiRoot
            .Find("examine_image")
            .GetComponent<Image>();

        descriptionText = examineUiRoot
            .Find("description_text")
            .GetComponent<TMP_Text>();
    }

    public void ExamineItem(Item item)
    {
        if (examining)
        {
            examineUiRoot
                .gameObject
                .SetActive(false);

            examining = false;
            return;
        }

        examining = true;

        examineImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
        descriptionText.text = item.DescriptionText;
        
        examineUiRoot
            .gameObject
            .SetActive(true);
    }
}
