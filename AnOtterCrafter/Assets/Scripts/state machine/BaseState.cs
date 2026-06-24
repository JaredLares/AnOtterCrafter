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
}
