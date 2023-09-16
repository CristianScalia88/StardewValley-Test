using System;
using UnityEngine;

public class InputBlocker : MonoBehaviour
{
    private void OnEnable()
    {
        InputsManager.Instance.AddInputBlocker(this);
    }

    private void OnDisable()
    {
        InputsManager.Instance.RemoveInputBlocker(this);
    }
}