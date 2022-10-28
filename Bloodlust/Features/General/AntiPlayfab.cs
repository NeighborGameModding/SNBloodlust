using Configuration.Configs;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloodlust.Features.General;

internal static class AntiPlayfab
{
    public static void Initialize()
    {
        //MelonEvents.OnUpdate.Subscribe(CheckConfig);
    }

    private static void CheckConfig()
    {
        if (MainConfig.instance == null)
            return;

        MelonEvents.OnUpdate.Unsubscribe(CheckConfig);
        OnMainConfigLoaded(MainConfig.instance);
    }

    private static void OnMainConfigLoaded(MainConfig config)
    {
        config.backendType = BackendType.OFFLINE;
    }
}
