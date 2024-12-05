using DG.Tweening;
using UnityEngine;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using static Unity.Collections.AllocatorManager;

public class DotweenTestScript : MonoBehaviour
{
    private void Start()
    {
        /*
        TweenerCore<Vector3, Vector3, VectorOptions> returnValue1 = transform.DOMove(Vector2.left * 10, 2);

        Color endColor = Color.red;
        float duration = 2;

        TweenerCore<Color, Color, ColorOptions> returnValue2 = GetComponent<SpriteRenderer>().DOColor(endColor, duration);

        12.ToString().ToUpper();

        */

        // transform.DOMove(new Vector3(2, 2, 2), 2).SetEase(Ease.InOutExpo).SetLoops(3).OnComplete(PrintHi);

        Sequence exampleSeq = DOTween.Sequence();

        exampleSeq.Append(transform.DOMoveY(3, 1))
          .Append(transform.DORotate(new Vector3(0, 180, 0), 1))
          .PrependInterval(5)
          .Insert(0, transform.DOScale(new Vector3(3, 3, 3), exampleSeq.Duration()));

        print(exampleSeq.Duration());
    }

    private void PrintHi()
    {
        print("hi!");
    }
}
