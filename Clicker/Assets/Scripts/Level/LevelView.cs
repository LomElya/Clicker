using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelView : MonoBehaviour
{
    [SerializeField] private Image _levelBarFilling;
    [SerializeField] private TMP_Text _levelValue;

    private Level _level;

    [Inject]
    public void Init(Level level)
    {
        _level = level;

        UpdateLevel();
        UpdateExperience(_level.GetCurrentExperience());

        _level.LevelChanged += UpdateLevel;
        _level.ExperienceChanged += UpdateExperience;
    }

    private void OnDestroy()
    {
        _level.LevelChanged -= UpdateLevel;
        _level.ExperienceChanged -= UpdateExperience;
    }

    private void UpdateLevel() => _levelValue.text = _level.GetCurrentLevel().ToString();

    private void UpdateExperience(float addedExperience) =>
        _levelBarFilling.fillAmount = addedExperience;
}
