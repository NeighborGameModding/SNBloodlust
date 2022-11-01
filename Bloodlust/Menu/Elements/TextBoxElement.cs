using Bloodlust.Menu.Utils;
using UnhollowerBaseLib;
using UnityEngine;

namespace Bloodlust.Menu.Elements;

public class TextBoxElement : MenuElement
{
    private static Il2CppReferenceArray<GUILayoutOption> _textFieldOptions = new(new GUILayoutOption[] { GUILayout.MinWidth(150) });

    public string Text { get; private set; }

    public TextBoxElement(string name, string initialText = "") : base(name)
    {
        Text = initialText;
    }

    public override void Render()
    {
        GUILayout.BeginHorizontal(GUIExt.NoOptions);
        GUILayout.Label(Name, GUIExt.NoOptions);
        GUILayout.FlexibleSpace();
        Text = GUILayout.TextField(Text, _textFieldOptions);
        GUILayout.EndHorizontal();
    }
}
