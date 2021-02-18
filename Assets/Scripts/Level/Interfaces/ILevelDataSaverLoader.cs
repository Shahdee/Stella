namespace Level
{
    public interface ILevelDataSaverLoader
    {
        void SaveLevel(LevelData levelData, int levelId); // TODO probably levelId will be inside levelData 
        
        string  LoadLevel(int levelId);
    }
}