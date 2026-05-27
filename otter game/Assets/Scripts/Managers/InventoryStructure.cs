using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryStructure", menuName = "Scriptable Objects/InventoryStructure")]
public class InventoryStructure : ScriptableObject
{
    [SerializeField] private List<BaseMaterial> allMaterials = new List<BaseMaterial>();
    [SerializeField] private List<BaseMaterial> inventory = new List<BaseMaterial>();

    public int AllmaterialsCount()
    {
        return allMaterials.Count;
    }
    public int InventoryCount()
    {
        return inventory.Count;
    }

    public Sprite inventorySprite(int index)
    {
        foreach (var material in inventory)
        {
            if (material.ID == index)
            {
                return material.MaterialSprite;
            }
        }

        return null;
    }
    
    public int InventoryAmount(int index)
    {
        foreach (BaseMaterial material in inventory)
        {
            if (material.ID == index)
            {
                return material.MaterialAmount;
            }
        }

        return 0;
    }

    public int InventoryID(int index)
    {
        foreach (BaseMaterial material in allMaterials)
        {
            if (material.ID == index)
            {
                return material.ID;
            }
        }
        return 0;
    }
    
    public int AllMaterialsCost(int ID)
    {
        foreach (BaseMaterial material in allMaterials)
        {
            if (material.ID == ID)
            {
                return material.MaterialCost;
            }
        }
        return 0;   
    }

    public void UpdateMaterialValues(int id, int amount)
    {
        foreach (BaseMaterial material in allMaterials)
        {
            if (material.ID == id)
            {
                material.MaterialAmount = amount;
            }
        }
    }

    public string AllMaterialsName(int materialId)
    {
        foreach (BaseMaterial material in allMaterials)
        {
            if (material.ID == materialId)
            {
                return material.MaterialName;
            }
        }
        return null; 
    }

    public BaseMaterial ReturnMaterialFromID(int materialId)
    {
        foreach (var material in allMaterials)
        {
            if (material.ID == materialId)
            {
                return material;
            }
        }
        return null;
    }
}
