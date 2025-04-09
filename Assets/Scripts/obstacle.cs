using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] Transform objectToMove;
    [SerializeField] Transform pointA; // INICIO
    [SerializeField] Transform pointB; // FINAL
    [SerializeField] float upDuration = 1f; // DURACION DE A-B
    [SerializeField] float downDuration = 1f; // DURACION B-A
    [SerializeField] AnimationCurve upEase; 
    [SerializeField] AnimationCurve downEase;

    void Start()
    {
        StartCoroutine(MoveObject());
    }

    IEnumerator MoveObject()
    {
        while (true)
        {
            yield return StartCoroutine(MoveBetweenPoints(pointA.position, pointB.position, downDuration, upEase));
            yield return StartCoroutine(MoveBetweenPoints(pointB.position, pointA.position, upDuration, downEase));
        }
    }

    IEnumerator MoveBetweenPoints(Vector3 from, Vector3 to, float duration, AnimationCurve ease)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float curveValue = ease.Evaluate(elapsedTime / duration);

            objectToMove.position = Vector3.Lerp(from, to, curveValue);

            yield return null;
        }

        objectToMove.position = to;
    }
}
