using Bloodlust.Menu;
using Bloodlust.Menu.Elements;
using GameModes.Shared.Models.Customization;
using System;

namespace Bloodlust.Features.MenuCategories;

public static class LoadoutModifiers
{
    private static BloodlustMenu.Category _category;
    private static ToggleElement _unlockAllItems;
    private static ToggleElement _crossEmotes;

    private static Tuple<CustomizationItemInfo, bool>[] _allItems;
    private static Tuple<CustomizationEmotionsInfo, ActorType, bool>[] _allEmotions;

    public static void Initialize()
    {
        _unlockAllItems = new ToggleElement("Unlock All Items", OnToggleAllItems);
        _crossEmotes = new ToggleElement("Cross-Emotes", OnToggleCrossEmotes);

        _category = BloodlustMenu.Category.Create("Loadout Modifiers", new()
        {
            _unlockAllItems,
            _crossEmotes
        });

        GameEvents.OnGameModeChanged.Subscribe(OnGameModeChanged);
    }

    private static void OnToggleCrossEmotes(bool value)
    {
        InitializeItemLists();

        foreach (var emotion in _allEmotions)
        {
            emotion.Item1.allowedActorType = value ? ActorType.ANY : emotion.Item2;
        }
    }

    private static void OnToggleAllItems(bool value)
    {
        InitializeItemLists();

        foreach (var item in _allItems)
        {
            item.Item1.isDefaultValue = value || item.Item2;
        }

        foreach (var emotion in _allEmotions)
        {
            emotion.Item1.isDefaultValue = value || emotion.Item3;
        }
    }

    private static void InitializeItemLists()
    {
        if (_allItems == null)
        {
            var items = UnityEngine.Resources.FindObjectsOfTypeAll<CustomizationItemInfo>();
            _allItems = new Tuple<CustomizationItemInfo, bool>[items.Length];
            for (var idx = 0; idx < _allItems.Length; idx++)
            {
                var item = items[idx];
                _allItems[idx] = new(item, item.isDefaultValue);
            }
        }

        if (_allEmotions == null)
        {
            var emotions = UnityEngine.Resources.FindObjectsOfTypeAll<CustomizationEmotionsInfo>();
            _allEmotions = new Tuple<CustomizationEmotionsInfo, ActorType, bool>[emotions.Length];
            for (var idx = 0; idx < _allEmotions.Length; idx++)
            {
                var emotion = emotions[idx];
                _allEmotions[idx] = new(emotion, emotion.allowedActorType, emotion.isDefaultValue);
            }
        }
    }

    private static void OnGameModeChanged(GameMode gameMode)
    {
        _category.Enabled = gameMode is GameMode.MENU or GameMode.LOBBY;
    }
}
