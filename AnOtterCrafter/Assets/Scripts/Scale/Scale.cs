using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Scale : MonoBehaviour, IScale
{
    #region Variables

    // public

    // private
    [SerializeField] private GameObject scaleTop,scaleLeft,scaleRight;
    [SerializeField] private Dictionary<int, int> animalScaleValue = new Dictionary<int, int>();
    [SerializeField] private List<GameObject> animalScale = new List<GameObject>();
    [SerializeField] private List<GameObject> tradingScale = new List<GameObject>();
    [SerializeField] private GameObject materialPRefab;
    [SerializeField] private int maxRotation = 30;
    [SerializeField] private VisualEffect leftSpawn;
    [SerializeField] private VisualEffect rightSpawn;
    #endregion

    #region InterfaceFunctions
    public void SpawnAnimalMaterial(Dictionary<int, int> animalTrade)
    {
        animalScaleValue = animalTrade;
        StartCoroutine(SpawnTrades());
        RotateScale(GameManager.Instance.InternalValue, GameManager.Instance.AnimalInternalValue);
    }

    public void SpawnPlayerMaterial(int ID)
    {
leftSpawn.Play();
        GameObject temp = Instantiate(materialPRefab, scaleLeft.transform.position,new Quaternion(0,0,0,0));
        temp.GetComponent<SpriteRenderer>().sprite =
            GameManager.Instance.Inventory().GetItemSprite(ID);   
        temp.GetComponent<SpriteRenderer>().sortingOrder = 4;
        temp.AddComponent<PolygonCollider2D>();
        Renderer rend = temp.GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material = new Material(rend.material);
            rend.material.SetFloat("_GlowMaterial", 0f);
        }
        tradingScale.Add(temp);
        RotateScale(GameManager.Instance.InternalValue, GameManager.Instance.AnimalInternalValue);
    }

    public void DespawnPlayerMaterial(int ID)
    {
        for (int i = 0; i < tradingScale.Count; i++)
        {
            if (tradingScale[i].GetComponent<SpriteRenderer>().sprite ==
                GameManager.Instance.Inventory().GetItemSprite(ID))
            {
                Destroy(tradingScale[i]);
                tradingScale.RemoveAt(i);
                break;
            }
        }
        RotateScale(GameManager.Instance.InternalValue, GameManager.Instance.AnimalInternalValue);
    }
    
    #endregion

    #region ScriptFunctions
    public void resetDictionaries()
    {
        StartCoroutine(Rotate(Vector3.zero));
        animalScaleValue.Clear();
        foreach (var item in animalScale)
        {
            Destroy(item);
        }
        foreach (var item in tradingScale)
        {
            Destroy(item);
        }
        animalScale.Clear();
        tradingScale.Clear();
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
    
    #endregion

    #region courutines
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
        for(int i = 1;i<=GameManager.Instance.Inventory().GetAllMaterialsCount() ;i++)
        {
            if(animalScaleValue.TryGetValue(i, out int amount))
            {
                RotateScale(GameManager.Instance.InternalValue, GameManager.Instance.AnimalInternalValue);
                for (int j = 0; j < amount; j++)
                {
                    rightSpawn.Play();
                    GameObject temp = Instantiate(materialPRefab, scaleRight.transform.position,new Quaternion(0,0,0,0));
                    temp.GetComponent<SpriteRenderer>().sprite =
                        GameManager.Instance.SpriteForID(i);
                    temp.GetComponent<SpriteRenderer>().sortingOrder = 4;
                    temp.AddComponent<PolygonCollider2D>();
                    yield return new WaitForSeconds(0.25f);
                    animalScale.Add(temp);
                }
            }
        }

    }
    #endregion

}