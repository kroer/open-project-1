using System;
using UnityEngine;

[Serializable]
public class ItemStack
{
#if UNITY_EDITOR
#pragma warning disable CS0649
	// for displaying human friendly name in inspector
	[SerializeField] private string _inspectorName;
#pragma warning restore CS0649
#endif

	[SerializeField] private ItemData _item;
	[SerializeField] private int _count = 1;

	public ItemData Item => _item;
	public int Count => _count;

	public ItemStack(ItemData item)
	{
		_item = item;
		if (_item == null)
		{
			throw new Exception($"Item cannot be null.");
		}
	}

	public ItemStack(ItemData item, int count) : this(item)
	{
		if (IncreaseCount(count) != 0)
		{
			throw new Exception($"Count too big. Item: {_item} Count: {count}");
		}
	}

	/// <summary>
	/// Try to add items to stack and return count of not added.
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public int IncreaseCount(int value)
	{
		var newCount = _count + value;
		if (newCount > _item.MaxStackSize)
		{
			_count = _item.MaxStackSize;
			return newCount - _item.MaxStackSize;
		}
		_count = newCount;
		return 0;
	}

	/// <summary>
	/// Try to remove items from stack and return count of not removed.
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public int DecreaseCount(int value)
	{
		var newCount = _count - value;
		if (newCount < 0)
		{
			_count = 0;
			return -newCount;
		}
		_count = newCount;
		return 0;
	}

	#region Editor methods
#if UNITY_EDITOR
	public bool Validate()
	{
		if (_item == null && _count == 0)
		{
			// item was crated in inspector
			_count = 1; // to prevent auto delete on next check
			return true;
		}
		if (_count <= 0)
		{
			// auto delete items with non-positive count
			return false;
		}
		if (_item != null)
		{
			if (_count > _item.MaxStackSize)
			{
				// fix for stack size
				_count = Item.MaxStackSize;
			}
		}
		return true;
	}

	public void UpdateInspectorName()
	{
		_inspectorName = $"{(_item == null ? "Undefined" : _item.Name)} x{_count}";
	}
#endif
	#endregion
}
