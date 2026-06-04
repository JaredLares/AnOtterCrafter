using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    [SerializeField] private GameObject scaleTop,scaleLeft,scaleRight;
    [SerializeField] private Dictionary<int, int> animaleScaleTrade = new Dictionary<int, int>();
    [SerializeField] private Dictionary<int, int> tradingScaleTrade = new Dictionary<int, int>();
    [SerializeField] private int maxRotation = 30;
    [SerializeField] private int minRotation = 0;
    
    public static Scale Instance;
    
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

    public void resetDictionaries()
    {
        animaleScaleTrade.Clear();
        tradingScaleTrade.Clear();
    }
    
    public void LoadAnimalTrade(Dictionary<int, int> animalTrade)
    {
        animaleScaleTrade = animalTrade; 
        Debug.Log(animaleScaleTrade.Count);
    }

    public void AddPlayerTrade()
    {
        
    }
    
    public void RatateScale(int playerScaleValue, int otterScaleValue)
    {
        
    }
}
