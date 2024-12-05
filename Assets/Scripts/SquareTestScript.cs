using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquareTestScript : MonoBehaviour
{
    [SerializeField] private Text _testText;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        transform.SetPositionX(10);
        _spriteRenderer.SetRandomAlpha(minAlpha: 0.5f);

        string content = "Доброго времени суток уважаемые дамы и господа программисты, сегодня будет пост по языку C# + пример использования на Unity.";
        float secondsToType = 3;
        StartCoroutine(_testText.TypeMode(content, content.Length / secondsToType));
    }
}
