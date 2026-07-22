using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "InventoryState", menuName = "BaseState/InventoryState")]
public class InventoryState : BaseState
{
    #region variables
    private Slot draggedSlot = null;
    private bool isDragging = false;
    public Image dragIcon;
    #endregion
    
    public override void EnterState(GameManager stateMachine)
    {
        dragIcon = stateMachine.dragIcon;
        stateMachine.OpenInventory();
    }

    public override void ExitState(GameManager stateMachine)
    {
        
    }

    public override void UpdateState(GameManager stateMachine)
    {
        StartDrag(stateMachine);
        UpdateDragIconPosition();
        EndDrag(stateMachine);

    }
    
    public override void LeftMouseDown(GameManager stateMachine)
    {

    }

    public override void LeftMouseUp(GameManager stateMachine)
    {
        
    }

    public override void RightMouseDown(GameManager stateMachine)
    {
        
    }

    public override void RightMouseUp(GameManager stateMachine)
    {
        
    }
    
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
    
    private void StartDrag(GameManager stateMachine)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Slot hoveredSlot = GetHoveredSlot(stateMachine);
            if (hoveredSlot != null && hoveredSlot.HasSlot())
            {
                draggedSlot = hoveredSlot;
                isDragging = true;
                dragIcon.sprite = hoveredSlot.HoldMaterial().MaterialSprite;
                dragIcon.color = new Color(1, 1, 1, 0.5f);
                dragIcon.enabled = true;
            }
        }
    }

    private void EndDrag(GameManager stateMachine)
    {
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
             Slot hoveredSlot = GetHoveredSlot(stateMachine);
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
            int max = to.HoldMaterial().MaterialMaxAmount;
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
    
    private Slot GetHoveredSlot(GameManager stateMachine)
    {
        foreach (var slots in stateMachine.Inventory().GetAllSlots())
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
}
