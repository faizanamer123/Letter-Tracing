using UnityEngine;

public class GuideSpriteHandler : MonoBehaviour
{
    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == TouchMovementHandler.Instance.currentNumPath)
            {
                transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            }
            else
            {
                transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
            }
            if (i < TouchMovementHandler.Instance.currentNumPath)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}