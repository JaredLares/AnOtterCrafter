using UnityEngine;

public interface IGManager
{
    void MaterialForID(int ID);
    void SpriteForID(int ID);

    void ShowHotbar();
    void HideHotbar();

    void ToggleTradeScreen();

    void Maketrade();

    void CancelTrade();

    void AddMaterial(int ID, int amount);

    void SubtractMaterial(int ID, int amount);

    void UpdateBiome();

}

public interface Iinventory
{
    void AddItem(int ID, int amount);

    void RemoveItem(int ID, int amount);

    int GetItemAmount(int ID);

    Sprite GetItemSprite(int ID);

}

public interface IAnimal
{
    AnimalPreferences GetPreferences();
}

public interface IScale
{
    void SpawnAnimalMaterial(int ID);

    void SpawnPlayerMaterial(int ID);

    void DespawnAnimalMaterial(int ID);
}