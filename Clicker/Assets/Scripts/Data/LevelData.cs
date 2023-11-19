using System;
using Newtonsoft.Json;

public class LevelData
{
    private int _currentLevel;
    private float _experience;

    public LevelData()
    {
        _currentLevel = 1;
        _experience = 0;
    }

    [JsonConstructor]
    public LevelData(int level, float experience)
    {
        CurrentLevel = level;
        Experience = experience;
    }

    public int CurrentLevel
    {
        get => _currentLevel;

        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            _currentLevel = value;
        }
    }

    public float Experience
    {
        get => _experience;

        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _experience = value;
        }
    }

    public void LevelUP()
    {
        _currentLevel++;
        _experience = 0;
    }

    public void AddExperience(float value) =>
        _experience += value;
}