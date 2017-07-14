using UnityEngine;
using UnityEditor;

public class BreakPrefab : MonoBehaviour
{

    [MenuItem("Editor Helpers/Break Prefab &b")]
    static void Break()
    {

        //Undo.RegisterSceneUndo("Break Prefab");
        //Undo.RecordObject(target, "Break Prefab");
        EditorApplication.ExecuteMenuItem("GameObject/Break Prefab Instance");
    }


}