using System;
using Zenject;

public class Level
{
    public event Action LevelChanged;
    public event Action<float> ExperienceChanged;

    private float _maxExperiance;

    private IPersistentData _persistentData;

    [Inject]
    public Level(IPersistentData persistentData) =>
        _persistentData = persistentData;

    public void AddLevel()
    {
        _persistentData.PlayerData.LevelUp();

        LevelChanged?.Invoke();
        ExperienceChanged?.Invoke(0f);
    }

    public void AddExperience(float addedExperience)
    {
        if (addedExperience < 0)
            throw new ArgumentOutOfRangeException(nameof(addedExperience));

        _persistentData.PlayerData.AddExperience(addedExperience);

        ExperienceChanged?.Invoke(GetCurrentExperience());
    }

    public void SetMaxExperience(float maxExperiance) => _maxExperiance = maxExperiance;

    public int GetCurrentLevel() => _persistentData.PlayerData.GetCurrentLevel();

    public float GetCurrentExperience() => _persistentData.PlayerData.GetCurrentExperience() / _maxExperiance;
}
