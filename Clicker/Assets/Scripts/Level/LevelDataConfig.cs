using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelDataConfig", menuName = "Level/LevelDataConfig")]
public class LevelDataConfig : ScriptableObject
{
    [SerializeField] private List<LevelConfig> _levelConfig;

    public IEnumerable<LevelConfig> LevelConfig => _levelConfig;

    private void OnValidate()
    {
        int level = 1;
        float experience = 10;

        foreach (LevelConfig levelConfig in _levelConfig)
        {
            levelConfig.Level = level;
            levelConfig.ExperienceForLevelUP = experience;

            level++;
            experience *= 1.4f;
        }
    }

    public LevelConfig Get(int currentLevel) =>
        _levelConfig.FirstOrDefault(x => x.Level == currentLevel);

}
