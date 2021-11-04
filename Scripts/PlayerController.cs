using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//handles player movement
public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private Joystick joystick;

    [SerializeField]
    private CharacterController controller;
    private float speed=7;

    [SerializeField]
    private Rigidbody Player;
    private void Update()
    {
        
            Vector3 move = new Vector3(joystick.GetComponent<Joystick>().Horizontal, 0, joystick.GetComponent<Joystick>().Vertical);

            controller.Move(move * Time.deltaTime * speed);
        
    }
}
