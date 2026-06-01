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

    #region Player Options And Funtions
    
    // player options an funtions
    public void TradeScreen()
    {
        tradeCamera.SetActive(true);
        UIManager.Instance.TradingScreen();
        isTrading = true;
    }

    public void ConsumerScreen()
    {
        tradeCamera.SetActive(false);   
        isTrading = false;
    }
    public void LoadMaterial(int MaterialID)
    {
        if(actualAnimal == null || isTrading == false) return;
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
        if(actualAnimal == null || isTrading == false) return;
        
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
    
    public void UpdateInternalValue()
    {
        int tempValue;
        internalValue = 0;
        for(int i = 1;i<=inventoryStructure.AllmaterialsCount() ;i++)
        {
            if(scaleValues.TryGetValue(i, out int amount))
            {
                tempValue = inventoryStructure.AllMaterialsCost(i) * amount;
                internalValue += tempValue;
            }
        }
    }

    public void MakeTrade()
    {
        if (internalValue > animalInternalValue)
        {
            Debug.Log("trade complete");
            AddNewMaterial();
        }
        else
        {
            Debug.Log("canceltrade");
            ReturnMaterials();
        }
    }
    public void ReturnMaterials()
    {
        if (scaleValues.Count > 0)
        {
            foreach (var material in scaleValues)
            {
                InventoryManager.Instance.AddToInventory(material.Key,material.Value);
            }
        }
    }

    public void AddNewMaterial()
    {
        if (animalScaleValues.Count > 0)
        {
            foreach (var material in animalScaleValues)
            {
                InventoryManager.Instance.AddToInventory(material.Key,material.Value);
            }
            
        }
    }
    #endregion

    #region Animals options And Funtions
    
    public void ResetAnimalDictionary()
    {
        animalScaleValues = new Dictionary<int, int>();
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
    
    #endregion
}