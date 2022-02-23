using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShotTaker : MonoBehaviour
{
    public int superSize = 1;
    private int _shotIndex = 0;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.A)) {
            ScreenCapture.CaptureScreenshot($"Screenshot{_shotIndex}.png",superSize);
            _shotIndex++;
        }
    }
}
