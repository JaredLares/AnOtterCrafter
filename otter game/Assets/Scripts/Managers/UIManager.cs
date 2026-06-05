using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //  hotbar
    public List<GameObject> images = new List<GameObject>();
    [SerializeField] private GameObject bagItem;
    [SerializeField] private bool isActive = true;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject hotbar;
    [SerializeField] private InventoryStructure structure;
    // texto en la hotbar
    public List<TextMeshProUGUI> itemsAmount;
    // trade button
    
    [SerializeField] private GameObject tradeButton;
    [SerializeField] private GameObject tradeOptions;
    
    // instance
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void UpdateHotbar()
    {
        if(structure.InventoryCount() == 0) return;
        int temp = 1;
        foreach (var image in images)
        {
            image.GetComponent<Image>().sprite = structure.inventorySprite(temp);
            image.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            image.GetComponent<hotbarItem>().saveID(structure.InventoryID(temp));
            itemsAmount[temp-1].text = structure.InventoryAmount(temp).ToString();
            temp++;
        }
    }

    public void triggerHotbar()
    {
        if(isActive)
        {
            hotbar.SetActive(false);
            button.SetActive(false);
            bagItem.SetActive(true);
            isActive = false;
        }
        else
        {
            hotbar.SetActive(true);
            button.SetActive(true);
            bagItem.SetActive(false);
            isActive = true;
        }
    }

    public void PrepareTrade()
    {
        tradeButton.SetActive(true);
    }
    public void TradingScreen()
    {
        tradeButton.SetActive(false);
        StartCoroutine(TradeScreen());
    }
    public void MakeTrade()
    {
        GameManager.Instance.MakeTrade();
        StartCoroutine(CostumerScreen());
    }

    public void DontTrade()
    {
        GameManager.Instance.ReturnMaterials();
        StartCoroutine(CostumerScreen());
    }

    IEnumerator TradeScreen()
    {
        yield return new WaitForSeconds(1.1f);
        tradeOptions.SetActive(true);
        yield return null;
    }

    IEnumerator CostumerScreen()
    {
        tradeOptions.SetActive(false);
        GameManager.Instance.ConsumerScreen();
        GameManager.Instance.ResetPlayerDictionary();
        yield return new WaitForSeconds(1.1f);
        GameManager.Instance.SpawnAnimal();
    }
}
