using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomIncomeVisualizer : IncomeVisualizer
{
    [Header("Offset")]
    [SerializeField, Range(0, 300)] private float _maxOffset;
    [SerializeField, Range(0, -300)] private float _minOffset;
    private float _offsetX;
    private float _offsetY;

    [SerializeField] private float _duration = 3.0f;

    public override void Fly(StringValueView flyingText)
    {
        flyingText.Fade(_duration);

        var move = flyingText.transform.DOMove(EndPoint(flyingText), _duration);

        move.onComplete += () => Hide(flyingText);
    }

    private Vector3 EndPoint(StringValueView flyingText)
    {
        float localPositionX = flyingText.transform.localPosition.x;
        float localPositionY = flyingText.transform.localPosition.y;

        bool isUP = localPositionY > 0f;
        bool isRight = localPositionX > 0f;

        if (isRight)
            _offsetX = Random.Range(localPositionX, _maxOffset);
        else
            _offsetX = Random.Range(localPositionX, _minOffset);

        if (isUP)
            _offsetY = Random.Range(localPositionY, _maxOffset);
        else
            _offsetY = Random.Range(localPositionY, _minOffset);

        return flyingText.transform.position + new Vector3(_offsetX, _offsetY);
    }
}
