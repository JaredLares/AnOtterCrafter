using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class SelectUI : MonoBehaviour
{
    [SerializeField] private LayerMask UILayer;
    [SerializeField] private LayerMask BubbleLayer;
    [SerializeField] private string UITag;
    [SerializeField] private string bubbleTag;

    void Update()
    { 
        // click Derecho
        if (Input.GetMouseButtonDown(0))
        {
            // revisar raycast en ui
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            if (results.Count > 0)
            {
                for(int i = 0;i < results.Count;i++)
                {
                    if (results[i].gameObject.CompareTag(UITag))
                    {
                        if (results[i].gameObject.GetComponent<hotbarItem>())
                        {   
                            GameManager.Instance.LoadMaterial(results[i].gameObject.GetComponent<hotbarItem>().GetID());
                        }
                    }
                }
            }
            // revisar raycast in game
            
            
            
        }
        // click izquierdo
        if (Input.GetMouseButtonDown(1))
        {
            // raycast en ui
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            if (results.Count > 0)
            {
                for(int i = 0;i < results.Count;i++)
                {
                    if (results[i].gameObject.CompareTag(UITag))
                    {
                        if (results[i].gameObject.GetComponent<hotbarItem>())
                        {
                            GameManager.Instance.UnloadMaterial(results[i].gameObject.GetComponent<hotbarItem>().GetID());

                        }
                    }
                }
            }
            // raycast in game
            
            
            
        }
    }
}
