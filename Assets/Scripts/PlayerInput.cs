using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : GameInput
{
    public override Vector2 GetInputs()
    {
        return new Vector2
        {
            x = Input.GetAxis("Horizontal"),
            y = Input.GetAxis("Vertical")
        };
    }
}
