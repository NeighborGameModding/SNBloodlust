using Bloodlust.Menu.Utils;
using UnityEngine;

namespace Bloodlust.Menu.Elements;

public abstract class HotkeyElement : MenuElement
{
    private bool _selectingKey;

    public KeyCode Key { get; set; }

    public HotkeyElement(string name, KeyCode defaultKey) : base(name)
    {
        Key = defaultKey;
    }

    public void RenderHotkey()
    {
        if (!_selectingKey && Key != KeyCode.None && GUIInput.KeyDown(Key))
            OnKeyPressed();

        if (_selectingKey)
        {
            if (Event.current.isMouse)
            {
                _selectingKey = false;
            }
            else if (Event.current.type == EventType.KeyDown)
            {
                var input = Event.current.keyCode;
                if ((input >= KeyCode.LeftBracket && input <= KeyCode.Tilde) || (input >= KeyCode.Keypad0 && input <= KeyCode.LeftArrow))
                    Key = input;

                _selectingKey = false;
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

    protected abstract void OnKeyPressed();
}
