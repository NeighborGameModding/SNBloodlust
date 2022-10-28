using Bloodlust.Menu.Utils;
using Bloodlust.Utils;
using MelonLoader;
using System;
using UnityEngine;

namespace Bloodlust.Menu;

public static class BloodlustMenu
{
    private static bool _initialized;
    private static Rect _menuRect = new(10, 10, 400, 500);
    private static Vector2 _scrollPosition;
    private static Rect _menuContentMargin = new(4, 20, 4, 4);

    public static void Initialize()
    {
        if (_initialized)
            return;

        MelonEvents.OnGUI.Subscribe(Render, -1000);

        _initialized = true;
    }

    private static void Render()
    {
        var screenWidth = Screen.width;
        var screenHeight = Screen.height;
        if (_menuRect.x < 0)
            _menuRect.x = 0;
        if (_menuRect.y < 0)
            _menuRect.y = 0;
        if (_menuRect.right > screenWidth)
            _menuRect.x = screenWidth - _menuRect.width;
        if (_menuRect.bottom > screenHeight)
            _menuRect.y = screenHeight - _menuRect.height;

        GUI.backgroundColor = Color.red;
        _menuRect = GUI.Window(666, _menuRect, new Action<int>(RenderMenu), "Bloodlust");
    }

    private static void RenderMenu(int id)
    {
        GUILayout.Space(_menuContentMargin.y);
        GUILayout.BeginHorizontal(GUIExt.NoOptions);
        GUILayout.Space(_menuContentMargin.x);
        GUI.backgroundColor = Color.black;
        GUI.Box(new(_menuContentMargin.x, _menuContentMargin.y, _menuRect.width - _menuContentMargin.x - _menuContentMargin.width, _menuRect.height - _menuContentMargin.y - _menuContentMargin.height), string.Empty);
        _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, GUIExt.NoOptions);
        {

        }
        GUILayout.EndScrollView();
        GUILayout.Space(_menuContentMargin.width);
        GUILayout.EndHorizontal();
        GUILayout.Space(_menuContentMargin.height);

        GUI.DragWindow(new(0, 0, _menuRect.width, 16));
    }
}
