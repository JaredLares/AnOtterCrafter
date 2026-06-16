using UnityEngine;

public class Inventory : MonoBehaviour , Iinventory
{
    #region Variables

    // public

    // private

    #endregion

    #region InterfaceFuntions
    public void AddItem(int ID, int amount)
    {

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
    #endregion
}
