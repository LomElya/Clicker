using System;
using UnityEngine;


[System.Serializable]
public class LevelConfig
{
    [SerializeField] private int _level;
    [SerializeField] private float _experienceForLevelUP;

    public int Level
    {
        get => _level;

        set
        {
            _level = value;
        }
    }

    public float ExperienceForLevelUP
    {
        get => _experienceForLevelUP;

        set
        {
            if (value <= 0)
                throw new ArgumentException(nameof(value));

            _experienceForLevelUP = value;
        }
    }



}
