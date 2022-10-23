using HarmonyLib;
using UnityEngine;

namespace Bloodlust.Menu.Utils;

[HarmonyPatch]
public static class CursorController
{
    private static bool _cursorModifier;

    private static bool _cursorVisible;
    private static CursorLockMode _cursorLock;

    private static bool _isUpdating;

    public static bool Enabled
    {
        get => _cursorModifier || _cursorVisible && _cursorLock == CursorLockMode.None;
        set
        {
            if (_cursorModifier == value)
                return;

            _cursorModifier = value;
            _isUpdating = true;
            Cursor.visible = value || _cursorVisible;
            Cursor.lockState = value ? CursorLockMode.None : _cursorLock;
            _isUpdating = false;
        }
    }

    [HarmonyPatch(typeof(Cursor), nameof(Cursor.visible), MethodType.Setter)]
    [HarmonyPrefix]
    private static bool CursorVisiblePatch([HarmonyArgument(0)] bool visible)
    {
        if (_isUpdating)
            return true;

        _cursorVisible = visible;
        return !_cursorModifier;
    }

    [HarmonyPatch(typeof(Cursor), nameof(Cursor.lockState), MethodType.Setter)]
    [HarmonyPrefix]
    private static bool CursorLockPatch([HarmonyArgument(0)] CursorLockMode lockMode)
    {
        if (_isUpdating)
            return true;

        _cursorLock = lockMode;
        return !_cursorModifier;
    }
}
