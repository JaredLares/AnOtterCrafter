using System.Collections.Generic;
using UnityEngine;

public interface IGManager
{
    BaseMaterialSO MaterialForID(int ID);
    void SpriteForID(int ID);

    void ToggleTradeScreen();

    void MakeTrade();

    void CancelTrade();

    void AddMaterial(int ID, int amount);

    void SubtractMaterial(int ID, int amount);

    void UpdateBiome(Biomes newBiomes);

    void StartTrade();

    Dictionary<int, int> GetAnimalTradeDictionary();

    void ScaleTrade();

}

public interface Iinventory
{
    int GetAllMaterialsCount();
    void AddItem(int ID, int amount);

    void RemoveItem(int ID, int amount);

    int GetItemAmount(int ID);

    Sprite GetItemSprite(int ID);
    
    BaseMaterialSO GetMaterialForID(int ID);

}

public interface IAnimal
{
    BaseAnimalPreferences GetPreferences();
    Dictionary<int, int> GetTradeDictionary();

}

public interface IScale
{
    void SpawnAnimalMaterial(Dictionary<int, int> animalTrade);

    void SpawnPlayerMaterial(int ID);

    void DespawnPlayerMaterial(int ID);
}