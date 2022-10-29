using Bloodlust.Menu.Utils;
using MelonLoader;
using UnityEngine;

namespace Bloodlust.Menu.Elements;

public class ToggleElement : MenuElement
{
    private bool _on;

    public readonly MelonEvent<bool> OnToggle = new();

    public bool On
    {
        get => _on;
        set
        {
            if (value == _on)
                return;

            _on = value;
            OnToggle.Invoke(value);
        }
    }

    public ToggleElement(string name, LemonAction<bool> onToggle = null, bool defaultValue = false) : base(name)
    {
        _on = defaultValue;
        if (onToggle != null)
            OnToggle.Subscribe(onToggle);
    }

    public override void Render()
    {
        GUILayout.BeginHorizontal(GUIExt.NoOptions);
        GUILayout.Label(Name, GUIExt.NoOptions);
        GUILayout.FlexibleSpace();
        On = GUILayout.Toggle(On, string.Empty, GUIExt.NoOptions);
        GUILayout.EndHorizontal();
    }
}
