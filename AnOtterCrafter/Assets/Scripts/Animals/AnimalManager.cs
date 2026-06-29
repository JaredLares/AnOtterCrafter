using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    [SerializeField] private BaseAnimalPreferences preferences;
    [SerializeField] private Animator animalAnim;
    [SerializeField] private GameObject speechDialgue;

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
    
    public void VisualizeDialogue()
    {
        speechDialgue.SetActive(true);
        if (speechDialgue.TryGetComponent<SpeechDialogue>(out SpeechDialogue speech))
        {
            speech.showMaterialsInBubble();
        }
    }

    public void AddToDialgue(BaseMaterialSO material)
    {
        if (speechDialgue.TryGetComponent<SpeechDialogue>(out SpeechDialogue speech))
        {
            speech.AddMaterial(material);
        }
            
    }
}
