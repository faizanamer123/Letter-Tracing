using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerAlignmentChecker : MonoBehaviour
{
    public GameObject myMask;
    private GameObject maskParent;

    private void Start()
    {
        maskParent = GameObject.FindGameObjectWithTag("Mask");
        if (maskParent == null)
        {
            Debug.LogError("No object with tag 'Mask' found in the scene!");
        }
    }

    private void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;  // Ensure the mask is placed at the correct Z level for 2D space

        if (TouchMovementHandler.Instance != null && TouchMovementHandler.Instance.isAlign)
        {
            CreateMaskAtPosition(pos);
            // Update progress on the current path
            PathDrawer currentPathDrawer = PathGenerateHandler.instance.myListPath[TouchMovementHandler.Instance.currentNumPath].GetComponent<PathDrawer>();
            if (currentPathDrawer != null)
            {
                currentPathDrawer.UpdateTracedProgress(pos); // Update traced progress based on the pointer position
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            DestroyAllMasks();
        }
    }

    private void CreateMaskAtPosition(Vector3 position)
    {
        GameObject mask = Instantiate(myMask, position, Quaternion.identity);

        if (maskParent != null)
        {
            mask.transform.SetParent(maskParent.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("myPath"))
        {
            TouchMovementHandler.Instance.isAlign = true; // Set alignment flag when entering the path
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("myPath"))
        {
            TouchMovementHandler.Instance.isAlign = false; // Reset alignment flag when exiting the path
            DestroyAllMasks();
        }
    }

    private void DestroyAllMasks()
    {
        if (maskParent != null && maskParent.transform.childCount > 0)
        {
            foreach (Transform child in maskParent.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
