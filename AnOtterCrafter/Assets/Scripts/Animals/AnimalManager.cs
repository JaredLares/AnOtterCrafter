using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour,IAnimal
{
    [SerializeField] private BaseAnimalPreferences preferences;
    [SerializeField] private List<BaseMaterialSO> tradeList;
    private Dictionary<int, int> animalTradeDictionary;
    [SerializeField] private Animator animalAnim;
    [SerializeField] private GameObject speechDialogue;

    #region Getters And Setters
        public string AnimalName()
        {
            return preferences.name;
        }

        BaseAnimalPreferences IAnimal.GetPreferences()
        {
            return preferences;
        }

        Dictionary<int, int> IAnimal.GetTradeDictionary()
        {
            return animalTradeDictionary;
        }
    #endregion


    #region trade functions
    public List<BaseMaterialSO> TradeList
        {
            get
            {
                return tradeList;
            }
        }
    public void createTrade()
    {
        int randomMax = preferences.MaxTradeItems;
        int randomMin = preferences.MinTradeItems;
        int randomTrade = Random.Range(randomMin, randomMax);
        for (int i = 0; i < randomTrade; i++) 
        {
            int ID = preferences.RandomIDTrade();
            AddToDialogue(GameManager.Instance.MaterialForID(ID));
            AddToTradeDictionary(ID, 1);
            tradeList.Add(GameManager.Instance.MaterialForID(ID)); 
        }
        VisualizeDialogue();
    }

    public void AddToTradeDictionary(int ID, int amount)
    {
        if (animalTradeDictionary.ContainsKey(ID))
        {
            animalTradeDictionary[ID] += amount;
        }
        else
        {
            animalTradeDictionary.Add(ID, amount);
        }
    }
    #endregion

    public void Destroy()
    {
        Destroy(gameObject);
    }

    #region Dialogue Functions
   
    public void VisualizeDialogue()
    {
        speechDialogue.SetActive(true);
        if (speechDialogue.TryGetComponent<SpeechDialogue>(out SpeechDialogue speech))
        {
            speech.showMaterialsInBubble();
        }
    }

    public void AddToDialogue(BaseMaterialSO material)
    {
        if (speechDialogue.TryGetComponent<SpeechDialogue>(out SpeechDialogue speech))
        {
            speech.AddMaterial(material);
        }
            
    }

    #endregion
}
