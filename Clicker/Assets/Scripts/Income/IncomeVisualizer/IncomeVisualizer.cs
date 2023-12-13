using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public abstract class IncomeVisualizer : MonoBehaviour
{
    [SerializeField] private StringValueView _textPrefab;
    [SerializeField] private RectTransform SpawnPosition;

    private Wallet _wallet;

    private float _width => SpawnPosition.rect.width;
    private float _radius => _width / 2;

    private readonly Dictionary<string, CustomPool<StringValueView>> _pools =
      new Dictionary<string, CustomPool<StringValueView>>();

    private void OnDisable() =>
        _wallet.AddCoins -= Visualize;

    [Inject]
    private void Construct(Wallet wallet)
    {
        _wallet = wallet;
        _wallet.AddCoins += Visualize;
    }

    public void Visualize(int textValue)
    {
        var pool = GetPool(_textPrefab);
        StringValueView text = pool.Get();
        text.Show(textValue.ToString());

        Vector3 spawnRandom = Random.insideUnitCircle * _radius;

        text.transform.parent = SpawnPosition.parent;

        text.transform.position = SpawnPosition.position + spawnRandom;

        text.transform.localScale = SpawnPosition.localScale;

        Fly(text);
    }

    public abstract void Fly(StringValueView flyingText);

    protected void Hide(StringValueView flyingText)
    {
        var pool = GetPool(flyingText);

        pool.Release(flyingText);
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
