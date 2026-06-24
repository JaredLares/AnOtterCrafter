using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
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
    public BaseState initialState;
    public BaseState currentState;
    

    // private
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject tradeCamera;
    [SerializeField] private GameObject inventoryCamera;
    [SerializeField] private GameObject craftingCamera;
    [SerializeField] private GameObject globalInventory;
    
#endregion

#region UnityFunctions

    private void Start()
    {
        StartStateMachine();
    }

    private void Update()
    {
        UpdateStateMachine();
    }
    
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

#region Camera Controllers

    public void TradingScene()
    {
        ChangeState(inventory.tradeState);
        globalInventory.SetActive(false);
        mainCamera.SetActive(false);
        tradeCamera.SetActive(true);
        inventoryCamera.SetActive(false);
        craftingCamera.SetActive(false);
    }
    
    public void MainScene()
    {        
        ChangeState(inventory.mainState);
        globalInventory.SetActive(false);
        inventoryCamera.SetActive(false);
        craftingCamera.SetActive(false);
        mainCamera.SetActive(true);
        tradeCamera.SetActive(false);
    }
    
    public void InventoryScene()
    {
        ChangeState(inventory.inventoryState);
        inventoryCamera.SetActive(true);
        craftingCamera.SetActive(false);
        mainCamera.SetActive(false);
        tradeCamera.SetActive(false);
        StartCoroutine(InventoryCouroutine());
    }
    
    public void CraftingScene()
    {
        ChangeState(inventory.craftState);
        globalInventory.SetActive(false);
        inventoryCamera.SetActive(false);
        craftingCamera.SetActive(true);
        mainCamera.SetActive(false);
        tradeCamera.SetActive(false);
    }

    public bool MainCameraActive()
    {
        return mainCamera.activeInHierarchy;
    }   
    public bool CraftingCameraActive()
    {
        return craftingCamera.activeInHierarchy;
    }

#endregion


#region StateMachine

    private void StartStateMachine()
    {
        currentState = initialState;
        currentState.EnterState(this);
    }

    private void UpdateStateMachine()
    {
        currentState.UpdateState(this);
    }

    public void ChangeState(BaseState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }
#endregion

#region courutines

    IEnumerator InventoryCouroutine()
    {
        yield return new WaitForSeconds(1);
        if (!mainCamera.activeInHierarchy && !craftingCamera.activeInHierarchy)
        {
            globalInventory.SetActive(true);
        }
        yield return null;
    }


#endregion
}