using System.Collections.Generic;

public interface IResettablePlatform
{
    void ResetPlatform();
}

public static class ResettablePlatformRegistry
{
    public static readonly List<IResettablePlatform> All = new();
}