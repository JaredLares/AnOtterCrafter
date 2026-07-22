using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseState", menuName = "BaseState")]
public class BaseState : ScriptableObject
{
    public virtual void EnterState(GameManager stateMachine)
    {
        
    }

    public virtual void ExitState(GameManager stateMachine)
    {
        
    }

    public virtual void UpdateState(GameManager stateMachine)
    {
        
    }
    
    public virtual void LeftMouseDown(GameManager stateMachine)
    {
        
    }
    public virtual void RightMouseDown(GameManager stateMachine)
    {
        
    }

    public virtual void LeftMouseUp(GameManager stateMachine)
    {
        
    }

    public virtual void RightMouseUp(GameManager stateMachine)
    {
        
    }
}