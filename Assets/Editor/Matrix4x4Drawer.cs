using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Matrix4x4Data))]
public class Matrix4x4Drawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty rows = property.FindPropertyRelative("rows");

        // Ensure the array always has exactly 16 elements
        if (rows.arraySize != 16)
        {
            rows.arraySize = 16;
        }

        EditorGUI.BeginProperty(position, label, property);

        // Draw the main field prefix label (e.g., "My Grid")
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Calculate layout constraints
        float size = 18f; // Size of each checkbox
        float spacing = 4f; // Spacing between checkboxes

        // Loop through rows and columns to draw the 4x4 grid layout
        for (int r = 0; r < 4; r++)
        {
            for (int c = 0; c < 4; c++)
            {
                int index = r * 4 + c;
                SerializedProperty cell = rows.GetArrayElementAtIndex(index);

                // Compute exact position for this specific checkbox
                Rect boxRect = new Rect(
                    position.x + (c * (size + spacing)),
                    position.y + (r * (size + spacing)),
                    size,
                    size
                );

                // Draw the toggle checkbox
                cell.boolValue = EditorGUI.Toggle(boxRect, cell.boolValue);
            }
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Allocate vertical space for 4 rows of checkboxes plus spacing
        return (18f * 4) + (4f * 3);
    }
}

