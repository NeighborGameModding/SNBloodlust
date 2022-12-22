using HarmonyLib;
using MelonLoader;
using System;
using UnityEngine;

namespace Bloodlust.Menu.Utils;

public static class CursorController
{
    private static bool _cursorModifier;

    private static bool _cursorVisible;
    private static CursorLockMode _cursorLock;

    private static bool _isUpdating;
    private static bool _disabled;
    private static bool _initialized;

    public static bool Enabled
    {
        get => _cursorModifier || _cursorVisible && _cursorLock == CursorLockMode.None;
        set
        {
            if (!_initialized)
                Initialize();

            if (_disabled || _cursorModifier == value)
                return;

            _cursorModifier = value;
            _isUpdating = true;
            Cursor.visible = value || _cursorVisible;
            Cursor.lockState = value ? CursorLockMode.None : _cursorLock;
            _isUpdating = false;
        }
    }

    private static void Initialize()
    {
        if (MelonBase.FindMelon("UnityExplorer", "Sinai") != null)
        {
            Melon<BloodlustMod>.Logger.Warning("UnityExplorer present, disabling CursorController.");
            _disabled = true;
        }

        _initialized = true;

        if (_disabled)
            return;

        Instance.HarmonyInstance.Patch(typeof(Cursor).GetProperty(nameof(Cursor.visible)).GetSetMethod(), new(new Func<bool, bool>(CursorVisiblePatch).Method));
        Instance.HarmonyInstance.Patch(typeof(Cursor).GetProperty(nameof(Cursor.lockState)).GetSetMethod(), new(new Func<CursorLockMode, bool>(CursorLockPatch).Method));

        CursorVisiblePatch(Cursor.visible);
        CursorLockPatch(Cursor.lockState);
    }

    private static bool CursorVisiblePatch([HarmonyArgument(0)] bool visible)
    {
        if (_isUpdating)
            return true;

        _cursorVisible = visible;
        return !_cursorModifier;
    }

    private static bool CursorLockPatch([HarmonyArgument(0)] CursorLockMode lockMode)
    {
        if (_isUpdating)
            return true;

        _cursorLock = lockMode;
        return !_cursorModifier;
    }
}
