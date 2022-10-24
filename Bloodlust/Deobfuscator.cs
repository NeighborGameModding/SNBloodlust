global using static Bloodlust.StaticDeobfuscator;

global using KeyType = EnumPublicSealedvaBLREYECAQUCARO12CAUnique;
global using EndingType = EnumPublicSealedvaALBATIQU5vUnique;
global using MessageTarget = EnumPublicSealedvaOtAlSe5vSeUnique;
global using LobbyPlayerState = EnumPublicSealedvaNORELOLESEGASCMA9vUnique;
global using PlayerController = ObjectPublicObLi1PlInPlInObLi1Unique;

global using HoloNetMessenger = ObjectPublicDoBoObBoUnique;
global using HoloNetGlobalMessage = Object1Public6Vo66666666Unique;
global using HoloNetObjectMessage = Object1Public316Vo6666666Unique;
global using EndMatchMessage = Object2Public47ObVo47Ob47Ob47Ob470;
global using StartMatchMessage = Object2PublicObVoObObObObObObObOb3;
global using SonarBeamMessage = Object2Public92SiObVo92SiOb92SiOb0;
global using SonarIdleMessage = Object2Public92SiObVo92SiOb92SiOb1; // I'm not exactly sure what this message is for, but it's used between the sonar beams
global using KeyPickedUpMessage = Object2PublicKeObKeUnique;
global using KeycardDoorUnlockedMessage = Object2PublicIn31Ob31InObVo31InObUnique;
global using LockUnlockedMessage = Object2Public3150PlObVo50PlObPl50Unique;
global using UpdateMatchSettingsMessage = Object2PublicMaUnique;
global using LobbyPlayerInfoSyncMessage = Object2PublicObObObUnique;
global using LobbyPlayerStateSyncMessage = Object2Public60ObVo60Ob60Ob60Ob60Unique;
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
global using DoorOpenMessage = Object2PublicSiObPlUnique;
global using MoveableObjectCloseMessage = Object3PublicVo113;
global using MoveableObjectOpenMessage = Object3PublicVo112;
global using LightSwitchToggleMessage = Object3PublicVo118;
global using ThrowableItemApplyForceMessage = Object2PublicVeDoVeUnique;
global using LampToggleMessage = Object3PublicVo76;
global using GlueBottleBreakMessage = Object2PublicVeSiVeUnique;
global using WindowBreakMessage = Object2PublicVeVoVeObObVeObVeObVeUnique;
global using ConsumeConsumableItemMessage = Object3PublicVo90;
global using PlayerApplyBuffMessage = Object2PublicSeUnique;
global using PlayerDeactiveBuffMessage = Object2PublicInObUnique;
global using AnimateSecretDoorMessage = Object2PublicVeObPlUnique;
global using PlayerPickUpResourceMessage = Object2Public31InObVoInObInObInOb0;

using HoloNetwork.NetworkObjects;

namespace Bloodlust;

internal static class Deobfuscator
{
    public static void SendMessage(this HoloNetObject obj, HoloNetObjectMessage message, MessageTarget target = MessageTarget.All)
    {
        obj.Method_Public_Void_Object1Public316Vo6666666Unique_EnumPublicSealedvaOtAlSe5vSeUnique_0(message, target);
    }
}

internal static class StaticDeobfuscator
{
    public static class BloodyHoloNetMessenger
    {
        public static void SendMessage(HoloNetGlobalMessage message, MessageTarget target = MessageTarget.All)
        {
            HoloNetMessenger.Method_Public_Static_Void_Object1Public6Vo66666666Unique_EnumPublicSealedvaOtAlSe5vSeUnique_0(message, target);
        }
    }
}