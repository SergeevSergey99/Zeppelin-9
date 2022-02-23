using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScript : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject brush;

    LineRenderer currentLineRenderer;

    Vector2 lastPos;

    private void Update()
    {
        Draw();
    }

    void Draw() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            CreateBrush();
        }
        if (Input.GetKey(KeyCode.Mouse0)) {
            PointToMousePos();
        }
        else {
            currentLineRenderer = null;
        }
    }

    void CreateBrush() {
        GameObject brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1 , mousePos);
    }

     void AddAPoint(Vector2 pointPos) 
    {
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

    void PointToMousePos() 
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos != lastPos) 
        {
            AddAPoint(mousePos);
            lastPos = mousePos;
        }
    }
}
