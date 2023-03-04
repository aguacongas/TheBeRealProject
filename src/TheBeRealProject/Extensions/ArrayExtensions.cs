namespace System;

public static class ArrayExtensions
{
    public static int GetBoundIndex(this Array array, int from)
    {
        if (array is null || array.Length == 0)
        {
            return 0;
        }
        if (from < array.Length)
        {
            return from;
        }
        return array.GetBoundIndex(from - array.Length);
    }
}
