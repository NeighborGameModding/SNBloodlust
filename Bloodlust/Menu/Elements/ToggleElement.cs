using Bloodlust.Menu.Utils;
using MelonLoader;
using UnityEngine;

namespace Bloodlust.Menu.Elements;

public class ToggleElement : HotkeyElement
{
    private bool _shouldBeOn;
    private bool _on;

    public readonly MelonEvent<bool> OnToggle = new();

    public bool On
    {
        get => _on;
        set
        {
            if (value == _shouldBeOn)
                return;

            _shouldBeOn = value;
            if (Enabled)
            {
                ForceToggle(value);
            }
        }
    }

    public ToggleElement(string name, LemonAction<bool> onToggle = null, KeyCode defaultKey = KeyCode.None, bool defaultValue = false) : base(name, defaultKey)
    {
        On = defaultValue;
        if (onToggle != null)
            OnToggle.Subscribe(onToggle);
    }

    private void ForceToggle(bool value)
    {
        if (value == _on)
            return;

        _on = value;
        OnToggle.Invoke(value);
    }

    public override void Render()
    {
        GUILayout.BeginHorizontal(GUIExt.NoOptions);
        GUILayout.Label(Name, GUIExt.NoOptions);
        GUILayout.FlexibleSpace();
        On = GUILayout.Toggle(On, string.Empty, GUIExt.NoOptions);
        RenderHotkey();
        GUILayout.EndHorizontal();
    }

    protected override void OnKeyPressed()
    {
        On = !On;
    }

    protected override void OnElementToggled(bool value)
    {
        ForceToggle(value && _shouldBeOn);
    }
}
