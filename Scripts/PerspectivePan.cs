using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this custom script handles the pan and zoom of the camera 
//scripts is held in the PerspectiveHolder gameobject that contains the camera.
public class PerspectivePan : MonoBehaviour
{
    [SerializeField]
    public static bool IsFreeCam = true; // defines whether camera is free or anchored to player.

    private Vector3 touchStart; // used in determining pan directon.


    [SerializeField]
    private Camera Cam; // the cam being controled

    //various other factors.
    [SerializeField]
    private float groundZ = 0;
    [SerializeField]
    private float zoomSpeed = 3;
    private int zoomDirection = 0;

    private Vector3 freeCamPosition = new Vector3(0, 70, 0);

    [SerializeField]
    private Transform Player;// player the camera is anchored too


    private void Start()
    {
        freeCamPosition.y = transform.position.y;
        touchStart = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        //Controls camera position
        if (IsFreeCam)
        {
            if (Input.GetMouseButtonDown(0))
            {
                touchStart = GetWorldPosition(groundZ);
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 direction = touchStart - GetWorldPosition(groundZ);
                transform.position += direction;
            }
        }
        else
        {
            transform.position = Player.position + new Vector3(0, PauseMenu.CurrentMazeType.minZoom, 0);
        }

        if ((zoomDirection == -1 && transform.position.y > PauseMenu.CurrentMazeType.minZoom) || (zoomDirection == 1 && transform.position.y < PauseMenu.CurrentMazeType.maxZoom))
        {
            transform.position -= zoomSpeed * zoomDirection * Vector3.down;
        }
    }

    // gets world position when in perspective mode.
    private Vector3 GetWorldPosition(float z)
    {
        Ray mousePos = Cam.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, new Vector3(0, 0, z));
        float distance;
        ground.Raycast(mousePos, out distance);
        return mousePos.GetPoint(distance);
    }

    public void ZoomIn(bool Pressed)
    {
        if (Pressed) zoomDirection = -1;
        else zoomDirection = 0;
    }
    public void ZoomOut(bool Pressed)
    {
        if (Pressed) zoomDirection = +1;
        else zoomDirection = 0;
    }

    public void SetCamMode()
    {
        IsFreeCam = !IsFreeCam;

        if (IsFreeCam)
        {
            transform.position = freeCamPosition;
        }
        else
        {
            freeCamPosition.y = transform.position.y;
        }
    }
}
