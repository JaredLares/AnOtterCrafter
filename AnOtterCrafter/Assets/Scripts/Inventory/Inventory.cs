using UnityEngine;

public class Inventory : MonoBehaviour , Iinventory
{
    #region Singleton
    public static Inventory Instance { get; private set; }
        private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

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
    #endregion
}
