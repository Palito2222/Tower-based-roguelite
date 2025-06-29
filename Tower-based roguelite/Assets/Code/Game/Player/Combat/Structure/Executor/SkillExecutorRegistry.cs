using System.Collections.Generic;

public static class SkillExecutorRegistry
{
    private static readonly Dictionary<string, ISkillExecutor> executors = new()
    {
        { "KaoruSingExplosion", new KaoruSingExplosionExecutor() }
    };

    public static ISkillExecutor GetExecutor(string key)
    {
        return executors.TryGetValue(key, out var executor) ? executor : null;
    }
}
