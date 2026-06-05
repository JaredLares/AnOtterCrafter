using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    [SerializeField] private GameObject scaleTop,scaleLeft,scaleRight;
    [SerializeField] private Dictionary<int, int> animaleScaleValue = new Dictionary<int, int>();
    [SerializeField] private List<GameObject> animaleScale = new List<GameObject>();
    [SerializeField] private List<GameObject> tradingScale = new List<GameObject>();
    [SerializeField] private GameObject materialPRefab;
    [SerializeField] private int maxRotation = 30;
    
    public static Scale Instance;
    
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

    public void resetDictionaries()
    {
        animaleScaleValue.Clear();
        foreach (var item in animaleScale)
        {
            Destroy(item);
        }
        foreach (var item in tradingScale)
        {
            Destroy(item);
        }
        animaleScale.Clear();
        tradingScale.Clear();
        Rotate(new Vector3(0,0,0));
    }
    
    public void LoadAnimalTrade(Dictionary<int, int> animalTrade)
    {
        animaleScaleValue = animalTrade;
        StartCoroutine(SpawnTrades());
    }
    
    
    public void AddPlayerTrade(int materialID)
    {
        GameObject temp = Instantiate(materialPRefab, scaleLeft.transform.position,new Quaternion(0,0,0,0));
        temp.GetComponent<SpriteRenderer>().sprite =
            InventoryManager.Instance.inventoryStructure.inventorySprite(materialID);
        temp.GetComponent<SpriteRenderer>().sortingOrder = 4;
        temp.AddComponent<PolygonCollider2D>();
        tradingScale.Add(temp);
        RotateScale(GameManager.Instance.InternalValue, GameManager.Instance.AnimalInternalValue);
    }

    public void RemovePlayerTrade(int materialID)
    {
        for (int i = 0; i < tradingScale.Count; i++)
        {
            if (tradingScale[i].GetComponent<SpriteRenderer>().sprite ==
                InventoryManager.Instance.inventoryStructure.inventorySprite(materialID))
            {
                Destroy(tradingScale[i]);
                tradingScale.RemoveAt(i);
                break;
            }
        }
        RotateScale(GameManager.Instance.InternalValue, GameManager.Instance.AnimalInternalValue);
    }
    
    public void RotateScale(int playerScaleValue, int animalScaleValue)
    {
        if (playerScaleValue == animalScaleValue)
        {
            StartCoroutine(Rotate(Vector3.zero));
        }
        else
        {
            float porTemp = maxRotation / Mathf.Max(animalScaleValue, Mathf.Epsilon);
            float tempRotation = -maxRotation + playerScaleValue * porTemp;
            if(tempRotation > maxRotation){tempRotation = maxRotation;}
            StartCoroutine(Rotate(new Vector3(0,0,tempRotation)));
        }
    }

    IEnumerator Rotate(Vector3 targetRotation)
    {
    Quaternion start = scaleTop.transform.localRotation;
    Quaternion end = Quaternion.Euler(targetRotation);
    float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 2f;
            scaleTop.transform.localRotation = Quaternion.Slerp(start, end, t);
            scaleRight.transform.localRotation = Quaternion.Euler(0,0,-scaleTop.transform.localEulerAngles.z);
            scaleLeft.transform.localRotation = Quaternion.Euler(0,0,-scaleTop.transform.localEulerAngles.z);
            yield return null;
        }
    }

    IEnumerator SpawnTrades()
    {
        for(int i = 1;i<=InventoryManager.Instance.inventoryStructure.AllmaterialsCount() ;i++)
        {
            if(animaleScaleValue.TryGetValue(i, out int amount))
            {
                for (int j = 0; j < amount; j++)
                {
                    GameObject temp = Instantiate(materialPRefab, scaleRight.transform.position,new Quaternion(0,0,0,0));
                    temp.GetComponent<SpriteRenderer>().sprite =
                        InventoryManager.Instance.inventoryStructure.inventorySprite(i);
                    temp.GetComponent<SpriteRenderer>().sortingOrder = 4;
                    temp.AddComponent<PolygonCollider2D>();
                    yield return new WaitForSeconds(0.25f);
                    animaleScale.Add(temp);
                }
            }
        }
        RotateScale(GameManager.Instance.InternalValue, GameManager.Instance.AnimalInternalValue);
    }
}