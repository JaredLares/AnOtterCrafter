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
    
}
