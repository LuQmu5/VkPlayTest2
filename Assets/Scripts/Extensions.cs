using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public static class Extensions
{
    public static void SetPositionX(this Transform transform, float x)
    {
        Vector3 newPosition = transform.position;
        newPosition.x = x;
        transform.position = newPosition;
    }

    public static void SetRandomAlpha(this SpriteRenderer spriteRenderer, float minAlpha = 0, float maxAlpha = 1)
    {
        Color newColor = spriteRenderer.color;
        newColor.a = Random.Range(minAlpha, maxAlpha);
        spriteRenderer.color = newColor;
    }

    public static IEnumerator TypeMode(this Text text, string content, float lettersPerSecond, bool replaceOldContent = true)
    {
        WaitForSeconds delay = new WaitForSeconds(1 / lettersPerSecond);

        if (replaceOldContent)
            text.text = "";

        foreach (char letter in content)
        {
            yield return delay;

            text.text += letter;
        }
    }
}