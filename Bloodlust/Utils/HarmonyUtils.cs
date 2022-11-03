using HarmonyLib;
using MelonLoader;
using System;

namespace Bloodlust.Utils;

public static class HarmonyUtils
{
    public static HarmonyLib.Harmony Instance => Melon<BloodlustMod>.Instance.HarmonyInstance;

    public static int PatchObfuscated(Type type, string methodName, HarmonyMethod prefix = null, HarmonyMethod postfix = null)
    {
        if (methodName == null || methodName.Length < 3)
            return 0;

        var lastChar = methodName[methodName.Length - 1];
        if (lastChar < '0' || lastChar > '9' || methodName[methodName.Length - 2] != '_')
            throw new ArgumentException("Method is not obfuscated.", nameof(methodName));

        methodName = methodName.Remove(methodName.Length - 1);

        if (methodName.EndsWith("PDM_"))
            methodName = methodName.Remove(methodName.Length - 4);

        var count = 0;
        var methods = type.GetMethods();
        foreach (var m in methods)
        {
            if (!m.Name.StartsWith(methodName))
                continue;

            Instance.Patch(m, prefix, postfix);

            count++;
        }

        return count;
    }
}
