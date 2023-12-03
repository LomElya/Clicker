using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class ProfitClickDisplay : MonoBehaviour
{
    public event Action<StringValueView> Show;

    [SerializeField] private StringValueView _textPrefab;

    [field: SerializeField] public Transform SpawnPoint { get; private set; }

    [field: SerializeField] public float MaxTime { get; private set; }
    
    [SerializeField] private float _minOffset;
    [SerializeField] private float _maxOffset;
    private float _offset;

    private ProfitClickAnimation _animation;

    private Wallet _wallet;


    private readonly Dictionary<string, CustomPool<StringValueView>> _pools =
         new Dictionary<string, CustomPool<StringValueView>>();

    private void OnDisable()
    {
        _wallet.AddCoins -= SetProfit;
        // _animation.Hide -= HideProfit;
    }

    [Inject]
    public void Init(Wallet wallet)
    {
        _wallet = wallet;

        _animation = new ProfitClickAnimation(this);

        //_animation.Init(this, _maxTime, _spawnPoint);

        _wallet.AddCoins += SetProfit;
        _animation.Hide += HideProfit;
    }

    public void SetProfit(int value)
    {
        var pool = GetPool(_textPrefab);

        _textPrefab.Show(value.ToString());

        StringValueView text = pool.Get();

        var offSetSpawnPoint = new Vector3(SpawnPoint.position.x + _offset, SpawnPoint.position.y);

        text.transform.parent = SpawnPoint;
        text.transform.position = offSetSpawnPoint;

        Debug.Log("Вызов анимации");

        Show?.Invoke(_textPrefab);
    }

    public void HideProfit(StringValueView text)
    {
        var pool = GetPool(text);

        Debug.Log("Crhsnm");

        pool.Release(text);
    }

    private CustomPool<StringValueView> GetPool(StringValueView text)
    {
        var objectTypeStr = text.GetType().ToString();
        CustomPool<StringValueView> pool;

        // Создать новый пул, если такого нет
        if (!_pools.ContainsKey(objectTypeStr))
        {
            pool = new CustomPool<StringValueView>(text, 3);
            _pools.Add(objectTypeStr, pool);
        }
        // Если пул есть, возвращаем его
        else
        {
            pool = _pools[objectTypeStr];
        }


        return pool;
    }
}
