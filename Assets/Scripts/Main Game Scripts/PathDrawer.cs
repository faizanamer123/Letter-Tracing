using System.Collections.Generic;
using UnityEngine;

public class PathDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Path path; // Path contains List<Vector2> points
    private float[] segmentLengths;
    private float totalPathLength;
    private float currentProgress = 0f; // Progress from 0 to 1
    private float distanceTraveled = 0f; // Track the distance the pointer has traveled
    public int myCurrentNo;
    public bool isCircle;
    private void Start()
    {
        InitializePath();
    }

    public void InitializePath()
    {
        if (path == null || path.points == null || path.points.Count < 2) return;

        segmentLengths = new float[path.points.Count - 1];
        totalPathLength = 0f;

        // Calculate the segment lengths
        for (int i = 0; i < path.points.Count - 1; i++)
        {
            segmentLengths[i] = Vector2.Distance(path.points[i], path.points[i + 1]);
            totalPathLength += segmentLengths[i];
        }
    }

    // Call this from TouchMovementHandler to update the progress
    public void UpdateTracedProgress(Vector3 pointerPosition)
    {
        distanceTraveled = CalculateDistanceTraveled(pointerPosition);
        currentProgress = Mathf.Clamp01(distanceTraveled / totalPathLength); // Calculate progress based on distance

        DrawTracedPath();
    }

    private float CalculateDistanceTraveled(Vector3 pointerPosition)
    {
        float distance = 0f;
        Vector2 lastPoint = path.points[0];

        for (int i = 1; i < path.points.Count; i++)
        {
            float segmentLength = Vector2.Distance(path.points[i - 1], path.points[i]);
            if (Vector2.Distance(lastPoint, pointerPosition) >= segmentLength)
            {
                distance += segmentLength;
                lastPoint = path.points[i];
            }
            else
            {
                distance += Vector2.Distance(lastPoint, pointerPosition);
                break;
            }
        }

        return distance;
    }

    public void DrawTracedPath()
    {
        List<Vector3> tracedPoints = new List<Vector3>();
        float tracedLength = currentProgress * totalPathLength;
        float accumulatedLength = 0f;

        for (int i = 0; i < path.points.Count - 1; i++)
        {
            float segmentLength = segmentLengths[i];

            if (accumulatedLength + segmentLength <= tracedLength)
            {
                tracedPoints.Add((Vector3)path.points[i]);
                tracedPoints.Add((Vector3)path.points[i + 1]);
                accumulatedLength += segmentLength;
            }
            else
            {
                float remainingLength = tracedLength - accumulatedLength;
                Vector2 interpolatedPoint = Vector2.Lerp(path.points[i], path.points[i + 1], remainingLength / segmentLength);
                tracedPoints.Add((Vector3)path.points[i]);
                tracedPoints.Add(interpolatedPoint);
                break;
            }
        }

        lineRenderer.positionCount = tracedPoints.Count;
        lineRenderer.SetPositions(tracedPoints.ToArray());
    }
}




/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDrawer : MonoBehaviour
{
    public Path path;
    private LineRenderer lineRenderer;
    public int myCurrentNo;
    public bool isCircle;
    [SerializeField] private float[] pointDis;

    private void Start()
    {
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.widthMultiplier = 0.2f;
            lineRenderer.useWorldSpace = true;
        }

        // Initialize the path
        if (path == null || path.points == null || path.points.Count == 0)
        {
            CreatePath();
        }
    }

    /*
    public void createPath()
    {
        path = new Path(transform.position);
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = path.points.Count;
        UpdateLineRenderer();
    }
    

    public void CreatePath()
    {
        path = new Path(transform.position);
        UpdateLineRenderer();
    }

    /*
    public void DrawPath(List<Vector2> points) //old
    {
        if (points == null || points.Count == 0)
            return;

        CalculateDisPoints(points);

        // Ensure EdgeCollider2D exists
        EdgeCollider2D edgeCollider = gameObject.GetComponent<EdgeCollider2D>();
        if (edgeCollider == null)
        {
            edgeCollider = gameObject.AddComponent<EdgeCollider2D>();
        }

        edgeCollider.points = points.ToArray();

        // Update LineRenderer
        lineRenderer.positionCount = points.Count;
        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(points[i].x, points[i].y, 0));
        }
    }
    

    public void DrawPath(List<Vector2> points)
    {
        if (points == null || points.Count == 0)
            return;

        // Update EdgeCollider2D
        EdgeCollider2D edgeCollider = gameObject.GetComponent<EdgeCollider2D>();
        if (edgeCollider == null)
        {
            edgeCollider = gameObject.AddComponent<EdgeCollider2D>();
        }
        edgeCollider.points = points.ToArray();

        // Update LineRenderer
        lineRenderer.positionCount = points.Count;
        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(points[i].x, points[i].y, 0));
        }
    }

    private void CalculateDisPoints(List<Vector2> points)
    {
        
        if (points == null || points.Count < 2) //old
            return;

        pointDis = new float[points.Count - 1];
        for (int i = 0; i < pointDis.Length; i++)
        {
            Vector2 point_A = points[i];
            Vector2 point_B = points[i + 1];
            pointDis[i] = Vector2.Distance(point_A, point_B);
        }
        
        if (points == null || points.Count < 2) return;

        pointDis = new float[points.Count - 1];
        for (int i = 0; i < pointDis.Length; i++)
        {
            pointDis[i] = Vector2.Distance(points[i], points[i + 1]);
        }
    }

    private void UpdateLineRenderer()
    {
        if (lineRenderer == null || path == null)
            return;

        List<Vector2> points = path.points;
        lineRenderer.positionCount = points.Count;

        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(points[i].x, points[i].y, 0));
        }
    }

    public void InitializePath(Vector2 startPosition)
    {
        if (path == null)
        {
            path = new Path(startPosition);
            UpdateLineRenderer();
        }
    }


    private void OnDrawGizmos()
    {
        if (path == null || path.points == null || path.points.Count == 0)
            return;

        Gizmos.color = Color.green;
        foreach (Vector2 point in path.points)
        {
            Gizmos.DrawSphere(point, 0.1f);
        }
    }
}
*/