using UnityEngine;

[CreateAssetMenu(fileName = "InventoryState", menuName = "BaseState/InventoryState")]
public class InventoryState : BaseState
{
    public override void EnterState(GameManager stateMachine)
    {
        stateMachine.OpenInventory();
    }

    public override void ExitState(GameManager stateMachine)
    {
        
    }

    public override void UpdateState(GameManager stateMachine)
    {
        stateMachine.CancelTrade();
    }
}
