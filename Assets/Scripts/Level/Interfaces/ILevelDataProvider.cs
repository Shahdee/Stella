namespace Level
{
    public interface ILevelDataProvider
    {
        LevelData LoadLevelData(int levelId);
    }
}