using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Dictionary<int, int> scaleValues = new Dictionary<int, int>();
    private Dictionary<int, int> animalScaleValues = new Dictionary<int, int>();
    [SerializeField] private InventoryStructure inventoryStructure;
    [SerializeField] private int internalValue = 0;
    [SerializeField] private int animalInternalValue;
    [SerializeField] private AnimalsBiomeList actualBiome;
    [SerializeField] private GameObject actualAnimal;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private bool isTrading = false;
    [SerializeField] private GameObject tradeCamera;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SpawnAnimal();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnAnimal();
        }
    }

    public void TradeScreen()
    {
        tradeCamera.SetActive(true);
        UIManager.Instance.TradingScreen();
        isTrading = true;
    }

    public void ConsumerScreen()
    {
        tradeCamera.SetActive(false);   
        isTrading = true;
    }
    public void LoadMaterial(int MaterialID)
    {
        if(actualAnimal == null) return;
        if (inventoryStructure.InventoryAmount(MaterialID) > 0)
        {
            if(scaleValues.TryGetValue(MaterialID, out var value))
            {
                value++;
                scaleValues[MaterialID] = value;
            }
            else
            {
                scaleValues[MaterialID] = 1;
            }
            InventoryManager.Instance.RemoveFromInventory(MaterialID,1);
            UpdateInternalValue();
        }
    }
    
    public void UnloadMaterial(int MaterialID)
    {
        if(actualAnimal == null) return;
        
        if (scaleValues.TryGetValue(MaterialID, out var value))
        {
            value--;
            if(value == 0)
            {
                scaleValues.Remove(MaterialID);
            }
            else
            {
                scaleValues[MaterialID] = value;
            }
            InventoryManager.Instance.AddToInventory(MaterialID,1);
        }
        UpdateInternalValue();
    }
    public void ResetPlayerDictionary()
    {
        scaleValues = new Dictionary<int, int>();
    }

    public void ResetAnimalDictionary()
    {
        animalScaleValues = new Dictionary<int, int>();
    }
    public void UpdateInternalValue()
    {
        int tempValue;
        internalValue = 0;
        foreach(var values in scaleValues)
        {
            
            Debug.Log(values);
        }

        for(int i = 1;i<=inventoryStructure.AllmaterialsCount() ;i++)
        {
            if(scaleValues.TryGetValue(i, out int amount))
            {
                tempValue = inventoryStructure.AllMaterialsCost(i) * amount;
                internalValue += tempValue;
            }
        }
    }
    public void SpawnAnimal()
    {
        if (actualAnimal != null)
        {
            animalInternalValue = 0;
            actualAnimal.TryGetComponent<AnimalManager>(out AnimalManager animal);
            actualAnimal = null;
            animal.Destroy();
        }

        GameObject temp = actualBiome.ReturnAnimal();
        actualAnimal = Instantiate(temp,spawnPoint.transform);
        AnimalTrade();
    }
    public void AnimalTrade()
    {
        ResetAnimalDictionary();
        // get animal manager
        if (actualAnimal.TryGetComponent<AnimalManager>(out AnimalManager animal))
        {
            // get animal preferences
            BaseAnimalPReferences animalpreferences = animal.Preferences;
            int tradeTemporal = Random.Range(animalpreferences.MinTradeItems,animalpreferences.MaxTradeItems + 1);
            for (int i = 0; i < tradeTemporal; i++)
            {
                int temporalID = animalpreferences.RandomIDTrade();
                animal.AddToDialgue(inventoryStructure.ReturnMaterialFromID(temporalID));
                if(animalScaleValues.TryGetValue(temporalID, out int materialId))
                {
                    materialId++;
                    animalScaleValues[temporalID] = materialId;
                }
                else
                {
                    animalScaleValues.Add(temporalID,1);
                }
            }
        }
        AnimalScaleValue();
        UIManager.Instance.PrepareTrade();
    }
    public void AnimalScaleValue()
    {
        int tempValue = 0;
        for(int i = 1;i<=inventoryStructure.AllmaterialsCount() ;i++)
        {
            if(animalScaleValues.TryGetValue(i, out int amount))
            {
                tempValue = inventoryStructure.AllMaterialsCost(i) * amount;
                animalInternalValue += tempValue;
            }
        }

        if (actualAnimal.TryGetComponent<AnimalManager>(out AnimalManager animal))
        {
            animal.VisualizeDialogue();
        }
    }

    public void TradingSwitch()
    {
        
    }
}