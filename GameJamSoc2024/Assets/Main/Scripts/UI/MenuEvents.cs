using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class TDHudEvents : MonoBehaviour
{
    private UIDocument _uiDocument;
    private List<Button> _buttons = new List<Button>();
    [SerializeField] private AudioSource _audioSource;
    private Dictionary<KeyCode, Button> _keyToButton;


    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        _audioSource = GetComponent<AudioSource>();
        _buttons = _uiDocument.rootVisualElement.Query<Button>().ToList();
        foreach (var button in _buttons)
        {
            button.RegisterCallback<ClickEvent>(ev => OnButtonClicked(ev));
        }

        _keyToButton = new Dictionary<KeyCode, Button>
        {
            { KeyCode.Alpha1, _uiDocument.rootVisualElement.Q<Button>("Button1") },
            { KeyCode.Alpha2, _uiDocument.rootVisualElement.Q<Button>("Button2") },
            { KeyCode.Alpha3, _uiDocument.rootVisualElement.Q<Button>("Button3") },
            { KeyCode.Alpha4, _uiDocument.rootVisualElement.Q<Button>("Button4") },
            { KeyCode.Alpha5, _uiDocument.rootVisualElement.Q<Button>("Button5") }
        };
    }

    private void onDisable() {
        foreach (var button in _buttons)
        {
            button.UnregisterCallback<ClickEvent>(ev => OnButtonClicked(ev));
        }
    }

    private void OnButtonClicked(ClickEvent ev)
    {
        // _audioSource.Play();
        var button = (Button) ev.target;
        var buttonName = button.name;
        Debug.Log($"Button {buttonName} clicked");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
{
    foreach (var key in _keyToButton.Keys)
    {
        if (Input.GetKeyDown(key))
        {
            // _audioSource.Play();
            var button = _keyToButton[key];
            var clickEvent = new ClickEvent { target = button };
            OnButtonClicked(clickEvent);
        }
    }
}
}
