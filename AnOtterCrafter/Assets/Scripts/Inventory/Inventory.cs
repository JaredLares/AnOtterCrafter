using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory : MonoBehaviour , Iinventory
{
    #region Variables
    // public
    public GameObject hotbarObject;
    public GameObject inventorySlotParent;
    public BaseState mainState;
    public BaseState craftState;
    public BaseState inventoryState;
    public BaseState tradeState;
    
    // private
    [SerializeField] private InventorySaver inventorySaver;
    private List<Slot> inventorySlots = new List<Slot>();
    private List<Slot> hotbarSlots = new List<Slot>();
    private List<Slot> allSlots = new List<Slot>();


    
    #endregion

    #region Unity Methods

    private void Awake()
    {
        hotbarSlots.AddRange(hotbarObject.GetComponentsInChildren<Slot>());
        allSlots.AddRange(hotbarSlots);
        inventorySlots.AddRange(inventorySlotParent.GetComponentsInChildren<Slot>());
        allSlots.AddRange(inventorySlots);
        
    }

    #endregion
    
    #region InterfaceFunctions

    public int GetAllMaterialsCount()
    {
        return inventorySaver.allMaterials.Count;
    }
    public void AddItem(int ID, int amount)
    {
        int remaining = amount;
        BaseMaterialSO tempMaterial = GetMaterialForID(ID);
        if (tempMaterial == null) return;
        foreach (var slot in allSlots)
        {
            if (slot.HasSlot() && slot.HoldMaterial() == tempMaterial)
            {
                int currentAmount = slot.MaterialAmount();
                int maxStack = tempMaterial.MaterialMaxAmount;
                if (currentAmount < maxStack)
                {
                    int spaceLeft = maxStack - currentAmount;
                    int amountToAdd = MinIntValue(spaceLeft, remaining);
                    slot.SetItem(tempMaterial, currentAmount + amountToAdd);
                    remaining -= amountToAdd;
                    if (remaining <= 0) return;
                }
            }
        }

        foreach (Slot slot in allSlots)
        {
            if (!slot.HasSlot())
            {
                int amountToAdd = MinIntValue(tempMaterial.MaterialMaxAmount, remaining);
                slot.SetItem(tempMaterial, amountToAdd);
                remaining -= amountToAdd;
                if(remaining <= 0) return;
            }
        }
        if (remaining > 0)
        {
            Debug.Log("inventario lleno");
        }
        
    }
    public void RemoveItem(int ID, int amount)
    {

    }

    public int GetItemAmount(int ID)
    {

        return 0;
    }

    public Sprite GetItemSprite(int ID)
    {
        foreach (var material in inventorySaver.allMaterials )
        {
            if (material.ID == ID)
            {
                return material.MaterialSprite;
            }
        }
        return null;
    }

    public BaseMaterialSO GetMaterialForID(int ID)
    {
        foreach (var material in inventorySaver.allMaterials )
        {
            if (material.ID == ID)
            {
                return material;
            }
        }
        return null;
    }
    #endregion
    
    #region Inventory Methods

    public int MinIntValue(int value1, int value2)
    {
        if (value1 < value2)
        {
            return value1;
        }
        else
        {
            return value2;
        }
    }

    public List<Slot> GetAllSlots()
    {
        return allSlots;
    }

    
    #endregion
}