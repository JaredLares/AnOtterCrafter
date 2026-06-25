using UnityEngine;
[CreateAssetMenu(fileName = "CraftState", menuName = "BaseState/CraftState")]
public class CraftState : BaseState
{
    public override void EnterState(GameManager stateMachine)
    {
        
    }

    public override void ExitState(GameManager stateMachine)
    {
        
    }

    public override void UpdateState(GameManager stateMachine)
    {
        stateMachine.CancelTrade();
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
