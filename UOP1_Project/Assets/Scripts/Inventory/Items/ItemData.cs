using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item", order = 0)]
public class ItemData : ScriptableObject
{
	public enum CategoryType
	{
		Common,
		Ingredients,
		Food,
		KeyItems,
	}

	[Header("Base settings")]
	[SerializeField] private Sprite _icon = null;
	[SerializeField] private string _name = string.Empty;
	[SerializeField] protected CategoryType _category;
	[SerializeField] private string _description = string.Empty;
	[SerializeField] private int _maxStackSize = 8;

	public Sprite Icon => _icon;
	public string Name => _name;
	public int MaxStackSize => _maxStackSize;
	public CategoryType Category => _category;

	public virtual string GetDescription()
	{
		return _description;
	}

	public override string ToString()
	{
		return $"{{ Name: {_name} Category: {_category} Max stack size: {_maxStackSize} }}";
	}
}
