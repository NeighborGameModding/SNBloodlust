using UnityEngine;

namespace Bloodlust.Menu.Utils;

public static class GUIInput
{
    public static bool KeyDown(KeyCode key)
    {
        if (Event.current.type != EventType.KeyDown)
            return false;

        return Event.current.keyCode == key;
    }

    public static bool KeyUp(KeyCode key)
    {
        if (Event.current.type != EventType.KeyUp)
            return false;

        return Event.current.keyCode == key;
    }

    public static bool Key(KeyCode key)
    {
        if (!Event.current.isKey)
            return false;

        return Event.current.keyCode == key;
    }

    public static bool MouseDown(KeyCode key)
    {
        if (Event.current.type != EventType.MouseDown)
            return false;

        return Event.current.keyCode == key;
    }
}
