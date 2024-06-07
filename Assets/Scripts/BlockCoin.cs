using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCoin : MonoBehaviour
    {
    private void Start() {
        GameManager.Instance.AddCoin();
        
        StartCoroutine(Animate());
    }

    private IEnumerator Animate() {

        Vector3 restingPosition = transform.localPosition;
        Vector3 animationPosition = restingPosition + Vector3.up * 2f;

        yield return Move(restingPosition, animationPosition);
        yield return Move(animationPosition, restingPosition);

        Destroy(gameObject);
    }

    private IEnumerator Move(Vector3 from, Vector3 to) {
        float elapsed = 0f;
        float duration = 0.25f;

        while (elapsed < duration) {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = to;
    }
}
