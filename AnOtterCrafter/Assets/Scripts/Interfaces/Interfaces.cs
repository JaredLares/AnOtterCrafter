using UnityEngine;

public interface IGManager
{
    BaseMaterialSO MaterialForID(int ID);
    void SpriteForID(int ID);

    void ToggleTradeScreen();

    void Maketrade();

    void CancelTrade();

    void AddMaterial(int ID, int amount);

    void SubtractMaterial(int ID, int amount);

    void UpdateBiome(Biomes newBiomes);

    void StartTrade();

}

public interface Iinventory
{
    void AddItem(int ID, int amount);

    void RemoveItem(int ID, int amount);

    int GetItemAmount(int ID);

    Sprite GetItemSprite(int ID);
    
    BaseMaterialSO GetMaterialForID(int ID);

}

public interface IAnimal
{
    BaseAnimalPreferences GetPreferences();
}

public interface IScale
{
    void SpawnAnimalMaterial(int ID);

    void SpawnPlayerMaterial(int ID);

    void DespawnAnimalMaterial(int ID);
}
