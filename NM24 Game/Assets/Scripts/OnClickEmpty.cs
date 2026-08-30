using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnClickEmpty : MonoBehaviour
{
    public static event Action OnMouseDown;
    public void OnClick(InputAction.CallbackContext context)
    {
        Debug.Log("Player Clicked");
        OnMouseDown?.Invoke();
   }
}
