using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class hoveringsides : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image materialImage;

    void Awake()
    {
        materialImage = GetComponent<Image>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        materialImage.color = new Color(materialImage.color.r, materialImage.color.g, materialImage.color.b, 1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        materialImage.color = new Color(materialImage.color.r, materialImage.color.g, materialImage.color.b, 0.5f);
    }
}
