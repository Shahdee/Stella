namespace Level
{
    public interface ILevelDataSaverLoader
    {
        void SaveLevel(LevelData levelData);  
        
        string  LoadLevel(int levelId);
    }
}