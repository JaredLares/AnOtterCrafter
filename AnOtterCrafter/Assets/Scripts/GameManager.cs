using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    public Image dragIcon;
    public bool isTrading;
    public GameObject actualAnimal;
    public Biomes biome;
    
    // private
    [SerializeField] private int internalValue;
    [SerializeField] private List<BaseMaterialSO> playerTradeMaterials;
    [SerializeField] private int animalInternalValue;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Scale scale;
    [SerializeField] private UI gameUI;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject tradeCamera;
    [SerializeField] private GameObject inventoryCamera;
    [SerializeField] private GameObject craftingCamera;
    [SerializeField] private GameObject globalInventory;
    [SerializeField] private GameObject tradingButton;
    [SerializeField] private GameObject tradingOptions;

    [SerializeField] private Transform SpawnPos;
    
#endregion

#region UnityFunctions

    
    private void Start()
    {
        int randomGenerator = Random.Range(3, 10);
        Debug.Log(randomGenerator);
        for (int i = 0; i < randomGenerator; i++)
        {   
            AddMaterial(Random.Range(1,7),Random.Range(1,5));
        }
        StartStateMachine();
    }

    private void Update()
    {
        UpdateStateMachine();
    }
    
#endregion

#region Getters and Setters

    public int InternalValue
    {
        get { return internalValue; }
    }

    public int AnimalInternalValue
    {
        get { return animalInternalValue; }
    }
#endregion

#region InterfaceFuntions

    public BaseMaterialSO MaterialForID(int ID)
    {
        return inventory.GetMaterialForID(ID);
    }


    public Sprite SpriteForID(int ID)
    {
        return inventory.GetItemSprite(ID);
    }

    public void ToggleTradeScreen()
    {
        TradingScene();
        tradingButton.SetActive(false);
        StartCoroutine(TradeCouroutine());
        scale.SpawnAnimalMaterial(actualAnimal.GetComponent<AnimalManager>().GetTradeDictionary());
    }

    public void MakeTrade()
    {
        if(tradingOptions.activeInHierarchy)
        {
            tradingOptions.SetActive(false);
        }
        scale.resetDictionaries();
        MainScene();

    }

    public void CancelTradeScene()
    {
        if(tradingOptions.activeInHierarchy)
        {
            tradingOptions.SetActive(false);
        }
        scale.resetDictionaries();
        MainScene();
    }

    public void CancelTrade()
    {
        if(tradingOptions.activeInHierarchy)
        {
            tradingOptions.SetActive(false);
        }
        scale.resetDictionaries();
        if (actualAnimal != null)
        {
            actualAnimal.GetComponent<AnimalManager>().Destroy();
        }
        actualAnimal = null;
        isTrading = false;
    }

    public void AddMaterial(int ID, int amount)
    {
        inventory.AddItem(ID, amount);
    }

    public void SubtractMaterial(int ID, int amount)
    {
        inventory.RemoveItem(ID, amount);
    }

    public void UpdateBiome(Biomes newBiomes)
    {
        biome = newBiomes;
    }

    public void StartTrade()
    {
        actualAnimal = Instantiate(biome.GetRandomAnimal(),SpawnPos);
        actualAnimal.GetComponent<AnimalManager>().createTrade();
        tradeButtonCouroutine();
    }

    public Dictionary<int, int> GetAnimalTradeDictionary()
    {
        return null;
    }

    public void GetAnimalInternalValue(int internalValue)
    {
        animalInternalValue = internalValue;
    }

    public void addPlayerTrade(BaseMaterialSO material)
    {
        playerTradeMaterials.Add(material);
    }

#endregion

    #region Camera Controllers

    public void TradingScene()
    {
        scale.resetDictionaries();
        ChangeState(inventory.tradeState);
        globalInventory.SetActive(false);
        mainCamera.SetActive(false);
        tradeCamera.SetActive(true);
        inventoryCamera.SetActive(false);
        craftingCamera.SetActive(false);
        OpenInventory();
    }
    
    public void MainScene()
    {        
        ChangeState(inventory.mainState);
        globalInventory.SetActive(false);
        inventoryCamera.SetActive(false);
        craftingCamera.SetActive(false);
        mainCamera.SetActive(true);
        tradeCamera.SetActive(false);
        StartCoroutine(tradeButtonCouroutine());
        scale.resetDictionaries();

    }
    
    public void InventoryScene()
    {
        ChangeState(inventory.inventoryState);
        inventoryCamera.SetActive(true);
        craftingCamera.SetActive(false);
        mainCamera.SetActive(false);
        tradeCamera.SetActive(false);
        StartCoroutine(InventoryCouroutine());
        tradingButton.SetActive(false);
        scale.resetDictionaries();

    }
    
    public void CraftingScene()
    {
        ChangeState(inventory.craftState);
        globalInventory.SetActive(false);
        inventoryCamera.SetActive(false);
        craftingCamera.SetActive(true);
        mainCamera.SetActive(false);
        tradeCamera.SetActive(false);
        tradingButton.SetActive(false);
        tradingOptions.SetActive(false);
        scale.resetDictionaries();

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

    public void CloseInventory()
    {
        gameUI.CloseHotbar();
    }

    public void OpenInventory()
    {
        gameUI.OpenHotbar();
    }

    public Inventory Inventory()
    {
        return inventory;
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
    IEnumerator TradeCouroutine()
    {
        yield return new WaitForSeconds(.9f);
        if (!mainCamera.activeInHierarchy && !craftingCamera.activeInHierarchy && !inventoryCamera.activeInHierarchy)
        {
            tradingOptions.SetActive(true);
        }
        yield return null;
    }
    IEnumerator tradeButtonCouroutine()
    {
        yield return new WaitForSeconds(.9f);
        if (!tradeCamera.activeInHierarchy && !craftingCamera.activeInHierarchy && !inventoryCamera.activeInHierarchy)
        {
            tradingButton.SetActive(true);
        }
        yield return null;
    }
    
#endregion
}