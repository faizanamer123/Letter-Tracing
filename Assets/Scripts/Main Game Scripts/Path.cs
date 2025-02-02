using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Path
{
    public List<Vector2> points;
    public Path(Vector2 centre)
    {
        points = new List<Vector2>
        {
            centre + Vector2.left
            , centre + Vector2.left * 0.35f
            , centre + Vector2.right * 0.35f
            , centre + Vector2.right
            
        };
    }
    public Vector2 this[int i] => points[i];
    public int NumPoints => points.Count;

    public int NumSegements => (points.Count - 4) / 3 + 1;

    public void AddSegments(Vector2 anchorPos)
    {
        points.Add(anchorPos);
    }

    public Vector2[] GetPointsInSegments(int i) =>
        new Vector2[]
        {
            points[i*3] , points[i*3+1] , points[i*3+2] , points[i*3+3]
        };

    public void MovePoint(int i , Vector2 pos) => points[i] = pos;

}

/*
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path
{
    public List<Vector2> points;

    public Path(Vector2 center)
    {
        points = new List<Vector2>
        {
            center + Vector2.left,
            center + Vector2.left * 0.35f,
            center + Vector2.right * 0.35f,
            center + Vector2.right
        };
    }

    public Vector2 this[int i] => points[i];
    public int NumPoints => points.Count;

    public void AddSegment(Vector2 newPoint)
    {
        points.Add(newPoint);
    }

    public void MovePoint(int index, Vector2 newPosition)
    {
        if (index >= 0 && index < points.Count)
        {
            points[index] = newPosition;
        }
    }
}
*/




/*
I am creating a letter tracing game, but I am facing an issue. When I click on the starting point of
each path, the entire path fills instantly instead of following a smooth tracing motion. For example, 
for the letter 'A', there are three paths (Path 1, Path 2, and Path 3). When I click on the starting 
point of any path, the whole path fills at once, making the experience slow and laggy.
I want the tracing to be smooth and natural, especially for kids. Since children place their fingers on the path and
move along it to trace the letter, the filling effect should mimic their real hand movements instead of instantly
completing the path. It should not look like a simple paint fill but should resemble real path tracing. The focus
should be on completing the path rather than just filling it instantly. The game should feel realistic, engaging,
and visually appealing for children, making the tracing experience as close to real-life handwriting practice as possible.
 */