using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory")]
public class InventoryData : ScriptableObject
{
	[SerializeField] private int _size = 8;
	[SerializeField] private List<ItemStack> _items;

	// bad, slow, temporary)
	public IList<ItemStack> Items => _items.AsReadOnly();
	public int Size => _size;

	public InventoryData()
	{
		_items = new List<ItemStack>();
	}

	public int AddItem(ItemData item, int count)
	{
		// trying to add item to existing stacks
		foreach (var stack in _items.Where(entry => entry.Item == item))
		{
			count = stack.IncreaseCount(count);
			if (count == 0)
			{
				break;
			}
		}
		// add remaining count to new stacks
		while (count > 0 && _items.Count < _size)
		{
			var stack = new ItemStack(item);
			count = stack.IncreaseCount(count);
		}
		return count;
	}

	#region Editor methods
#if UNITY_EDITOR
	private void OnValidate()
	{
		for (int i = 0; i < _items.Count; i++)
		{
			if (_items[i].Validate())
			{
				_items[i].UpdateInspectorName();
			}
			else
			{
				_items.RemoveAt(i);
				i--;
			}
		}
	}
#endif
	#endregion
}
