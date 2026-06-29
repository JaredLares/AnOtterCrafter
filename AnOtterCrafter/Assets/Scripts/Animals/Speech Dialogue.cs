using System;
using System.Collections.Generic;
using UnityEngine;

public class SpeechDialogue : MonoBehaviour
{
    [SerializeField] private float sizex;
    [SerializeField] private float maxScale;
    [SerializeField] private float minScale;
    [SerializeField] private List<BaseMaterialSO> materials = new List<BaseMaterialSO>();
    private Dictionary<BaseMaterialSO, int> speechDictionary = new Dictionary<BaseMaterialSO, int>();
    [SerializeField] private List<GameObject>  speechList = new List<GameObject>();

    public void AddMaterial(BaseMaterialSO material)
    {
        materials.Add(material);
        if(speechDictionary.TryGetValue(material, out int materialId))
        {
            materialId++;
            speechDictionary[material] = materialId;
        }
        else
        {
            speechDictionary.Add(material,1);
        }
    }
    
    public void ResetSpeech()
    {
         speechDictionary.Clear();
         materials.Clear();
         speechList.Clear();
    }

    public void showMaterialsInBubble()
    {
        int count = speechDictionary.Keys.Count;
        int index = 0;
        foreach (BaseMaterialSO material in speechDictionary.Keys)
        {
            GameObject temp = new GameObject("Material " + material);
            temp.transform.SetParent(transform);
            float posX;
            if (count == 1)
            {
                posX = 0f;
            }
            else
            {
                float spacing = (2f * sizex) / (count - 1);
                posX = -sizex + index * spacing;
            }
            temp.transform.position = transform.position + new Vector3(posX, 0, 0);
            temp.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            SpriteRenderer sr = temp.AddComponent<SpriteRenderer>();
            sr.sprite = material.MaterialSprite;
            sr.sortingOrder = 5;
            speechList.Add(temp);
            index++;
        }
    }
}