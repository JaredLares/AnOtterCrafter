public interface IGManager
{
    void MaterialForID(int ID);
    void SpriteForID(int ID);

    void ShowHotbar();
    void HideHotbar();

    void ToggleTradeScreen();

    void Maketrade();

    void CancelTrade();

    void AddMaterial();

    void SubtractMaterial();

    void UpdateBiome();

}

public interface Iinventory
{
    void AddItem(int ID, int amount);

    void RemoveItem(int ID, int amount);

    int GetItemAmount(int ID);

    //Sprite GetItemSprite(int ID);
}