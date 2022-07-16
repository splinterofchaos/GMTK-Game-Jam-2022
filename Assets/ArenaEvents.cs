using System;

public static class ArenaEvents {
    static int flagCount;

    public static Action<int> flagCountChanged;
    public static Action nextFlagChanged;
    public static Action<int> lapCountChanged;
    public static Action<int> setLapCount;

    public static void AddFlag() => flagCountChanged?.Invoke(++flagCount);
    public static void FlagCleared() {
        flagCountChanged?.Invoke(--flagCount);
        nextFlagChanged?.Invoke();
    }

    public static void SetLapCount(int count) =>
        setLapCount?.Invoke(count);

    public static void LapCountChanged(int newCount) =>
        lapCountChanged?.Invoke(newCount);
}
