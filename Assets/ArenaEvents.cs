using System;

public static class ArenaEvents {
    static int flagCount;

    public static Action<int> flagCountChanged;
    public static Action nextFlagChanged;
    public static Action<int> lapCountChanged;
    public static Action onVictory;
    public static Action onRaceStarted;

    public static void AddFlag() => flagCountChanged?.Invoke(++flagCount);
    public static void FlagCleared() {
        flagCountChanged?.Invoke(--flagCount);
        nextFlagChanged?.Invoke();
    }

    public static void LapCountChanged(int newCount) =>
        lapCountChanged?.Invoke(newCount);

    public static void Victory() => onVictory?.Invoke();

    public static void LoadNextLevel() {
        if (LevelManager.instance == null) return;
        string next = LevelManager.instance.NextLevel();
        if (string.IsNullOrEmpty(next)) return;
        LevelManager.instance.LoadLevel(next);
    }

    public static void RaceStarted() => onRaceStarted?.Invoke();
}
