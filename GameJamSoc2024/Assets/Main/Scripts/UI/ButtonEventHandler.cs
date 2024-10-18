using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEventHandler : MonoBehaviour
{
    [System.Serializable]
    public struct ButtonKeybind
    {
        public Button button;
        public KeyCode key;
    }

    public List<ButtonKeybind> buttonKeybinds;

    // Update is called once per frame
    void Update()
    {
        foreach (var buttonKeybind in buttonKeybinds)
        {
            if (Input.GetKeyDown(buttonKeybind.key))
            {
                buttonKeybind.button.onClick.Invoke();
            }
        }
    }
}