using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//defines what buttons are shown based on the current camera mode
public class UIControls : MonoBehaviour
{
    [SerializeField]
    private GameObject Joystick;
    [SerializeField]
    private GameObject ZoomInButton;
    [SerializeField]
    private GameObject ZoomOutButton;

    private void Start()
    {
        SwitchControls();
    }
    // Start is called before the first frame update
    void Update()
    {
        SwitchControls();
    }

    public void SwitchControls()
    {
        if (Joystick.activeSelf == PerspectivePan.IsFreeCam)
        {
            Joystick.SetActive(!PerspectivePan.IsFreeCam);
            ZoomInButton.SetActive(PerspectivePan.IsFreeCam);
            ZoomOutButton.SetActive(PerspectivePan.IsFreeCam);
        }
    }
}
