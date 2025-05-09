using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_LoadingScene : MonoBehaviour
{
    // 다음 씬을 비동기 로딩한다.
    // 진행률을 시각적으로 표현한다. (프로그레스 바와 텍스트)
    
    [SerializeField]
    private int _nextSceneIndex = 2;
    
    [SerializeField]
    private Slider _progressSlider;
    
    [SerializeField]
    private TextMeshProUGUI _progressText;

    private void Start()
    {
        StartCoroutine(LoadNextScene_Coroutine());
    }

    private IEnumerator LoadNextScene_Coroutine()
    {
        // 씬을 비동기 로딩한다.
        AsyncOperation ao = SceneManager.LoadSceneAsync(_nextSceneIndex);
        ao.allowSceneActivation = false; // 비동기로 로드되는 신의 모습을 화면에서 안보이게 한다.

        while (!ao.isDone)
        {
            // 로딩을 시각적으로 표현
            Debug.Log(ao.progress);
            _progressSlider.value = ao.progress;
            _progressText.text = $"{ao.progress * 100f:N0}%";
            
            // 이때 서버와 통신해서 필요한 데이터를 가져온다.

            if (ao.progress >= 0.9f)
            {
                ao.allowSceneActivation = true;
            }
            
            yield return null;
        }
    }
}