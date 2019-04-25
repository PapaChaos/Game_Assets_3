using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public List<InventoryItem> ItemsInInventory;

	void AddItemToInventory(InventoryItem item)
	{
		ItemsInInventory.Add(item);
	}
	void RemoveItemfromInventory(InventoryItem item)
	{
		ItemsInInventory.Remove(item);
	}
}
