using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashTimeOut : MonoBehaviour
{
    [SerializeField]
    private float delayBeforeLoading = 2f;

    private float timeElaspsed;

    private void Update()
    {
        timeElaspsed += Time.deltaTime;
        if (timeElaspsed > delayBeforeLoading)
        {

            SceneManager.LoadScene("MainScene");

        }
    }
}
