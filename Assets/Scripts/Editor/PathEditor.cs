using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathDrawer))]
public class PathEditor : Editor
{
    private PathDrawer creator;
    private Path path => creator.path;

    private const float handleSize = 0.1f; // Size of the handles in the editor
    private const float pickSize = 0.15f; // Clickable area size for the handles

    private void OnEnable()
    {
        creator = (PathDrawer)target;

        // Initialize the path if it hasn't been set
        if (creator.path == null || creator.path.points == null)
        {
            creator.InitializePath();
        }
    }

    private void OnSceneGUI()
    {
        HandleInput();
        DrawPath();
    }

    private void HandleInput()
    {
        Event guiEvent = Event.current;
        Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;

        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift)
        {
            Undo.RecordObject(creator, "Add Segment");
            path.AddSegments(mousePos);
            creator.InitializePath();
            creator.DrawTracedPath();  // Ensure the path updates after adding a segment
        }
    }

    private void DrawPath()
    {
        // Draw the path points as editable handles
        Handles.color = Color.red;

        for (int i = 0; i < path.NumPoints; i++)
        {
            Vector2 point = path.points[i];

            // Allow free movement of the point
            EditorGUI.BeginChangeCheck();
            var fmh_56_62_638740160526310830 = Quaternion.identity; Vector2 newPoint = Handles.FreeMoveHandle(point, handleSize, Vector2.zero, Handles.CylinderHandleCap);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(creator, "Move Point");
                path.points[i] = newPoint;
                creator.InitializePath(); // Reinitialize path after point move
                creator.DrawTracedPath();
            }
        }

        // Draw lines between points
        Handles.color = Color.green;

        for (int i = 0; i < path.NumPoints - 1; i++)
        {
            Handles.DrawLine(path.points[i], path.points[i + 1]);
        }

        // Handle closed path if applicable
        if (creator.isCircle && path.NumPoints > 2)
        {
            Handles.DrawLine(path.points[path.NumPoints - 1], path.points[0]);
        }
    }
}



/*
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathDrawer))]
public class PathEditor : Editor
{
    private PathDrawer drawer;
    private Path path => drawer.path;

    private void OnEnable()
    {
        drawer = (PathDrawer)target;

        if (drawer.path == null)
        {
            drawer.CreatePath();
        }
    }

    private void OnSceneGUI()
    {
        HandleInput();
        DrawPoints();
    }

    private void HandleInput()
    {
        Event guiEvent = Event.current;
        Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;

        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift)
        {
            Undo.RecordObject(drawer, "Add Segment");
            path.AddSegment(mousePos);
            drawer.DrawPath(path.points);
        }
    }

    private void DrawPoints()
    {
        Handles.color = Color.red;
        for (int i = 0; i < path.NumPoints; i++)
        {
            var fmh_103_62_638734422427208132 = Quaternion.identity; Vector2 newPos = Handles.FreeMoveHandle(path[i], 0.1f, Vector3.zero, Handles.SphereHandleCap);

            if (newPos != path[i])
            {
                Undo.RecordObject(drawer, "Move Point");
                path.MovePoint(i, newPos);
                drawer.DrawPath(path.points);
            }
        }
    }
}
*/