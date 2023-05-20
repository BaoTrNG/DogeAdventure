using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private Dictionary<GameObject, int> inventory = new Dictionary<GameObject, int>();
    public Dictionary<GameObject, int> GetInventory()
    {
        return inventory;
    }
    // Add an item to the inventory
    public void AddItem(GameObject itemName, int quantity)
    {
        if (inventory.ContainsKey(itemName))
        {
            inventory[itemName] += quantity;
        }
        else
        {
            inventory.Add(itemName, quantity);
        }
    }

    // Remove an item from the inventory
    public void RemoveItem(GameObject ItemName, int quantity)
    {
        if (inventory.ContainsKey(ItemName))
        {
            inventory[ItemName] -= quantity;
            if (inventory[ItemName] <= 0)
            {
                inventory.Remove(ItemName);
            }
        }
    }

    public bool HasItem(GameObject itemName)
    {
        return inventory.ContainsKey(itemName);
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(PlayerInventory))]
    public class PlayerInventoryEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            PlayerInventory inventoryScript = (PlayerInventory)target;

            DrawDefaultInspector();

            GUILayout.Space(10);
            GUILayout.Label("Inventory Items:");
            foreach (KeyValuePair<GameObject, int> item in inventoryScript.inventory)
            {
                GUILayout.Label(item.Key + " x " + item.Value);
            }
        }
    }
#endif


}
