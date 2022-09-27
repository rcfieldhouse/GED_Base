using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public PlayerAction inputAction;

    public static PlayerInputController controller;
    // Start is called before the first frame update
    void Awake()
    {
        inputAction = new PlayerAction();
        //this do be the singleton logic
        if (controller == null)
        {
            controller = this;
        }
       
    }
    private void OnEnable()
    {
        inputAction.Enable();
    }
    private void OnDisable()
    {
        inputAction.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
