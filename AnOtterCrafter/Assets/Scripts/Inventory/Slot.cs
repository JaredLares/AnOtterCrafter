using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region Variables
    // publicas
    public bool hovering;
    
    
    // privadas
    private BaseMaterialSO holdMaterial;
    private int materialAmount;
    private Image materialImage;
    private TextMeshProUGUI materialAmountText;
    #endregion

    #region unity methods

    private void Awake()
    {
        materialImage = transform.GetChild(0).GetComponent<Image>();
        materialAmountText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    #endregion

    #region Interfaces

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;

    }
    #endregion
    
    #region logic methods

    public BaseMaterialSO HoldMaterial()
    {
        return holdMaterial;
    }

    public int MaterialAmount()
    {
        return materialAmount;
    }

    public void SetItem(BaseMaterialSO material,int amount = 1)
    {
        holdMaterial = material;
        materialAmount = amount;
        
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        if (holdMaterial != null)
        {
            materialImage.enabled = true;
            materialImage.sprite = holdMaterial.materialSprite;
            materialAmountText.text = materialAmount.ToString();
        }
        else
        {
            materialImage.enabled = false;
            materialAmountText.text = "";
        }
    }

    public int AddMaterialAmount(int amount)
    {
        materialAmount += amount;
        UpdateSlot();
        return materialAmount;
    }

    public int RemoveMaterialAmount(int amount)
    {
        materialAmount -= amount;
        if (materialAmount <= 0)
        {
            ClearSlot();
        }
        else
        {
            UpdateSlot();
        }
        return materialAmount;
    }

    public void ClearSlot()
    {
        holdMaterial = null;
        materialImage.enabled = false;
        materialAmountText.text = "";
    }

    public bool HasSlot()
    {
        return holdMaterial != null;
    }
    
    #endregion
}
