using Bloodlust.Menu.Utils;
using MelonLoader;
using UnityEngine;

namespace Bloodlust.Menu;

public static class BloodlustMenu
{
    private static bool _initialized;

    public static void Initialize()
    {
        if (_initialized)
            return;

        MelonEvents.OnGUI.Subscribe(Render, -1000);

        _initialized = true;
    }

    private static void Render()
    {
        GUI.Label(new(0, 0, 100, 40), "Bloodlust");

        if (GUIInput.KeyDown(KeyCode.B))
            CursorController.Enabled = !CursorController.Enabled;
    }
}
