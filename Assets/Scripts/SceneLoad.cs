using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public TextMeshProUGUI loadingPercent;

    public AudioClip move;
    public AudioClip close;
    public AudioSource sfx;
    
    private static SceneLoad instant;
    private static bool shouldPlayOpenningAnim = false; 
    
    private Animator anim;
    private AsyncOperation loadingSceneOperation;
    
    
    public static void SwitchScene(string sceneName)
    {
        instant.anim.SetTrigger("sceneStartLoading");

        instant.loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        instant.loadingSceneOperation.allowSceneActivation = false;
        
        
    }
    // Start is called before the first frame update
    void Start()
    {
        instant = this;
        
        
        anim = GetComponent<Animator>();
        
        if(shouldPlayOpenningAnim) anim.SetTrigger("sceneEndLoading");
    }

    // Update is called once per frame
    void Update()
    {
        if (loadingSceneOperation != null)
        {
            loadingPercent.text = Mathf.RoundToInt(loadingSceneOperation.progress * 100) + "%";
        }
    }


    public void PlayMoveSound()
    {
        sfx.PlayOneShot(move);
    }

    public void PlayCloseSound()
    {
        sfx.PlayOneShot(close);
    }
    public void OnAnimationOver()
    {
        shouldPlayOpenningAnim = true;
        instant.loadingSceneOperation.allowSceneActivation = true;
    }
}
