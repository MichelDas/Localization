using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class LocalizedTextEditor : EditorWindow
{
    public LocalizationData localizationData;

    [MenuItem("Window/localized text Editor")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(LocalizedTextEditor)).Show();
    }

    private void OnGUI()
    {
        if(localizationData != null)
        {
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("localizationdata");
            EditorGUILayout.PropertyField(serializedProperty, true);
            serializedObject.ApplyModifiedProperties();

            if(GUILayout.Button("Save data"))
            {
                SaveGameData();
            }
        }
        if(GUILayout.Button("Load Data"))
        {
            LoadGameData();
        }
        if(GUILayout.Button("Create new data"))
        {
            CreateNewData();
        }
    }

    private void LoadGameData()
    {
        string filepath = EditorUtility.OpenFilePanel("Select localization data file", Application.streamingAssetsPath, "Json");
        if(!string.IsNullOrEmpty(filepath))
        {
            string dataAsJson = File.ReadAllText(filepath);
            localizationData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
        }
    }

    private void SaveGameData()
    {
        string filePath = EditorUtility.SaveFilePanel("save localization data file", Application.streamingAssetsPath, "", "json");

        if(!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = JsonUtility.ToJson(localizationData);
            File.WriteAllText(filePath, dataAsJson);

        }
    }

    private void CreateNewData()
    {
        localizationData = new LocalizationData();
    }
}
