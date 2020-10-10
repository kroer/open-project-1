using UnityEngine;

// Class is temporary and just for inventory tests
// inventory enabling/disabling should be handle in some manager
// InventoryData should not be pass through inspector
public class InventoryView : MonoBehaviour
{
	[Header("Tmp settings")]
	[SerializeField] private InventoryData _data = null;
	[SerializeField] private InputReader _inputReader = null;

	[Header("Slots settings")]
	[SerializeField] private InventorySlotView _slotPrefab = null;
	[SerializeField] private Transform _slotsContainer = null;

	private InventorySlotView[] _slots;

	private void Start()
	{
		_inputReader.inventoryEvent += ToggleSelf;
	}

	private void ToggleSelf()
	{
		// Todo: change action map
		gameObject.SetActive(!gameObject.activeSelf);
	}

	private void OnEnable()
	{
		var items = _data.Items;
		_slots = new InventorySlotView[_data.Size];
		for (int i = 0; i < _data.Size; i++)
		{
			ItemStack itemStack = i < items.Count ? items[i] : null;
			_slots[i] = Instantiate(_slotPrefab, _slotsContainer);
			_slots[i].Init(itemStack);
		}
	}

	private void OnDisable()
	{
		foreach (var slot in _slots)
		{
			Destroy(slot.gameObject);
		}
		_slots = null;
	}
}
