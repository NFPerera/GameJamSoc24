using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudEventHandler : MonoBehaviour
{
    [System.Serializable]
    public struct ButtonKeybind
    {
        public Button button;
        public KeyCode key;
    }

    public List<ButtonKeybind> buttonKeybinds;

    public GameObject pauseMenu;

    public GameObject gameManager;
    public GameObject hud;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (pauseMenu.activeSelf)
            {
                hud.SetActive(true);
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
            }
            else
            {
                hud.SetActive(false);
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
            }

            if (gameManager != null)
            {
                // gameManager.GetComponent<GameManager>().PauseGame();
            }

        }

        foreach (var buttonKeybind in buttonKeybinds)
        {
            if (Input.GetKeyDown(buttonKeybind.key))
            {
                buttonKeybind.button.onClick.Invoke();

                // Trigger the button transition
                switch (buttonKeybind.button.transition)
                {
                    case Selectable.Transition.Animation:
                        var animator = buttonKeybind.button.GetComponent<Animator>();
                        if (animator != null)
                        {
                            animator.SetTrigger("Press");
                        }
                        break;

                    case Selectable.Transition.ColorTint:
                        // Trigger button's transition to the pressed state
                        var buttonColors = buttonKeybind.button.colors;
                        buttonKeybind.button.targetGraphic.CrossFadeColor(buttonColors.pressedColor, buttonColors.fadeDuration, true, true);

                        // Wait for the fade duration, then reset color
                        StartCoroutine(ResetButtonColor(buttonKeybind.button, buttonColors.normalColor, buttonColors.fadeDuration));
                        break;

                    case Selectable.Transition.SpriteSwap:
                        var spriteState = buttonKeybind.button.spriteState;
                        buttonKeybind.button.targetGraphic.GetComponent<Image>().sprite = spriteState.pressedSprite;
                        StartCoroutine(ResetSprite(buttonKeybind.button, spriteState.highlightedSprite));
                        break;
                }


            }
        }

        IEnumerator ResetButtonColor(Button button, Color targetColor, float duration)
{
    yield return new WaitForSeconds(duration); // Wait for the duration of the color fade
    button.targetGraphic.CrossFadeColor(targetColor, duration, true, true);
}


        IEnumerator ResetSprite(Button button, Sprite normalSprite)
        {
            yield return new WaitForSeconds(0.1f);
            button.targetGraphic.GetComponent<Image>().sprite = normalSprite;
        }
    }
}