using Bloodlust.Deobfuscation;
using Bloodlust.Menu;
using Bloodlust.Menu.Elements;
using GameModes.GameplayMode.Cameras;
using GameModes.GameplayMode.Interactables.InventoryItems.Base;
using GameModes.GameplayMode.Levels.Basement.Objectives;
using HarmonyLib;
using MelonLoader;
using System;
using System.Collections.Generic;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace Bloodlust.Features.MenuCategories;

public static class VisualAdvantages
{
    private static BloodlustMenu.Category _category;
    private static ToggleElement _itemESPToggle;
    private static ToggleElement _playerESPToggle;
    private static ToggleElement _revealClasses;
    private static GUIStyle _ESPBoxStyle;

    private static readonly List<CachedItem> _items = new();

    public static void Initialize()
    {
        _itemESPToggle = new ToggleElement("Item ESP", ToggleItemESP, KeyCode.Keypad2);
        _playerESPToggle = new ToggleElement("Player ESP", TogglePlayerESP, KeyCode.Keypad1);
        _revealClasses = new ToggleElement("Reveal Classes", defaultValue: true);

        _category = BloodlustMenu.Category.Create("Visual Advantages", new()
        {
            _itemESPToggle,
            _playerESPToggle,
            _revealClasses
        });

        GameEvents.OnGameModeChanged.Subscribe(OnGameModeChanged);

        Instance.HarmonyInstance.Patch(typeof(InventoryItem).GetMethod(nameof(InventoryItem.LocalInit)), postfix: new(new Action<InventoryItem>(OnItemSpawnPatch).Method));
    }

    private static Il2CppSystem.Type _inventoryItemType = Il2CppType.Of<InventoryItem>();

    private static void OnItemSpawnPatch(InventoryItem __instance)
    {
        if (!_inventoryItemType.IsAssignableFrom(__instance.GetIl2CppType())) // Patch verification for when Harmony fucks up
            return;

        _items.Add(new(__instance, new(FormatItemGUILabel(__instance)), __instance.TryCast<KeyInventoryItem>() != null));
    }

    private static void OnGameModeChanged(GameMode gameMode)
    {
        _category.Enabled = gameMode == GameMode.GAMEPLAY;
    }

    private static void TogglePlayerESP(bool value)
    {
        if (value)
            MelonEvents.OnGUI.Subscribe(DrawPlayerESP);
        else
            MelonEvents.OnGUI.Unsubscribe(DrawPlayerESP);
    }

    private static void ToggleItemESP(bool value)
    {
        if (value)
            MelonEvents.OnGUI.Subscribe(DrawItemESP);
        else
            MelonEvents.OnGUI.Unsubscribe(DrawItemESP);
    }

    private static void DrawESPBox(Vector2 position, float width, Color color, GUIContent content, int lines)
    {
        var height = lines * 17 + 8;
        var rect = new Rect(position.x - width / 2, position.y - height / 2, width, height);

        if (rect.bottom < 0f || rect.top > Screen.height || rect.right < 0f || rect.left > Screen.width)
            return;

        if (_ESPBoxStyle == null)
        {
            _ESPBoxStyle = new GUIStyle(GUI.skin.box);
            _ESPBoxStyle.normal.background = Texture2D.whiteTexture;
        }

        var bg = GUI.backgroundColor;
        GUI.backgroundColor = color;
        GUI.Box(rect, content, _ESPBoxStyle);
        GUI.backgroundColor = bg;
    }

    // Don't allocate too many objects, otherwise the garbage collector will be going nuts
    private static void DrawItemESP()
    {
        if (Event.current.type != EventType.Repaint)
            return;

        var cam = GameCamera.instance?.prop_Camera_0;
        if (cam == null)
            return;

        for (var idx = 0; idx < _items.Count; idx++)
        {
            var item = _items[idx];
            if (item.Item == null)
            {
                _items.RemoveAt(idx);
                idx--;
                continue;
            }

            if (!item.GameObject.activeInHierarchy || (!item.IsKey && item.Item.rarity != ItemRarity.EPIC && item.Item.rarity != ItemRarity.LEGENDARY))
                continue;

            var pos = cam.WorldToScreenPoint(item.Transform.position);
            if (pos.z < 0)
                continue;

            pos.y = Screen.height - pos.y;

            DrawESPBox(pos, 130, new(1f, 0f, 1f, 0.3f), item.GUILabel, 1);
        }
    }

    private static void DrawPlayerESP()
    {
        if (Event.current.type != EventType.Repaint)
            return;

        var cam = GameCamera.instance?.prop_Camera_0;
        if (cam == null)
            return;

        var players = BloodyPlayerController.GetAllPlayers();
        if (players == null)
            return;

        foreach (var player in players)
        {
            if (player.IsDead())
                continue;

            var actor = player.GetCurrentActor();
            var clas = actor.GetActorClassInfo();
            if (clas == null)
                continue;

            var pos = cam.WorldToScreenPoint(actor.transform.position);
            if (pos.z < 0)
                continue;

            pos.y = Screen.height - pos.y;

            var distance = Vector3.Distance(cam.transform.position, actor.transform.position);

            var name = player.GetPlayerInfo().GetName();
            if (name.Length > 24)
                name = name.Substring(0, 24) + "...";

            var nameStr = $"<b>{name}</b>";
            var distanceStr = $"{distance:0.0}m";
            var text = _revealClasses.On ? $"{nameStr}\n<color={(clas.isNeighborClass ? "red" : "aqua")}>{clas.Id}</color>\n{distanceStr}" : $"{nameStr}\n{distanceStr}";

            DrawESPBox(pos, 200, _revealClasses.On && player.IsNeighbor() ? new(1f, 0.2f, 0.2f, 0.3f) : new(0.2f, 0.2f, 1f, 0.3f), new(text), _revealClasses.On ? 3 : 2);
        }
    }

    private static string FormatItemGUILabel(InventoryItem item)
    {
        string result = null;

        if (item.TryCast<KeyInventoryItem>() is var key && key != null)
        {
            result = key.keyType switch
            {
                KeyType.BLUE => "<color=blue>Blue Key</color>",
                KeyType.RED => "<color=red>Red Key</color>",
                KeyType.YELLOW => "<color=yellow>Yellow Key</color>",
                KeyType.CARD_1 => "Key Card Level 1",
                KeyType.CARD_2 => "Key Card Level 2",
                KeyType.CARD_3 => "Key Card Level 3",
                KeyType.CARD_4 => "Key Card Level 4",
                KeyType.QUEST => "Key Card Level ?",
                KeyType.CAR => "Car Key",
                KeyType.ROCKET => "Rocket Key",
                KeyType.ROCKET_FLY_PANNEL => "Rocket Fly Panel Key",
                _ => null
            };
        }

        if (result == null)
        {
            result = item.name;
            var length = result.Length;

            var containsStart = result.StartsWith("Item_");
            if (containsStart)
                length -= 5;
            var containsEnd = result.EndsWith("(Clone)");
            if (containsEnd)
                length -= 7;

            if (containsStart || containsEnd)
                result = result.Substring(containsStart ? 5 : 0, length);

            result = result.Replace('_', ' ');
        }

        return $"<b>{result}</b>";
    }

    private struct CachedItem
    {
        public InventoryItem Item;
        public GameObject GameObject;
        public Transform Transform;
        public GUIContent GUILabel;
        public bool IsKey;

        public CachedItem(InventoryItem item, GUIContent label, bool isKey)
        {
            Item = item;
            GameObject = item.gameObject;
            Transform = item.transform;
            GUILabel = label;
            IsKey = isKey;
        }
    }
}
