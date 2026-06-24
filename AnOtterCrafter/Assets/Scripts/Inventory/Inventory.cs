using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory : MonoBehaviour , Iinventory
{
    #region Variables

    // public
    public GameObject hotbarObject;
    public GameObject inventorySlotParent;
    public Image dragIcon;
    public BaseState mainState;
    public BaseState craftState;
    public BaseState inventoryState;
    public BaseState tradeState;
    
    // private
    [SerializeField] private InventorySaver inventorySaver;
    private List<Slot> inventorySlots = new List<Slot>();
    private List<Slot> hotbarSlots = new List<Slot>();
    private List<Slot> allSlots = new List<Slot>();
    private Slot draggedSlot = null;
    private bool isDragging = false;

    
    #endregion

    #region Unity Methods

    private void Awake()
    {
        hotbarSlots.AddRange(hotbarObject.GetComponentsInChildren<Slot>());
        allSlots.AddRange(hotbarSlots);
        inventorySlots.AddRange(inventorySlotParent.GetComponentsInChildren<Slot>());
        allSlots.AddRange(inventorySlots);
        
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
        StartDrag();
        UpdateDragIconPosition();
        EndDrag();
    }

    #endregion
    
    #region InterfaceFuntions
    public void AddItem(int ID, int amount)
    {
        int remainig = amount;
        BaseMaterialSO tempMaterial = GetMaterialForID(ID);
        if (tempMaterial == null) return;
        foreach (var slot in allSlots)
        {
            if (slot.HasSlot() && slot.HoldMaterial() == tempMaterial)
            {
                int currentAmount = slot.MaterialAmount();
                int maxStack = tempMaterial.materialMaxAmount;
                if (currentAmount < maxStack)
                {
                    int spaceLeft = maxStack - currentAmount;
                    int amountToAdd = MinIntValue(spaceLeft, remainig);
                    slot.SetItem(tempMaterial, currentAmount + amountToAdd);
                    remainig -= amountToAdd;
                    if (remainig <= 0) return;
                }
            }
        }

        foreach (Slot slot in allSlots)
        {
            if (!slot.HasSlot())
            {
                int amountToAdd = MinIntValue(tempMaterial.materialMaxAmount, remainig);
                slot.SetItem(tempMaterial, amountToAdd);
                remainig -= amountToAdd;
                if(remainig <= 0) return;
            }
        }
        if (remainig > 0)
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
        return null;
    }

    public BaseMaterialSO GetMaterialForID(int ID)
    {
        foreach (var material in inventorySaver.allMaterials )
        {
            if (material.id == ID)
            {
                return material;
            }
        }
        return null;
    }
    #endregion
    
    #region Inventory Methods

    private int MinIntValue(int value1, int value2)
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

    private void StartDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Slot hoveredSlot = GetHoveredSlot();
            if (hoveredSlot != null && hoveredSlot.HasSlot())
            {
                draggedSlot = hoveredSlot;
                isDragging = true;

                dragIcon.sprite = hoveredSlot.HoldMaterial().materialSprite;
                dragIcon.color = new Color(1, 1, 1, 0.5f);
                dragIcon.enabled = true;
            }
        }
    }

    private void EndDrag()
    {
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
             Slot hoveredSlot = GetHoveredSlot();
             if (hoveredSlot != null )
             {
                   HanddleDrop(draggedSlot, hoveredSlot);
                   
                   dragIcon.enabled = false;
                   draggedSlot = null;
                   isDragging = false;
             }
        }
    }

    private void HanddleDrop(Slot from , Slot to)
    {
        if(from == to) return;
        // stacking same item
        if (to.HasSlot() && to.HoldMaterial() == from.HoldMaterial())
        {
            int max = to.HoldMaterial().materialMaxAmount;
            int space = max - to.MaterialAmount();

            if (space > 0)
            {
                  int move = MinIntValue(space, from.MaterialAmount());
                  to.SetItem(to.HoldMaterial(), to.MaterialAmount() + move);
                  from.SetItem(from.HoldMaterial(), from.MaterialAmount() - move);
                  if(from.MaterialAmount() <= 0) from.ClearSlot();
                  return;
            }
        }
        // replace diferent item
        if(to.HasSlot())
        {
            BaseMaterialSO tempMaterial = to.HoldMaterial();
            int temporalAmount = to.MaterialAmount();

            to.SetItem(from.HoldMaterial(), from.MaterialAmount());
            from.SetItem(tempMaterial, temporalAmount);
            return;
        }
        // move to empty slot
        if (!to.HasSlot())
        {
            to.SetItem(from.HoldMaterial(), from.MaterialAmount());
            from.ClearSlot();
        }
    }
    
    private Slot GetHoveredSlot()
    {
        foreach (var slots in allSlots)
        {
            if (slots.hovering)
            {
                return slots;
            }
        }
        return null;
    }

    private void UpdateDragIconPosition()
    {
        if (isDragging)
        {
            Vector2 mousePos = Input.mousePosition;
            dragIcon.transform.position = mousePos;
        }
    }
    #endregion
}