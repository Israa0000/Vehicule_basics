using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI deathText;  
    public TextMeshProUGUI scoreText;  
    public Button restartButton;  
    public float fadeDuration = 1f; 
    public GameObject panelGameOver; 

    private CanvasGroup canvasGroup;  //PANEL

    private void Start()
    {
        canvasGroup = panelGameOver.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = panelGameOver.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0;
        deathText.alpha = 0;
        restartButton.interactable = false;
    }

    public void TurnPanelOn()
    {
        Time.timeScale = 0; 
        panelGameOver.SetActive(true);  
        Coin coinScript = FindObjectOfType<Coin>();  
        scoreText.text = "Score: " + coinScript.points.ToString(); 
        StartCoroutine(FadeIn()); 
    }

    public void Restart()
    {
        Debug.Log("Restart");
        Time.timeScale = 1; 
        SceneManager.LoadScene("Game"); 
    }

    private IEnumerator FadeIn()
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime; 
            float alpha = Mathf.Clamp01(time / fadeDuration); 
            canvasGroup.alpha = alpha; 

            deathText.alpha = alpha;

            restartButton.image.color = new Color(restartButton.image.color.r, restartButton.image.color.g, restartButton.image.color.b, alpha);
            restartButton.interactable = alpha > 0.1f;  

            yield return null;
        }

        canvasGroup.alpha = 1;
        deathText.alpha = 1;
        restartButton.image.color = new Color(restartButton.image.color.r, restartButton.image.color.g, restartButton.image.color.b, 1); 
        restartButton.interactable = true; 
    }
}
