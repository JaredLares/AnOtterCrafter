using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseMaterialSO", menuName = "Scriptable Objects/BaseMaterialSO")]
public class BaseMaterialSO : ScriptableObject
{
    public int id;
    public string materialName;
    public Sprite materialSprite;
    public int materialMaxAmount;
    public int materialValue;
}
