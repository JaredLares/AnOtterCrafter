using UnityEngine;

[CreateAssetMenu(fileName = "TradeState", menuName = "BaseState/TradeState")]
public class TradeState : BaseState
{
    public override void EnterState(GameManager stateMachine)
    {

        //stateMachine.OpenInventory();
        //stateMachine.ToggleTradeScreen();
    }

    public override void ExitState(GameManager stateMachine)
    {
        stateMachine.CancelTrade();
    }

    public override void UpdateState(GameManager stateMachine)
    {
        LeftMouseDown(stateMachine);
        LeftMouseUp(stateMachine);
        RightMouseDown(stateMachine);
        RightMouseUp(stateMachine);
    }
    
    public override void LeftMouseDown(GameManager stateMachine)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Slot hoveredSlot = GetHoveredSlot(stateMachine);
            if (hoveredSlot != null && hoveredSlot.HasSlot())
            {
                stateMachine.addPlayerTrade(hoveredSlot.HoldMaterial());
                hoveredSlot.RemoveMaterialAmount(1);
            }
        }
    }

    public override void LeftMouseUp(GameManager stateMachine)
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("left mouse up");
        }
    }

    public override void RightMouseDown(GameManager stateMachine)
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("right mouse down");
        }
    }

    public override void RightMouseUp(GameManager stateMachine)
    {
        if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("right mouse up");
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

}


