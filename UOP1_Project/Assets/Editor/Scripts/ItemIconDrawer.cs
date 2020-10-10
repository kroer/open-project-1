using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ItemIconAttribute))]
public class ItemIconDrawer : PropertyDrawer
{
	private const int TextureSize = 64;

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return TextureSize;
	}

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);

		var initialWidth = position.width;

		position.width = EditorGUIUtility.labelWidth;
		GUI.Label(position, property.displayName);

		position.x += initialWidth - TextureSize;
		position.width = TextureSize;
		position.height = TextureSize;
		property.objectReferenceValue = EditorGUI.ObjectField(position, property.objectReferenceValue, typeof(Sprite), false);

		EditorGUI.EndProperty();
	}
}
