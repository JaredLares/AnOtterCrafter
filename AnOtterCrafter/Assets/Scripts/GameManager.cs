using UnityEngine;

public class GameManager : MonoBehaviour, IGManager
{
#region Singleton
    public static GameManager Instance { get; private set; }

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
    [SerializeField] private Inventory inventory;

#endregion

#region InterfaceFuntions

    public void MaterialForID(int ID)
    {

    }

    public void SpriteForID(int ID)
    {

    }

    public void ShowHotbar()
    {

    }
    public void HideHotbar()
    {

    }

    public void ToggleTradeScreen()
    {

    }

    public void Maketrade()
    {

    }

    public void CancelTrade()
    {

    }

    public void AddMaterial(int ID, int amount)
    {
        inventory.AddItem(ID, amount);
    }

    public void SubtractMaterial(int ID, int amount)
    {
        inventory.RemoveItem(ID, amount);
    }

    public void UpdateBiome()
    {

    }

#endregion

#region GeneralFunctions



#endregion



}