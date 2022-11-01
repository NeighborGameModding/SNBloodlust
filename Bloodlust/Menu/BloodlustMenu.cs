using Bloodlust.Info;
using Bloodlust.Menu.Utils;
using MelonLoader;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bloodlust.Menu;

public static class BloodlustMenu
{
    private static bool _initialized;
    private static readonly List<Category> _categories = new();
    private static readonly KeyCode _toggleMenuKey = KeyCode.Insert;

    private static Rect _menuRect = new(10, 10, 400, 500);
    private static Vector2 _scrollPosition;
    private static Rect _menuContentMargin = new(4, 20, 4, 4);
    private static bool _enabled;

    public static void Initialize()
    {
        if (_initialized)
            return;

        MelonEvents.OnGUI.Subscribe(Render, 1000);
        MelonEvents.OnUpdate.Subscribe(Update);

        _initialized = true;
    }

    private static void Update()
    {
        foreach (var category in _categories)
        {
            category.Update();
        }
    }

    private static void Render()
    {
        if (GUIInput.KeyDown(_toggleMenuKey))
        {
            _enabled = !_enabled;
            CursorController.Enabled = _enabled;
        }

        if (!_enabled)
            return;

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
        _menuRect = GUI.Window(666, _menuRect, new Action<int>(RenderMenu), "<b>Bloodlust</b>  <color=grey>v" + ModInfo.Version + "</color>");
    }

    private static void RenderMenu(int id)
    {
        GUILayout.Space(_menuContentMargin.y);
        GUILayout.BeginHorizontal(GUIExt.NoOptions);
        GUILayout.Space(_menuContentMargin.x);
        GUI.backgroundColor = Color.black;
        var boxRect = new Rect(_menuContentMargin.x, _menuContentMargin.y, _menuRect.width - _menuContentMargin.x - _menuContentMargin.width, _menuRect.height - _menuContentMargin.y - _menuContentMargin.height);
        GUI.Box(boxRect, string.Empty);
        GUI.Box(boxRect, string.Empty);
        _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, GUIExt.NoOptions);
        {
            foreach (var c in _categories)
            {
                GUI.color = new(0.8f, 0.8f, 0.8f);
                GUI.backgroundColor = Color.gray;
                c.Render();
            }
        }
        GUILayout.EndScrollView();
        GUILayout.Space(_menuContentMargin.width);
        GUILayout.EndHorizontal();
        GUILayout.Space(_menuContentMargin.height);

        GUI.DragWindow(new(0, 0, _menuRect.width, 16));
    }

    public class Category
    {
        private bool _enabled;
        private readonly List<MenuElement> _elements;

        public string Name { get; private set; }
        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (value == _enabled)
                    return;

                _enabled = value;
                foreach (var e in _elements)
                    e.Enabled = value;
            }
        }

        private Category(string name, List<MenuElement> elements, bool enabled = false)
        {
            Name = name;
            _elements = elements;
            Enabled = enabled;
        }

        public static Category Create(string name, List<MenuElement> elements, bool enabled = false)
        {
            var c = new Category(name, elements, enabled);
            _categories.Add(c);
            return c;
        }

        public void Update()
        {
            if (!Enabled)
                return;

            foreach (var element in _elements)
            {
                element.Update();
            }
        }

        public void Render()
        {
            if (!Enabled)
                return;

            GUILayout.Label($"<b><size=20>{Name}</size></b>", GUIExt.NoOptions);
            foreach (var element in _elements)
            {
                element.Render();
            }
            GUILayout.Space(20);
        }
    }
}
