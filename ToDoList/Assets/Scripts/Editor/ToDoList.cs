using UnityEngine;
using System;
using System.IO;
using UnityEditor;

public class ToDoList : EditorWindow
{
    public GUISkin skin;
    Color userColor;
    int fontSize = 20;
    Vector2 scroll;

    bool hasSaved = false;

    float sliderFloat = 20f;
    string userInput = "";
    string filePath = "";

    [MenuItem("Custom Tools/ToDo List")]
    public static void ShowWindow()
    {
        GetWindow<ToDoList>("ToDo List");
    }
    void OnEnable()
    {
        //filePath = @"C:\Users\ellio\Documents\Notes.txt";
        filePath = Application.dataPath + "/Notes.txt";
        if (File.Exists(filePath))
        {
            userInput = File.ReadAllText(filePath);
        }
    }
    void OnGUI()
    {
        //GUIStyle todoStyle = new GUIStyle("Text Area");
        userColor = Color.white;

        skin.GetStyle("textarea").normal = skin.GetStyle("textarea").onHover;

        sliderFloat = EditorGUILayout.Slider(sliderFloat, 0f, 100f);
        fontSize = Mathf.FloorToInt(sliderFloat);

        skin.GetStyle("textarea").fontSize = (fontSize);

        userColor = EditorGUILayout.ColorField("Text Color", userColor);
        skin.GetStyle("textarea").normal.textColor = userColor;
        skin.GetStyle("textarea").active.textColor = userColor;
        skin.GetStyle("textarea").focused.textColor = userColor;
        skin.GetStyle("textarea").hover.textColor = userColor;

        scroll = EditorGUILayout.BeginScrollView(scroll);

        userInput = EditorGUILayout.TextArea(userInput, skin.GetStyle("textarea"), GUILayout.MinHeight(150), GUILayout.Height(position.height - 30));
        EditorGUILayout.EndScrollView();

        hasSaved = GUILayout.Button("Save");
        if (hasSaved)
        {
            OnSaved();
        }
    }
    void OnSaved()
    {
        filePath = Application.dataPath + "/Notes.txt";

        File.WriteAllText(filePath, userInput);
        
    }
}
