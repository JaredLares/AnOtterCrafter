using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseMaterialSO", menuName = "Scriptable Objects/BaseMaterialSO")]
public class BaseMaterialSO : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private string materialName;
    [SerializeField] private Sprite materialSprite;
    [SerializeField] private int materialMaxAmount;
    [SerializeField] private int materialValue;

    #region getters
        
    public int ID
    {
        get { return id; } 
    }

    public string MaterialName
    {
        get { return materialName; } 
    }
    public Sprite MaterialSprite
    {
        get { return materialSprite; } 
    }
    public int MaterialMaxAmount
    { 
        get { return materialMaxAmount; } 
    }
    public int MaterialValue
    {
        get { return materialValue; }
    }
    #endregion
}
