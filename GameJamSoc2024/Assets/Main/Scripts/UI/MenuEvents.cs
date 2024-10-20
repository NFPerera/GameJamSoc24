using System.Collections;
using System.Collections.Generic;
using Main.Scripts.TowerDefenseGame._Managers;
using UnityEngine;
using UnityEngine.UIElements;

public class TDHudEvents : MonoBehaviour
{
    private UIDocument _uiDocument;
    private List<Button> _buttons = new List<Button>();
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private GameObject hud;

    [SerializeField] private GameObject mainMenu;
    
    [SerializeField] private GameObject credits;

    [SerializeField] private GameObject gameOver;
    [SerializeField] private bool isGameOver;
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        // Pause the game on start
        Time.timeScale = 0f;
        hud.SetActive(false);
        _uiDocument = GetComponent<UIDocument>();
        _audioSource = GetComponent<AudioSource>();
        _buttons = _uiDocument.rootVisualElement.Query<Button>().ToList();
        foreach (var button in _buttons)
        {
            // Register the button click event using a named method
            button.RegisterCallback<ClickEvent>(OnButtonClicked);
        }
    }

    private void OnEnable()
    {

        Time.timeScale = 0f;
        hud.SetActive(false);

        // Disable input events
        Input.simulateMouseWithTouches = false;

        // Re-query buttons on enable to ensure they're the active UI buttons
        _buttons = _uiDocument.rootVisualElement.Query<Button>().ToList();

        foreach (var button in _buttons)
        {
            // Register the button click event using a named method
            button.RegisterCallback<ClickEvent>(OnButtonClicked);
        }
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
        //hud.SetActive(true);
        foreach (var button in _buttons)
        {
            // Unregister using the same named method to ensure it's properly removed
            button.UnregisterCallback<ClickEvent>(OnButtonClicked);
        }
    }


    // This will be called whenever any button is clicked
    private void OnButtonClicked(ClickEvent ev)
    {
        var button = (Button)ev.target;
        var buttonName = button.name;

        switch (buttonName)
        {
            case "StartButton":
                StartGame();
                break;
            case "CreditsButton":
                CreditsScreen();
                break;
            case "QuitButton":
                QuitGame();
                break;
            case "QuitToMainMenuButton":
                QuitToMainMenuGame();
                break;
            case "ResumeButton":
                ResumeGame();
                break;
            
            default:
                Debug.Log("Unknown button clicked");
                break;
        }
    }

    private void StartGame()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);

        // gameManager.GetComponent<GameManager>().StartGame();
    }

    private void QuitToMainMenuGame()
    {
        if (isGameOver)
        {
            gameManager.ResetGame();
        }
        Time.timeScale = 0f;
        hud.SetActive(false);
        gameObject.SetActive(false);
        credits.SetActive(false);
        mainMenu.SetActive(true);
        // call teh reset logic.
    }

    private void CreditsScreen()
    {
        // Show credits screen
        hud.SetActive(false);
        gameObject.SetActive(false);
        credits.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        hud.SetActive(false);
        gameObject.SetActive(false);
        credits.SetActive(false);
        mainMenu.SetActive(false);
        gameOver.SetActive(true);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        hud.SetActive(true);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var button in _buttons)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (button.name == "PauseButton")
                {
                    if (gameObject.activeSelf)
                    {
                        Time.timeScale = 1f;
                        gameObject.SetActive(false);
                    }
                    else
                    {
                        Time.timeScale = 0f;
                        gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}
