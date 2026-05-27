using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimalsBiomeList", menuName = "Scriptable Objects/AnimalsBiomeList")]
public class AnimalsBiomeList : ScriptableObject
{
    [SerializeField] private int rareProbability;
    [SerializeField] private List<GameObject> biomeAnimals;
    [SerializeField] private List<GameObject> rareAnimalsList;

    public GameObject ReturnAnimal()
    {
        int index = Random.Range(1, 100);
        int randomAnimal;
        if (index <= rareProbability)
        {
            randomAnimal = Random.Range(0, rareAnimalsList.Count);
            return rareAnimalsList[randomAnimal];
        }
        randomAnimal = Random.Range(0, biomeAnimals.Count);
        return biomeAnimals[randomAnimal];
    }
}