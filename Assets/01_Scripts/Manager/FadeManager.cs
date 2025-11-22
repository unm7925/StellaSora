using System;
using UnityEngine;
using DG.Tweening;

public class FadeManager : Singleton<FadeManager>
{
    [SerializeField] private RectTransform fadeImage;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private Ease easeType = Ease.Linear;
    
    [Header("[테스트] 대기시간 / 스페이스로 페이드 호출")]
    [SerializeField] private float TestholdTime = 0.5f;

    private float _screenWidth;

    void Start()
    {
        _screenWidth = Screen.width;
        fadeImage.gameObject.SetActive(true); 
        fadeImage.anchoredPosition = new Vector2(_screenWidth, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayFade(TestholdTime);
        }
    }
    
    public void PlayFade(float holdTime = 0f, Action loading = null)
    {
        fadeImage.anchoredPosition = new Vector2(_screenWidth, 0);
        
        fadeImage.DOAnchorPosX(0, duration)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                loading?.Invoke();

                DOVirtual.DelayedCall(holdTime, () =>
                {
                    fadeImage.DOAnchorPosX(-_screenWidth, duration)
                        .SetEase(easeType);
                });
            });
    }
}
