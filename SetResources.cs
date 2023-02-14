// Created by NeroAngra
// To use: 
// 1. Create all of your resources nodes under a parent object.
// 2. Copy a base resource node script compnent to each resource node.
// 3. Run SetResources under MyScripts
// 4. Drag over your parent object from step 1.
// 5. It will add all child objects of each of your resource nodes to the Resource Node script's Sub POrofile Game Objects list.
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SetResources : EditorWindow
{
    private ScriptableObject scriptableObj;
    private SerializedObject serialObj;

    private Vector2 viewScrollPosition;

    public Transform parentResource;
    public string resourceProfile;

    private Atavism.ResourceNode node;
    private GameObject childObject;

    [MenuItem("MyScripts/SetResources")]
    private static void OpenWindow()
    {
        var window = (SetResources)GetWindow(typeof(SetResources), false, "Add Resources");
        window.minSize = new Vector2(400, 500);
        GUI.contentColor = Color.white;
        window.Show();
    }

    private void OnEnable()
    {
        scriptableObj = this;
        serialObj = new SerializedObject(scriptableObj);
    }

    private void OnGUI()
    {
        DrawMain();
    }

    private void DrawMain()
    {
        viewScrollPosition = EditorGUILayout.BeginScrollView(viewScrollPosition, false, false);

        GUILayout.Space(7);
        parentResource = (Transform)EditorGUILayout.ObjectField("", parentResource, typeof(Transform), true);
        GUILayout.Space(15);
        

        if (GUILayout.Button("Set Resources", GUILayout.MinWidth(150), GUILayout.MinHeight(30), GUILayout.ExpandWidth(true)))
        {
            SetChildResources();
        }

        serialObj.ApplyModifiedProperties();

        GUILayout.Space(20);
        GUILayout.EndScrollView();
    }

    private void SetChildResources()
    {
        foreach (Transform child in parentResource)
        {

            foreach (Transform obj in child)
            {
                node = child.GetComponent<Atavism.ResourceNode>();
                childObject = obj.gameObject;

                node.subProfileGameObjects.Add(childObject);
            }
        }
    }

}
