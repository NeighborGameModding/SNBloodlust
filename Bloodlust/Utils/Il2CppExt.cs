using UnhollowerBaseLib;

namespace Bloodlust.Utils;

internal static class Il2CppExt
{
    public static Il2CppReferenceArray<T> CreateEmptyArray<T>() where T : Il2CppSystem.Object
    {
        return new Il2CppReferenceArray<T>(0);
    }
}
