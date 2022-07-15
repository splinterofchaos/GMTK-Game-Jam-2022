using System;

public static class ArenaEvents {
    static int flagCount;

    public static Action<int> flagCountChanged;

    public static void AddFlag() => flagCountChanged?.Invoke(++flagCount);
    public static void FlagCleared() =>
        flagCountChanged?.Invoke(--flagCount);
}
