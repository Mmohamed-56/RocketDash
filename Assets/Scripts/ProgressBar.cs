using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressBar : MonoBehaviour{

   public Slider slider;
    [SerializeField] private float fillSpeed = 0.5f;
    [SerializeField] private float targetProgress = 0f;


    public void Update()
    {
        
    }
    public void incrementProgress(float amount){
        targetProgress += amount;
        if (targetProgress > 1f){
            targetProgress = 1f;
        }
        if (targetProgress < 0f){
            targetProgress = 0f;
        }

        while(slider.value < targetProgress){
            slider.value += fillSpeed * Time.deltaTime;
            if (slider.value > targetProgress){
                slider.value = targetProgress;
            }
        }
    }
}
