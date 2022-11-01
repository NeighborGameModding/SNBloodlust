using Bloodlust.Menu;
using Bloodlust.Menu.Elements;

namespace Bloodlust.Features.MenuCategories;

public static class GameController
{
    private static BloodlustMenu.Category _category;

    public static void Initialize()
    {
        var triggerNeighborVictoryButton = new ButtonElement("Trigger Neighbor Victory", TriggerNeighborVictory);
        var triggerKidsVictoryButton = new ButtonElement("Trigger Kids Victory", TriggerKidsVictory);
        var triggerTimeout = new ButtonElement("Trigger Timeout", TriggerTimeout);
        var triggerQuestEnding = new ButtonElement("Trigger Quest Ending", TriggerQuestEnding);

        _category = BloodlustMenu.Category.Create("Game Controller", new()
        {
            triggerNeighborVictoryButton,
            triggerKidsVictoryButton,
            triggerTimeout,
            triggerQuestEnding
        });

        GameEvents.OnGameModeChanged.Subscribe(OnGameModeChanged);
    }

    private static void TriggerQuestEnding()
    {
        EndGame(EndingType.QUEST_COMPLETED);
    }

    private static void TriggerTimeout()
    {
        EndGame(EndingType.TIME_IS_UP);
    }

    private static void TriggerKidsVictory()
    {
        EndGame(EndingType.BASEMENT_ENTERED);
    }

    private static void TriggerNeighborVictory()
    {
        EndGame(EndingType.ALL_CHILDREN_DEAD);
    }

    private static void EndGame(EndingType type)
    {
        BloodyHoloNetMessenger.SendMessage(Messages.EndMatchMessage(type));
    }

    private static void OnGameModeChanged(GameMode gameMode)
    {
        _category.Enabled = gameMode == GameMode.GAMEPLAY;
    }
}
