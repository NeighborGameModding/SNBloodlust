using Bloodlust.Menu.Utils;
using MelonLoader;
using UnityEngine;

namespace Bloodlust.Menu.Elements;

public abstract class HotkeyElement : MenuElement
{
    private bool _selectingKey;
    private static bool _pauseInput;

    public KeyCode Key { get; set; }

    public HotkeyElement(string name, KeyCode defaultKey) : base(name)
    {
        Key = defaultKey;
    }

    public void RenderHotkey()
    {
        if (_selectingKey)
        {
            var keyDown = Event.current.type == EventType.KeyDown;
            if (keyDown || Event.current.isMouse)
            {
                _selectingKey = false;
                PauseInput();
            }

            if (keyDown)
            {
                var input = Event.current.keyCode;
                if (input is >= KeyCode.LeftBracket and <= KeyCode.Tilde or >= KeyCode.Keypad0 and <= KeyCode.LeftArrow)
                    Key = input;
            }
        }

        var color = GUI.backgroundColor;
        GUI.backgroundColor = _selectingKey ? Color.red : Color.white;
        if (GUILayout.Button(_selectingKey ? "Press a key..." : Key.ToString(), GUIExt.NoOptions))
        {
            Key = KeyCode.None;
            _selectingKey = true;
        }
        GUI.backgroundColor = color;
    }

    private static void PauseInput()
    {
        _pauseInput = true;
        MelonEvents.OnUpdate.Subscribe(UnpauseInput, 100, true);
    }

    private static void UnpauseInput()
    {
        _pauseInput = false;
    }

    public override void Update()
    {
        if (_pauseInput)
            return;
        
        if (Key != KeyCode.None && Input.GetKeyDown(Key))
            OnKeyPressed();
    }

    protected abstract void OnKeyPressed();
}
