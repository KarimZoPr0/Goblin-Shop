using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public void ShakeCamera(float strength = 0.1f, float duration = 0.1f)
    {
        transform.parent.DOShakePosition(duration, strength, 10, 90);
    }
}
