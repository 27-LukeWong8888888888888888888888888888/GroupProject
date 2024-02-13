using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiFollowPlayer : MonoBehaviour
{
    public Transform ObjToFollow;
    RectTransform rectTransform;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        if (ObjToFollow != null)
        {
            rectTransform.transform.position = ObjToFollow.transform.position;
        }
    }
}
