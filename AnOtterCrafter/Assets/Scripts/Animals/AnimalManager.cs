using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    [SerializeField] private BaseAnimalPreferences preferences;
    [SerializeField] private List<BaseMaterialSO> tradeList;
    [SerializeField] private Animator animalAnim;
    [SerializeField] private GameObject speechDialogue;

    public string AnimalName()
    {
        return preferences.name;
    }

    public BaseAnimalPreferences Preferences
    {
        get
        {
            return preferences;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

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
            tradeList.Add(GameManager.Instance.MaterialForID(ID)); 
        }
        VisualizeDialogue();
    }

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
}
