using UnityEngine;

[CreateAssetMenu(fileName = "MainState", menuName = "BaseState/MainState")]
public class MainState : BaseState
{
    public override void EnterState(GameManager stateMachine)
    {
        stateMachine.CloseInventory();
    }

    public override void ExitState(GameManager stateMachine)
    {
        
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
