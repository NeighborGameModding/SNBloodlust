global using static Bloodlust.Deobfuscation.StaticDeobfuscator;

global using KeyType = EnumPublicSealedvaBLREYECAQUCARO12CAUnique;
global using EndingType = EnumPublicSealedvaALBATIQU5vUnique;
//global using LobbyPlayerState = EnumPublicSealedvaNORELOLESEGASCMA9vUnique;
global using BackendType = EnumPublicSealedvaOFPL3vUnique;
global using OriginalMessageTarget = EnumPublicSealedva5v1;
global using ActorType = EnumPublicSealedvaEXNECUSPITAN8vNEUnique;
global using GameMode = EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique;

global using PlayerController = ObjectPublicObLi1PlInPlInObLi1Unique;
global using NetworkAdapterController = ObjectPublicOb8459Ob617057205817Unique;
global using PlayfabBackendAdapter = ObjectPublicStObStDi2SiObDi2StUnique;
global using UpdateLoadoutRequestResult = Object1PublicVo7;
global using Loadout = ObjectPublicAcLiAc1ObObObUnique;
global using GetLoadoutRequestResult = Object1Public99VoUnique;
global using LoadoutSerializer = h5ay7rju46La5doyt5jtndrdfjf;
global using GameModeController = ObjectPublicDi2ObLiOb1AcCoAcBoUnique;
global using Lobby = ObjectPublicStObInObBoBoInObBoObUnique;
global using PlayerResourceManager = Object1PublicAbstractAcObSkObBoLiOb1NuObUnique;
global using SlingshotManager = Object2PublicObSiInSlBoScInBoSlUnique;

global using HoloNetMessenger = ObjectPublicDoBoObBoUnique;
global using HoloNetGlobalMessage = Object1PublicObVoObObObObObObObObUnique;
global using HoloNetObjectMessage = Object1PublicObBoObUnique;
global using EndMatchMessage = Object2Public47ObVo47Ob47Ob47Ob470;
global using StartMatchMessage = Object2PublicObVoObObObObObObObOb3;
global using SonarBeamMessage = Object2Public92SiObVo92SiOb92SiOb0;
global using SonarIdleMessage = Object2Public92SiObVo92SiOb92SiOb1; // I'm not exactly sure what this message is for, but it's used between the sonar beams
global using KeyPickedUpMessage = Object2PublicKeObKeUnique;
global using KeycardDoorUnlockedMessage = Object2PublicIn31Ob31InObVo31InObUnique;
//global using LockUnlockedMessage = Object2Public3150PlObVo50PlObPl50Unique;
global using UpdateMatchSettingsMessage = Object2PublicMaUnique;
global using LobbyPlayerInfoSyncMessage = Object2PublicObObObUnique;
//global using LobbyPlayerStateSyncMessage = Object2Public60ObVo60Ob60Ob60Ob60Unique;
global using SpawnActorMessage = Object2PublicStObObUnique; // ???
global using PlayerJumpMessage = Object3PublicVo145;
global using PlayerLandMessage = Object2PublicSiObVoSiObSiObSiObSi1;
global using DropInventoryItemMessage = Object2Public23ObVo23Ob23Ob23Ob23Unique;
global using SwitchInventorySlotMessage = Object2PublicInObVoInObInObInObIn1;
global using TakeInventoryItemMessage = Object2PublicObInInUnique;
global using InventoryItemThrowAnimationMessage = Object3PublicVo94;
global using InventoryItemThrowAnimationCancellationMessage = Object3PublicVo96;
global using FlashlightOnMessage = Object3PublicVo87;
global using FlashlightOffMessage = Object3PublicVo88;
//global using DoorOpenMessage = Object2PublicSiObPlUnique;
global using MoveableObjectCloseMessage = Object3PublicVo113;
global using MoveableObjectOpenMessage = Object3PublicVo112;
global using LightSwitchToggleMessage = Object3PublicVo118;
global using ThrowableItemApplyForceMessage = Object2PublicVeDoVeUnique;
global using LampToggleMessage = Object3PublicVo76;
global using GlueBottleBreakMessage = Object2PublicVeSiVeUnique;
//global using WindowBreakMessage = Object2PublicVeVoVeObObVeObVeObVeUnique;
global using ConsumeConsumableItemMessage = Object3PublicVo90;
global using PlayerApplyBuffMessage = Object2PublicSeUnique;
global using PlayerDeactiveBuffMessage = Object2PublicInObUnique;
global using AnimateSecretDoorMessage = Object2PublicVeObPlUnique;
//global using PlayerPickUpResourceMessage = Object2Public31InObVoInObInObInOb0;
global using RifleShootMessage = Object2PublicVeObVeVoObVeObVeObVe1;

using HoloNetwork.NetworkObjects;
using Bloodlust.Deobfuscation.Enums;
using Ui.Screens.Preload;
using Ui.Screens.CustomGame;
using GameModes.LobbyMode;
using GameModes.GameplayMode.Players;
using GameModes.GameplayMode.Actors;
using GameModes.GameplayMode.Interactables.InventoryItems;

namespace Bloodlust.Deobfuscation;

internal static class DeobfuscatorExtensions
{
    public static void SendMessage(this HoloNetObject obj, HoloNetObjectMessage message, MessageTarget target = MessageTarget.All)
    {
        obj.Method_Public_Void_Object1PublicObBoObUnique_EnumPublicSealedva5v1_0(message, (OriginalMessageTarget)target);
    }

    public static void SetResultReceived<T>(this T requestResult, Il2CppSystem.Action<T> callback) where T : ObjectPublicBoObStAc1TCBoUnique<T>
    {
        requestResult.field_Public_Action_1_TC_0 = callback;
        requestResult.field_Public_Boolean_0 = true;
        callback?.Invoke(requestResult);
    }

    public static void SetLoadoutResult(this GetLoadoutRequestResult requestResult, Loadout loadout, Il2CppSystem.Action<GetLoadoutRequestResult> callback)
    {
        requestResult.field_Public_ObjectPublicAcLiAc1ObObObUnique_0 = loadout;
        requestResult.SetResultReceived(callback);
    }

    public static Loadout Unbox(this LoadoutSerializer serializer)
    {
        return serializer.Method_Public_ObjectPublicAcLiAc1ObObObUnique_PDM_0();
    }

    public static LoadoutSerializer Box(this Loadout loadout)
    {
        return LoadoutSerializer.Method_Public_Static_h5ay7rju46La5doyt5jtndrdfjf_ObjectPublicAcLiAc1ObObObUnique_0(loadout);
    }

    public static Lobby GetSelectedLobby(this CustomGameScreen cgs)
    {
        return cgs.field_Private_ObjectPublicStObInObBoBoInObBoObUnique_0;
    }

    public static Il2CppSystem.Collections.Generic.List<Actor> GetActors(this Player player)
    {
        return player.prop_List_1_Actor_0;
    }

    public static Actor GetCurrentActor(this Player player)
    {
        return player.prop_Actor_0;
    }

    public static void Reload(this RifleInventoryItem rifle)
    {
        rifle.field_Protected_ObscuredBool_0 = true;
    }

    public static void SetResourceAmount(this PlayerResourceManager prm, int amount)
    {
        prm.prop_ObscuredInt_1 = amount;
    }
}

internal static class StaticDeobfuscator
{
    public static class BloodyHoloNetMessenger
    {
        public static void SendMessage(HoloNetGlobalMessage message, MessageTarget target = MessageTarget.All)
        {
            HoloNetMessenger.Method_Public_Static_Void_Object1PublicObVoObObObObObObObObUnique_EnumPublicSealedva5v1_0(message, (OriginalMessageTarget)target);
        }
    }

    public static class BloodyPlayfabBackendAdapter
    {
        public const string InitializeMethod = nameof(PlayfabBackendAdapter.Method_Public_Virtual_Final_New_Void_2);
        public const string MelRepRequestMethod = nameof(PlayfabBackendAdapter.Method_Public_Virtual_Final_New_Object1PublicVo6_Action_1_Object1PublicVo6_3);
        public const string UpdateLoadoutRequestMethod = nameof(PlayfabBackendAdapter.Method_Public_Virtual_Final_New_Object1PublicVo7_ObjectPublicAcLiAc1ObObObUnique_Action_1_Object1PublicVo7_0);
        public const string GetLoadoutRequestMethod = nameof(PlayfabBackendAdapter.Method_Public_Virtual_Final_New_Object1Public99VoUnique_Action_1_Object1Public99VoUnique_1);
    }

    public static class BloodyPreloadScreen
    {
        public const string OnLoadMethod = nameof(PreloadScreen.Method_Public_Virtual_Void_2);
    }

    public static class AppControllerUtils
    {
        public const string SetGameModeMethod = nameof(GameModeController.Method_Public_Void_EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique_0);

        public static void JoinLobby(Lobby lobby)
        {
            LobbyController.prop_LobbyController_0.Method_Public_Void_ObjectPublicStObInObBoBoInObBoObUnique_0(lobby);
        }
    }

    public static class BloodyCustomGameScreen
    {
        public const string OnJoinAttemptMethod = nameof(CustomGameScreen.Method_Private_Void_List_1_InterfacePublicAbstractTyBoVoTyBoObVoUnique_0);
    }

    public static class BloodyPlayerController
    {
        public static Player GetLocalPlayer()
        {
            return PlayerController.prop_Player_0;
        }

        public static Il2CppSystem.Collections.Generic.List<Player> GetAllPlayers()
        {
            return PlayerController.prop_List_1_Player_0;
        }
    }

    public static class RifleInventoryItemUtils
    {
        public const string ShootRifleMethod = nameof(RifleInventoryItem.Method_Protected_Void_Object2PublicVeObVeVoObVeObVeObVe1_PDM_2);
        public const string PickUpRifleMethod = nameof(RifleInventoryItem.Method_Public_Virtual_Final_New_Void_Action_0);
    }

    public static class SlingshotUtils
    {
        public const string InitializeMethod = nameof(SlingshotInventoryItem.Method_Public_Virtual_Void_1);
        public const string HasEnoughResourcesMethod = nameof(SlingshotManager.Method_Private_Boolean_PDM_0);
    }
}