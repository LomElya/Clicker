using UnityEngine;
using Zenject;

public class ClickablePanel : MonoBehaviour
{
    [SerializeField] private LevelDataConfig _levelDataConfig;

    private LevelConfig _levelConfig;

    private ClickableItemView _item;

    private IDataProvider _dataProvider;

    private Wallet _wallet;

    private Level _level;

    private CountItemChecker _countItemChecker;
    private SelectedItemChecker _selectedItemChecher;
    private SelectItem _selectItem;

    private CurrentLevelChecker _currentLevelChecker;

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        _item.Click -= OnItemViewClicked;
        _level.LevelChanged -= ChangeLevel;
    }

    [Inject]
    public void Init(IDataProvider dataProvider, Wallet wallet, ClickableItemView item, Level level,
    CountItemChecker countItemChecker, SelectedItemChecker selectedItemChecher, SelectItem selectItem, CurrentLevelChecker currentLevelChecker)
    {
        _dataProvider = dataProvider;

        _item = item;

        _countItemChecker = countItemChecker;
        _selectedItemChecher = selectedItemChecher;
        _selectItem = selectItem;

        _currentLevelChecker = currentLevelChecker;

        _wallet = wallet;

        _level = level;

        ChangeLevel();

        _item.Click += OnItemViewClicked;
        _level.LevelChanged += ChangeLevel;
    }

    private void OnItemViewClicked(ClickableItemView item)
    {
        _countItemChecker.Visit(ItemType.Cursor);

        if (_countItemChecker.Count <= 0)
            return;

        _wallet.AddCoin(_countItemChecker.Count * item.Multiplier);

        AddExperience(item);

        //_dataProvider.Save();
    }

    private void AddExperience(ClickableItemView item)
    {
        _level.AddExperience(_countItemChecker.Count * item.Multiplier);

        _currentLevelChecker.Visit();

        if (_levelConfig.ExperienceForLevelUP <= _currentLevelChecker.Experience)
            _level.AddLevel();
    }

    private void ChangeLevel()
    {
        _currentLevelChecker.Visit();
        _levelConfig = _levelDataConfig.Get(_currentLevelChecker.Level);
        _level.SetMaxExperience(_levelConfig.ExperienceForLevelUP);
    }
}
