using UnityEngine;

[CreateAssetMenu(fileName = "TradeState", menuName = "BaseState/TradeState")]
public class TradeState : BaseState
{
    public override void EnterState(GameManager stateMachine)
    {
        stateMachine.OpenInventory();
        stateMachine.ToggleTradeScreen();
    }

    public override void ExitState(GameManager stateMachine)
    {
        stateMachine.CancelTrade();
    }

    public override void UpdateState(GameManager stateMachine)
    {
        
    }
    
    public override void LeftMouseDown()
    {

    }

    public override void LeftMouseUp()
    {
        
    }

    public override void RightMouseDown()
    {
        
    }

    public override void RightMouseUp()
    {
        
    }
}
