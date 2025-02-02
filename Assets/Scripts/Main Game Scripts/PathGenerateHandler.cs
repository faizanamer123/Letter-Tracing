using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class PathGenerateHandler : MonoBehaviour
{
    public static PathGenerateHandler instance;
    public List<GameObject> myListPath = new List<GameObject>();
    public GameObject LinePathPrefab;
    public Transform SpawnPoints;
    public int theCurrentNo;

    private void Awake()
    {
        instance = this;
    }

    #region Editor
#if UNITY_EDITOR

    [CustomEditor(typeof(PathGenerateHandler))]
    public class LineEditor : Editor
    {
        private string ThisField;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            PathGenerateHandler thisItem = (PathGenerateHandler)target;

            if (GUILayout.Button("Create Line"))
            {
                thisItem.GenerateLine();
            }

            GUILayout.Space(15);

            if (GUILayout.Button("Remove All Paths"))
            {
                for (int i = 0; i < thisItem.myListPath.Count; i++)
                {
                    DestroyImmediate(thisItem.myListPath[i]);
                }
                thisItem.myListPath.Clear();
            }

            GUILayout.Space(15);
            ThisField = GUILayout.TextField(ThisField);
            if (GUILayout.Button("Remove Specific Path"))
            {
                if (int.TryParse(ThisField, out int index))
                {
                    thisItem.RemovePath(index - 1);
                }
                else
                {
                    Debug.LogError("Invalid input. Please enter a valid number.");
                }
            }

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
#endif
    #endregion

    public void GenerateLine()
    {
        GameObject thisGO = Instantiate(LinePathPrefab, SpawnPoints);
        theCurrentNo = myListPath.Count;
        myListPath.Add(thisGO);
        theCurrentNo += 1;

        PathDrawer pathDrawer = thisGO.GetComponent<PathDrawer>();
        if (pathDrawer != null)
        {
            pathDrawer.myCurrentNo = theCurrentNo;
        }
        else
        {
            Debug.LogError("Generated object does not contain a PathDrawer component.");
        }
    }

    public void RemovePath(int itemNo)
    {
        if (itemNo < 0 || itemNo >= myListPath.Count)
        {
            Debug.LogError("Invalid item number! Cannot remove path.");
            return;
        }

        DestroyImmediate(myListPath[itemNo]);
        myListPath.RemoveAt(itemNo);
        theCurrentNo = myListPath.Count;

        // Update the numbering of remaining paths
        for (int i = 0; i < myListPath.Count; i++)
        {
            PathDrawer pathDrawer = myListPath[i].GetComponent<PathDrawer>();
            if (pathDrawer != null)
            {
                pathDrawer.myCurrentNo = i + 1;
            }
        }
    }
}


/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class PathGenerateHandler : MonoBehaviour
{
    public static PathGenerateHandler instance;
    public List<GameObject> myListPath = new List<GameObject>();
    public GameObject LinePathPrefab;
    public Transform SpawnPoints;
    public int theCurrentNo;

    public void Awake()
    {
        instance = this;
    }

    #region Editor
#if UNITY_EDITOR

    [CustomEditor(typeof(PathGenerateHandler))]
    public class LineEditor : Editor
    {
        string ThisField;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            PathGenerateHandler thisItem = (PathGenerateHandler)target;

            if (GUILayout.Button("create Line"))
            {
                thisItem.GenerateLine();
            }

            GUILayout.Space(15);

            if (GUILayout.Button("Remove all path"))
            {
                for (int i = 0; i < thisItem.myListPath.Count; i++)
                {
                    DestroyImmediate(thisItem.myListPath[i]);
                }
                thisItem.myListPath.Clear();
            }

            GUILayout.Space(15);
            ThisField = GUILayout.TextField(ThisField);
            if (GUILayout.Button("Remove Specific Path"))
            {
                thisItem.RemovePath(int.Parse(ThisField) - 1);
            }

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
#endif
    #endregion

    public void GenerateLine()
    {
        GameObject thisGO = Instantiate(LinePathPrefab, SpawnPoints);
        theCurrentNo = myListPath.Count;
        myListPath.Add(thisGO);
        theCurrentNo += 1;
        thisGO.GetComponent<PathDrawer>().myCurrentNo = theCurrentNo;
    }


    public void RemovePath(int itemNo)
    {
        DestroyImmediate(myListPath[itemNo]);
        myListPath.RemoveAt(itemNo);
        theCurrentNo = myListPath.Count;
        for (int i = 0; i <= myListPath.Count; i++)
        {
            myListPath[i - 1].GetComponent<PathDrawer>().myCurrentNo = i;
        }
    }
}

*/