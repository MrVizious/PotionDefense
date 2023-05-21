using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;
using UnityEngine.InputSystem;
using DesignPatterns;

[RequireComponent(typeof(Rigidbody2D))]
public class UIState : PlayerState
{
    public override void Shoot(InputAction.CallbackContext c) { }
}
