using System.Collections;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Transform[] spawnposition;
    [SerializeField] TextMeshProUGUI pointsText;

    [SerializeField] Transform Cube0;
    [SerializeField] Transform Cube1;
    [SerializeField] Transform Cube2;

    public int points = 0;  

    bool isAnimating = false;

    private void Start()
    {
        int randomIndex = Random.Range(0, spawnposition.Length);
        transform.position = spawnposition[randomIndex].position + new Vector3(0, 2, 0);

        StartCoroutine(cube0Rotate());
        StartCoroutine(cube1Rotate());
        StartCoroutine(cube2Rotate());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int pointValue = Random.Range(5, 100);
            points += pointValue; 

            if (!isAnimating)
            {
                StartCoroutine(ShowPointAnimation(pointValue));
            }

            int randomIndex = Random.Range(0, spawnposition.Length);
            transform.position = spawnposition[randomIndex].position + new Vector3(0, 2, 0);
        }
    }

    //ANIMACIONES

    //ANIMACIONES CUBOS
    IEnumerator cube0Rotate()
    {
        while (true)
        {
            Cube0.Rotate(Vector3.up * 45 * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator cube1Rotate()
    {
        float time = 0;
        while (true)
        {
            Vector3 eje = new Vector3(Mathf.Sin(time), Mathf.Cos(time), Mathf.Sin(time * 2));
            Cube1.Rotate(eje * 90 * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator cube2Rotate()
    {
        while (true)
        {
            float anguloX = Mathf.Sin(Time.time * 2f) * 45f;
            float anguloZ = Mathf.Cos(Time.time * 1.5f) * 30f;
            Cube2.localRotation = Quaternion.Euler(anguloX, 0, anguloZ);
            yield return null;
        }
    }

    //ANIMACION PUNTUACION
    IEnumerator ShowPointAnimation(int value)
    {
        isAnimating = true;

        pointsText.text = $"+{value}";

        Vector3 startPosition = new Vector3(400, 220, 0);
        pointsText.transform.position = startPosition;

        pointsText.gameObject.SetActive(true);

        Vector3 targetPosition = startPosition + new Vector3(0, 50, 0);

        float timeElapsed = 0f;
        float animationDuration = 0.5f;  

        while (timeElapsed < animationDuration)
        {
            pointsText.transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / animationDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        pointsText.transform.position = targetPosition;

        yield return new WaitForSeconds(0.5f);

        timeElapsed = 0f;
        while (timeElapsed < 0.5f)
        {
            pointsText.alpha = Mathf.Lerp(1f, 0f, timeElapsed / 0.5f);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        pointsText.alpha = 0f;
        pointsText.gameObject.SetActive(false);

        isAnimating = false;

        timeElapsed = 0;
    }
}
