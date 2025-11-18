using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressBar : MonoBehaviour{
    
    [SerializeField] private Transform player;
    [SerializeField] private Transform landingPad;
    [SerializeField] private Slider progressBar;

    private float startX;
    private float endX;

    void Start()
    {
        startX = player.position.x;
        endX = landingPad.position.x;

        progressBar.value = 0f;
        progressBar.maxValue = endX - startX;
    }


    private void Update()
    {
        float distanceCovered = player.position.x - startX;
        progressBar.value = Mathf.Clamp(distanceCovered, 0, progressBar.maxValue);
    }
      
}
