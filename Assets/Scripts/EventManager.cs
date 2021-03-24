public static class EventManager
{
    public delegate void ActionDelegate();
    public delegate void FinishLevelDelegate(int plumbob);

    public static event ActionDelegate OnPlumbobCollected;
    public static event ActionDelegate OnPlumbobCountReset;
    public static event FinishLevelDelegate OnLevelFinished;
    public static event ActionDelegate OnLevelFailed;

    public static void InvokePlumbobCollectEvent() => OnPlumbobCollected.Invoke();
    public static void InvokeLevelFinishEvent(int multiplier) => OnLevelFinished.Invoke(multiplier);

    public static void InvokeLevelFailedEvent() => OnLevelFailed.Invoke();
}
