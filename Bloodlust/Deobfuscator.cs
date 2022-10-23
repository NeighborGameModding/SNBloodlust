global using static Bloodlust.StaticDeobfuscator;

global using KeyType = EnumPublicSealedvaBLREYECAQUCARO12CAUnique;
global using EndingType = EnumPublicSealedvaALBATIQU5vUnique;
global using MessageTarget = EnumPublicSealedvaOtAlSe5vSeUnique;
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