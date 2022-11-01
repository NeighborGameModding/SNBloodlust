using Bloodlust.Menu.Utils;
using System;
using UnhollowerBaseLib;
using UnityEngine;

namespace Bloodlust.Menu.Elements;

public class ButtonElement : HotkeyElement
{
    private static Il2CppReferenceArray<GUILayoutOption> _buttonOptions = new(new GUILayoutOption[] { GUILayout.MinWidth(150) });

    private readonly Action _onClick;

    public ButtonElement(string name, Action onClick, KeyCode defaultKey = KeyCode.None) : base(name, defaultKey)
    {
        _onClick = onClick;
    }

    public void Click()
    {
        _onClick?.Invoke();
    }

    public override void Render()
    {
        GUILayout.BeginHorizontal(GUIExt.NoOptions);

        if (GUILayout.Button(Name, _buttonOptions))
            Click();

        GUILayout.FlexibleSpace();
        RenderHotkey();
        GUILayout.EndHorizontal();
    }

    protected override void OnKeyPressed()
    {
        Click();
    }
}
