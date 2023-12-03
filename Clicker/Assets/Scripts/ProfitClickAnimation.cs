using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProfitClickAnimation : ITickable
{
    public event Action<StringValueView> Hide;

    private float _maxTime;

    private readonly List<ActiveText> _activeText
        = new List<ActiveText>();

    private Transform _spawnPoint;

    private Camera _camera;

    private ProfitClickDisplay _profit;

    private void OnDestroy() =>
         _profit.Show -= AddText;

    public ProfitClickAnimation(ProfitClickDisplay profit)
    {
        _profit = profit;

        _camera = Camera.main;

        _maxTime = _profit.MaxTime;

        _spawnPoint = _profit.SpawnPoint;

        _profit.Show += AddText;
    }

   /*  public void Init(ProfitClickDisplay profit, float maxTIme, Transform spawnPoin)
    {

        _profit = profit;

        _camera = Camera.main;

        _maxTime = maxTIme;

        _spawnPoint = spawnPoin;

        _profit.Show += AddText;
    } */

    private void AddText(StringValueView value)
    {
        ActiveText spawnText = new ActiveText(value, _maxTime, _maxTime, _spawnPoint.position);

        spawnText.AnimationText(_camera);

        Debug.Log("Анимация");

        if (!_activeText.Contains(spawnText))
            _activeText.Add(spawnText);
    }

    public void Tick()
    {
        Debug.Log("Тик");
        if (_activeText.Count == 0)
            return;

        foreach (ActiveText text in _activeText)
        {
            ActiveText activeText = text;
            activeText.ReduceTIme();

            if (activeText.Timer < 0f)
            {
                Debug.Log("1");
                //Сигнал о скрытии
                Hide?.Invoke(activeText.UIText);

                if (_activeText.Contains(text))
                    _activeText.Remove(text);
            }
            else
            {
                Debug.Log("2");
                //var color = activeText.UIText.color;
            }

        }
    }
}
public class ActiveText
{
    public StringValueView UIText { get; private set; }
    public float MaxTime { get; private set; }
    public float Timer { get; private set; }
    public Vector2 SpawnPosition { get; private set; }

    public ActiveText(StringValueView uiText, float maxTIme, float timer, Vector2 spawnPosition)
    {
        UIText = uiText;
        MaxTime = maxTIme;
        Timer = timer;
        SpawnPosition = spawnPosition;
    }

    public void AnimationText(Camera camera)
    {
        float delta = 1f - (Timer / MaxTime);

        Vector2 spawn = SpawnPosition + new Vector2(delta / 10, delta / 10);

        spawn = camera.WorldToScreenPoint(spawn);

        UIText.transform.position = spawn;
    }

    public void ReduceTIme() =>
        Timer -= Time.deltaTime;
}
