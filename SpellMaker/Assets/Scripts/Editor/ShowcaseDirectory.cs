using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// <para>
/// Showcase of custom editor field which accepts only directory (folder) assets.
/// Useful for configuring access to assets which normally would be loaded by full path with AssetDatabase,
/// which would be prone to breaking when changing folder structure/naming.
/// </para>
/// <para>
/// Supported features:
/// <list type="bullet">
/// <item>
/// <term>Drag/drop asset with visual indicator when its type is correct/incorrect</term>
/// </item>
/// <item>
/// <term>Deleting (delete key when focused)</term>
/// </item>
/// <item>
/// <term>Highlighting referenced asset in project window on click</term>
/// </item>
/// </list>
/// </para>
/// </summary>
public class ShowcaseDirectory : EditorWindow
{
    private Object folder1;
    private Object folder2;

    [MenuItem("Custom/Directory field showcase")]
    public static void ShowWindow()
    {
        GetWindow(typeof(ShowcaseDirectory), false, "Directory Field Showcase");
    }

    private void OnGUI()
    {
        DirectoryField(ref folder1, "Folder 1", "control1");
        DirectoryField(ref folder2, "Folder 2", "control2");
    }

    private void DirectoryField(ref Object obj, string labelText, string controlName)
    {
        GUI.SetNextControlName(controlName);

        // draw content and label
        GUIContent guiContent = EditorGUIUtility.ObjectContent(obj, typeof(DefaultAsset));
        Rect rect = EditorGUI.PrefixLabel(EditorGUILayout.GetControlRect(), new GUIContent(labelText));

        // set asset image
        GUIStyle textFieldStyle = new GUIStyle("TextField")
        {
            imagePosition = obj ? ImagePosition.ImageLeft : ImagePosition.TextOnly,
        };

        // set custom text when reference is null
        if (!obj)
        {
            guiContent.text = "None (Directory)";
        }

        // add button to the control
        if (GUI.Button(rect, guiContent, textFieldStyle))
        {
            // focus the control with visual outline
            GUI.FocusControl(controlName);

            if (obj)
            {
                // highlight asset in project window
                EditorGUIUtility.PingObject(obj);
            }
        }

        // check for pressed key when this control is focused
        if (Event.current.type == EventType.KeyDown && GUI.GetNameOfFocusedControl() == controlName)
        {
            if (Event.current.keyCode == KeyCode.Delete)
            {
                obj = null;
                Event.current.Use();
                return;
            }
        }

        // check if mouse cursor is on the control
        if (rect.Contains(Event.current.mousePosition))
        {
            // dragging in progress
            if (Event.current.type == EventType.DragUpdated)
            {
                Object reference = DragAndDrop.objectReferences[0];
                string path = AssetDatabase.GetAssetPath(reference);
                // check if the dragged asset is a directory, if yes show indicator that it can be dropped
                DragAndDrop.visualMode = Directory.Exists(path) ? DragAndDropVisualMode.Copy : DragAndDropVisualMode.Rejected;
                Event.current.Use();
            }
            // dragging finished
            else if (Event.current.type == EventType.DragPerform)
            {
                Object reference = DragAndDrop.objectReferences[0];
                string path = AssetDatabase.GetAssetPath(reference);
                if (Directory.Exists(path))
                {
                    obj = reference;
                    // save asset guid so it can be restored after reopening
                    EditorPrefs.SetString(nameof(obj), AssetDatabase.AssetPathToGUID(path));
                }

                GUI.FocusControl(controlName);
                Event.current.Use();
            }
        }
    }
}