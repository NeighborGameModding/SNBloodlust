using Bloodlust.Deobfuscation;
using Bloodlust.Menu;
using Bloodlust.Menu.Elements;
using GameModes.Shared.Models.Customization;
using System;

namespace Bloodlust.Features.MenuCategories;

public static class Identity
{
    private static BloodlustMenu.Category _category;

    private static TextBoxElement _nameToSetTextBox;

    private static Tuple<CustomizationItemInfo, bool>[] _allItems;
    private static Tuple<CustomizationEmotionsInfo, ActorType, bool>[] _allEmotions;

    public static void Initialize()
    {
        _nameToSetTextBox = new("Name To Set");
        var changeNameButton = new ButtonElement("Change Name", ChangeName);
        var unlockAllItems = new ToggleElement("Unlock All Items", OnToggleAllItems);
        var crossEmotes = new ToggleElement("Cross-Emotes", OnToggleCrossEmotes);

        _category = BloodlustMenu.Category.Create("Identity", new()
        {
            _nameToSetTextBox,
            changeNameButton,
            unlockAllItems,
            crossEmotes
        });

        GameEvents.OnGameModeChanged.Subscribe(OnGameModeChanged);
    }

    private static void ChangeName()
    {
        var info = GameContextUtils.GetLocalPlayerInfo();
        info.SetName(_nameToSetTextBox.Text);
        MenuScenaryUtils.GetInstance().lobbyPlayerCharacter.RefreshName();

        //var player = BloodyPlayerController.GetLocalLobbyPlayer();
        //if (player == null)
        //    return;

        //player.GetNetObject().SendMessage(Messages.CreateLobbyPlayerSyncInfoMessage(info, player.field_Public_ObjectPublicISerializableObLoObAcLoUnique_0, player.field_Public_ObjectPublicISerializableObLoObAcLoUnique_1));
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
            if (value)
                emotion.Item1.hasIcon = true;

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
