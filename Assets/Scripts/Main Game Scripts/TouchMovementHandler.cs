using System.Collections.Generic;
using UnityEngine;

public class TouchMovementHandler : MonoBehaviour
{
    public static TouchMovementHandler Instance;
    public GameObject PointerPrefab;
    private GameObject pointerGO;

    private Plane touchPlane;
    private float rayDistance;

    public int currentNumPath = 0;
    private int currentPointIndex = 0;
    public bool isAlign = false; // This flag is used to check if the pointer is aligned with the path

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        touchPlane = new Plane(Camera.main.transform.forward, Vector3.zero);
    }

    private void Update()
    {
        HandleTouch();
    }

    private void HandleTouch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartPointer();
        }
        else if (Input.GetMouseButton(0))
        {
            UpdatePointer();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ResetPointer();
        }
    }

    public void NextPath()
    {
        if (currentNumPath < PathGenerateHandler.instance.myListPath.Count - 1)
        {
            currentNumPath++; // Move to the next path
            currentPointIndex = 0; // Reset point index
            isAlign = false; // Reset alignment state, assuming you want to reset the alignment check.
            Debug.Log("Moving to next path: " + currentNumPath);
        }
        else
        {
            Debug.Log("All paths completed!");
        }
    }


    private void StartPointer()
    {
        if (TryGetPointerPosition(out Vector3 pointerPosition))
        {
            pointerGO = Instantiate(PointerPrefab, pointerPosition, Quaternion.identity);
        }
    }

    private void UpdatePointer()
    {
        if (pointerGO != null && TryGetPointerPosition(out Vector3 pointerPosition))
        {
            pointerGO.transform.position = pointerPosition;

            // Check for path alignment
            if (isAlign)
            {
                PathDrawer currentPathDrawer = PathGenerateHandler.instance.myListPath[currentNumPath].GetComponent<PathDrawer>();
                if (currentPathDrawer != null)
                {
                    float progress = CalculateProgressOnPath(pointerPosition, currentPathDrawer.path.points);
                    currentPathDrawer.UpdateTracedProgress(pointerPosition); // Update the tracing progress
                    // If tracing is complete, move to the next path
                    if (progress >= 1f)
                    {
                        CompletePath();
                    }
                }
            }
        }
    }

    private void ResetPointer()
    {
        if (pointerGO != null)
        {
            Destroy(pointerGO);
        }
    }

    private float CalculateProgressOnPath(Vector3 pointerPosition, List<Vector2> pathPoints)
    {
        float totalDistance = 0f, tracedDistance = 0f;

        for (int i = 0; i < pathPoints.Count - 1; i++)
        {
            float segmentLength = Vector2.Distance(pathPoints[i], pathPoints[i + 1]);
            totalDistance += segmentLength;

            if (tracedDistance + segmentLength >= Vector2.Distance(pointerPosition, pathPoints[i]))
            {
                float segmentProgress = Vector2.Distance(pointerPosition, pathPoints[i]) / segmentLength;
                return (tracedDistance + (segmentLength * segmentProgress)) / totalDistance;
            }
            tracedDistance += segmentLength;
        }

        return 1f; // Full progress if pointer is beyond the last point
    }

    private void CompletePath()
    {
        if (currentNumPath < PathGenerateHandler.instance.myListPath.Count - 1)
        {
            currentNumPath++;
            currentPointIndex = 0;
        }
        else
        {
            Debug.Log("All paths completed!");
        }
    }

    private bool TryGetPointerPosition(out Vector3 pointerPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (touchPlane.Raycast(ray, out rayDistance))
        {
            pointerPosition = ray.GetPoint(rayDistance);
            return true;
        }

        pointerPosition = Vector3.zero;
        return false;
    }
}
