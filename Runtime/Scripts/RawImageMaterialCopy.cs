using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RawImageMaterialCopy : MonoBehaviour
{
    RawImage rawImage;

    void Awake()
    {
        rawImage = GetComponent<RawImage>();
        rawImage.material = new Material(rawImage.material);
    }
}
