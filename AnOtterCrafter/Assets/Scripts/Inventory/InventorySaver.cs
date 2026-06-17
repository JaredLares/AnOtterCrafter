using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventorySaver", menuName = "Scriptable Objects/InventorySaver")]
public class InventorySaver : ScriptableObject
{
    public List<BaseMaterialSO> allMaterials = new List<BaseMaterialSO>();                          
}
