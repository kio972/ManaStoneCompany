using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    // Start is called before the first frame update
    public static float scaleHeight = ((float)Screen.width / Screen.height) / ((float)9 / 16);
    public static float scaleWidth = 1f / scaleHeight;
    void Awake()
    {
        //Camera cam = Camera.main;
        Camera cam = gameObject.GetComponent<Camera>();
        Rect rect = cam.rect;

        //float scaleHeight = ((float)Screen.width / Screen.height) / ((float)16/9);
        //float scaleWidth = 1f / scaleHeight;
        if (scaleHeight < 1)
        {
            rect.height = scaleHeight;
            rect.y = (1f - scaleHeight) / 2f;
        }
        else
        {
            rect.width = scaleWidth;
            rect.x = (1f - scaleWidth) / 2f;
        }
        cam.rect = rect;
    }
}
