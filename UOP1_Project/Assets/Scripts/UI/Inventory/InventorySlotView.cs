using UnityEngine;
using UnityEngine.UI;

class InventorySlotView : MonoBehaviour
{
	[SerializeField] private Image _icon = null;
	[SerializeField] private GameObject _countContainer = null;
	[SerializeField] private Text _count = null;

	private ItemStack _item;

	public void Init(ItemStack item)
	{
		_item = item;
		if (item != null)
		{
			_icon.sprite = _item.Item.Icon;
			if (_item.Item.MaxStackSize == 1)
			{
				// it makes no sense to show the quantity of item if it not stackable (equipments, unique items etc)
				_countContainer.SetActive(false);
			}
			else
			{
				_count.text = _item.Count.ToString();
				_countContainer.SetActive(true);
			}
		}
		else
		{
			_icon.sprite = null;
			_icon.color = new Color(0, 0, 0, 0);
			_count.text = string.Empty;
			_countContainer.SetActive(false);
		}
	}
}
