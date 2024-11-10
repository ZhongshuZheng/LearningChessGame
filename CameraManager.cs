using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Camera Manager
/// </summary>
public class CameraManager
{
    Transform cameraTf;
    Vector3 prePos;

    public CameraManager() {
        cameraTf = Camera.main.transform;
        prePos = cameraTf.transform.position;
    }

    public void SetPosition(Vector3 pos) {
        pos.z = cameraTf.position.z;
        cameraTf.position = pos;
    }

    public void ResetPostion() {
        cameraTf.position = prePos;
    }
}
