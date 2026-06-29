using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseAnimalPreferences", menuName = "Scriptable Objects/BaseAnimalPreferences")]
public class BaseAnimalPreferences : ScriptableObject
{
    [SerializeField] private string animalName;
    [SerializeField] private int animalID;
    [SerializeField] private int animalTradeProbability;
    [SerializeField] private int maxTradeItems;
    [SerializeField] private int minTradeItems;
    public List<BaseMaterialSO> animalLikes;
    public List<BaseMaterialSO> animalDislikes;
    public List<BaseMaterialSO> allTradePool;
    public List<BaseMaterialSO> preferencesPool;

    #region getters
    public string AnimalName
    {
        get
        {
            return animalName;
        }
    }
    public int AnimalID
    {
        get
        {
            return animalID;
        }
    }
    public int AnimalTradeProbability
    {
        get
        {
            return animalTradeProbability;
        }
    }
    public int MaxTradeItems
    {
        get
        {
            return maxTradeItems;
        }
    }
    public int MinTradeItems
    {
        get
        {
            return minTradeItems;
        }
    }
    #endregion
    public int RandomIDTrade()
    {
        int probability = Random.Range(1, 101);
        if (animalTradeProbability > probability)
        {
            // return all trade pool
            int temp = Random.Range(0, allTradePool.Count);
            return allTradePool[temp].ID;
        }
        else
        {
            // return preferences pool
            int temp = Random.Range(0, preferencesPool.Count);
            return preferencesPool[temp].ID;
        }
    }
}
