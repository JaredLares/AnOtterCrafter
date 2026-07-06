using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Biomes", menuName = "Scriptable Objects/Biomes")]
public class Biomes : ScriptableObject
{
    public int biomeID;
    public string biomeName;
    public int animalProv;
    public List<GameObject> mainAnimals;
    public List<GameObject> allAnimals;
    
    public GameObject GetRandomAnimal()
    {
        int randomNum = Random.Range(1,100);
        if (randomNum <= animalProv )
        {
            randomNum = Random.Range(0,allAnimals.Count);
            return allAnimals[randomNum];
        }
        // spawn basic animal
        randomNum = Random.Range(0,mainAnimals.Count);
        return mainAnimals[randomNum];
    }


}
