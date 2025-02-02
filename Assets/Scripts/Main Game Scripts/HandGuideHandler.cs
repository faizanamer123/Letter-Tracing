using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGuideHandler : MonoBehaviour
{
    public static HandGuideHandler Instance;
    public GameObject handGuideGO;
    private Vector3 currentPos;
    private Vector3 nextPos;
    public bool isShowGuide;

    [SerializeField] private int currentCount;
    [SerializeField] private int countRepeatGuideHand;
    [SerializeField] private float speed;

    private PathDrawer _pathDrawer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        handGuideGO.SetActive(false);
        isShowGuide = true;
    }

    private void Update()
    {
        if (!GameManager.instance.isGameEnded && isShowGuide)
        {
            ShowHandGuide();
        }
    }

    public void ShowHandGuide()
    {
        if (!handGuideGO || PathGenerateHandler.instance == null || TouchMovementHandler.Instance == null)
        {
            Debug.LogWarning("Missing references in HandGuideHandler.ShowHandGuide()");
            return;
        }

        handGuideGO.SetActive(true);

        var currentPathObject = PathGenerateHandler.instance.myListPath[TouchMovementHandler.Instance.currentNumPath];
        if (currentPathObject != null)
        {
            _pathDrawer = currentPathObject.GetComponent<PathDrawer>();
        }

        if (_pathDrawer == null || _pathDrawer.path == null || _pathDrawer.path.points == null)
        {
            Debug.LogError("PathDrawer or path points not properly initialized in HandGuideHandler.");
            return;
        }

        if (currentCount < _pathDrawer.path.points.Count)
        {
            if (Vector3.Distance(handGuideGO.transform.position, currentPos) < 0.1f)
            {
                currentCount++;
                if (currentCount < _pathDrawer.path.points.Count)
                {
                    nextPos = _pathDrawer.path.points[currentCount];
                    currentPos = nextPos;
                    SetSpeed();
                }
                else
                {
                    ResetGuide();
                }
            }

            handGuideGO.transform.position = Vector3.MoveTowards(handGuideGO.transform.position, currentPos, speed * Time.deltaTime);
        }
    }

    private void ResetGuide()
    {
        if (_pathDrawer == null || _pathDrawer.path == null || _pathDrawer.path.points == null)
        {
            Debug.LogError("ResetGuide failed due to uninitialized PathDrawer or path.");
            return;
        }

        if (countRepeatGuideHand < 2)
        {
            countRepeatGuideHand++;
            currentCount = 0;
            currentPos = _pathDrawer.path.points[0];
            handGuideGO.transform.position = currentPos;
        }
        else
        {
            TouchMovementHandler.Instance.NextPath();
            DefaultGuideHand();
        }
    }

    public void DefaultGuideHand()
    {
        countRepeatGuideHand = 0;
        isShowGuide = false;
        currentCount = 0;
        handGuideGO.SetActive(false);
    }

    private void SetSpeed()
    {
        speed = 5f;
    }
}


/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGuideHandler : MonoBehaviour
{
    public static HandGuideHandler Instance;
    public GameObject handGuideGO;
    private Vector3 currentPos;
    private Vector3 nextPos;
    public bool isShowGuide;

    [SerializeField] private int currentCount;
    [SerializeField] private int countRepeatGuideHand;
    [SerializeField] private float speed;

    private PathDrawer _pathDrawer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        handGuideGO.SetActive(false);
        isShowGuide = true;
    }

    private void Update()
    {
        if (!GameManager.instance.isGameEnded && isShowGuide)
        {
            ShowHandGuide();
        }
    }

    public void ShowHandGuide()
    {
        // Ensure handGuideGO is active and check _pathDrawer initialization
        if (!handGuideGO || PathGenerateHandler.instance == null || TouchMovementHandler.Instance == null)
        {
            Debug.LogWarning("Missing references in HandGuideHandler.ShowHandGuide()");
            return;
        }

        handGuideGO.SetActive(true);

        // Retrieve the current path drawer
        var currentPathObject = PathGenerateHandler.instance.myListPath[TouchMovementHandler.Instance.currentNumPath];
        if (currentPathObject != null)
        {
            _pathDrawer = currentPathObject.GetComponent<PathDrawer>();
        }

        if (_pathDrawer == null || _pathDrawer.path == null || _pathDrawer.path.points == null)
        {
            Debug.LogError("PathDrawer or path points not properly initialized in HandGuideHandler.");
            return;
        }

        // Only proceed if there are path points
        if (currentCount < _pathDrawer.path.points.Count)
        {
            // If the hand guide has reached the current target, move to the next point
            if (Vector3.Distance(handGuideGO.transform.position, currentPos) < 0.1f)
            {
                currentCount++;
                if (currentCount < _pathDrawer.path.points.Count)
                {
                    nextPos = _pathDrawer.path.points[currentCount];
                    currentPos = nextPos;
                    SetSpeed(); // Adjust speed if needed
                }
                else
                {
                    ResetGuide(); // Reset guide after the last point of the path
                }
            }

            // Move towards the next position gradually
            handGuideGO.transform.position = Vector3.MoveTowards(handGuideGO.transform.position, currentPos, speed * Time.deltaTime);
        }
    }

    private void ResetGuide()
    {
        if (_pathDrawer == null || _pathDrawer.path == null || _pathDrawer.path.points == null)
        {
            Debug.LogError("ResetGuide failed due to uninitialized PathDrawer or path.");
            return;
        }

        if (countRepeatGuideHand < 2)
        {
            countRepeatGuideHand++;
            currentCount = 0;
            currentPos = _pathDrawer.path.points[0]; // Go back to the start
            handGuideGO.transform.position = currentPos;
        }
        else
        {
            // If paths are complete, reset the guide and move to the next path
            TouchMovementHandler.Instance.NextPath();
            DefaultGuideHand(); // End the guide after the path is completed
        }
    }

    public void DefaultGuideHand()
    {
        countRepeatGuideHand = 0;
        isShowGuide = false;
        currentCount = 0;
        handGuideGO.SetActive(false); // Deactivate hand guide after completion
    }

    private void SetSpeed()
    {
        speed = 5f; // Set the speed of hand guide, adjust if needed for smoothness
    }
}

*/

/*
public class HandGuideHandler : MonoBehaviour
{
    public static HandGuideHandler Instance;
    public GameObject handGuideGO;
    public Vector3 currentPos;
    public Vector3 nextPos;
    public bool isShowGuide;
    [SerializeField] private int currentCount , CountRepeatGuideHand;
    [SerializeField] private float speed;
    PathDrawer _pathDrawer;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        handGuideGO.SetActive(false);
        isShowGuide = true;
    }

    void Update()
    {
        if (GameManager.instance.isGameEnded)
        {
            if (isShowGuide) ShowHandGuide();
        }
    }

    public void ShowHandGuide()
    {
        handGuideGO.SetActive(true);
        _pathDrawer = PathGenerateHandler.instance.myListPath[TouchMovementHandler.Instance.currentNumPath].GetComponent<PathDrawer>();

        if(currentPos == handGuideGO.transform.position)
        {
            if(currentCount < _pathDrawer.path.points.Count)
            {
                currentCount += 1;
                SetSpeed();
                nextPos = _pathDrawer.path.points[currentCount - 1];
                currentPos = (Vector3)nextPos;
                handGuideGO.transform.position = Vector3.MoveTowards(handGuideGO.transform.position, currentPos, speed + Time.deltaTime);
            }
            else
            {
                ResetGuide();
            }
        }
        else
        {
            if (currentCount > 1)
            {
                handGuideGO.transform.position = Vector3.MoveTowards(handGuideGO.transform.position, currentPos, speed + Time.deltaTime);
            }
            else
            {
                nextPos = _pathDrawer.path.points[0];
                currentPos = (Vector3)nextPos;
                handGuideGO.transform.position = currentPos;
            }
        }
    }

    private void ResetGuide()
    {
        if(CountRepeatGuideHand < 2)
        {
            isShowGuide = true;
            CountRepeatGuideHand += 1;
            currentCount = 0;

        }
        else
        {
            DefaultGuideHand();
        }
    }

    public void DefaultGuideHand()
    {
        CountRepeatGuideHand = 0;
        isShowGuide = false;
        currentCount = 0;
        handGuideGO.SetActive(false);
    }

    private void SetSpeed()
    {
        if (_pathDrawer.isCircle)
        {
            speed = Vector2.Distance(_pathDrawer.path.points[0], _pathDrawer.path.points[_pathDrawer.path.points.Count - 1]) + 5f;
        }
        else
        {
            speed = Vector2.Distance(_pathDrawer.path.points[0], _pathDrawer.path.points[_pathDrawer.path.points.Count - 1]) + 0.54f;
        }
    }
}
*/