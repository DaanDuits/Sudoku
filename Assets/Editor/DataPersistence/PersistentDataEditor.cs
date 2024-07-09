using System;
using System.IO;
using DataPersistence;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PersistentDataBehaviour), true)]
public class PersistentDataEditor : Editor
{
    private PersistentDataBehaviour _persistentDataObject;
    
    public override void OnInspectorGUI()
    {
        _persistentDataObject = (PersistentDataBehaviour)target;
        base.OnInspectorGUI();
        
        if (String.IsNullOrEmpty(_persistentDataObject.filePath))
        {
            _persistentDataObject.filePath = "/SaveData";
        }
        if (!Directory.Exists(Application.persistentDataPath + _persistentDataObject.filePath))
        {
            Directory.CreateDirectory(Application.persistentDataPath + _persistentDataObject.filePath);
        }
        
        EditorGUILayout.Space(15);
        
        EditorGUILayout.LabelField(Application.persistentDataPath + _persistentDataObject.filePath, EditorStyles.boldLabel);
        if (GUILayout.Button("Set directory"))
        {
            SelectDir:
                var path = EditorUtility.SaveFolderPanel("Select directory", Application.persistentDataPath + _persistentDataObject.filePath, "");
                if (!path.Contains(Application.persistentDataPath))
                {
                    goto SelectDir;
                }
                var dir = path.Remove(0, Application.persistentDataPath.Length);
                _persistentDataObject.filePath = dir;
        }
        if (GUILayout.Button("Open save file path"))
        {
            EditorUtility.RevealInFinder(Application.persistentDataPath + _persistentDataObject.filePath);
        }
    }
}
