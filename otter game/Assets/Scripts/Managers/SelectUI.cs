using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class SelectUI : MonoBehaviour
{
    [SerializeField] private LayerMask UILayer;
    [SerializeField] private string UITag;

    void Update()
    { 
        
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            if (results.Count > 0)
            {
                for (int i = 0; i < results.Count; i++)
                {
                    if (results[i].gameObject.CompareTag(UITag))
                    {
                        hotbarItem item = results[i].gameObject.GetComponent<hotbarItem>();
                        if (item)
                        {
                            GameManager.Instance.LoadMaterial(item.GetID());
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
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
        }
    }
}
