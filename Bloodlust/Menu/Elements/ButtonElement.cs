using Bloodlust.Menu.Utils;
using System;
using UnityEngine;

namespace Bloodlust.Menu.Elements;

public class ButtonElement : MenuElement
{
    private readonly Action _onClick;

    public ButtonElement(string name, Action onClick) : base(name)
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

        if (GUILayout.Button(Name, new(new GUILayoutOption[] { GUILayout.MinWidth(150) })))
            Click();

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
}
