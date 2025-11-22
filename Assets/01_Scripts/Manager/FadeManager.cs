using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeManager : Singleton<FadeManager>
{
    [SerializeField] private RectTransform fadeImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image logoImage;
    [SerializeField] private Image[] halftoneImages;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private Ease easeType = Ease.Linear;

    [Header("추가 이동 거리")]
    [SerializeField] private float halftoneOffset = 950f;

    [Header("로고 랜덤 이미지")]
    [SerializeField] private Image randomImage;
    [SerializeField] private Sprite[] randomSprites;

    private float _screenWidth;
    private CanvasGroup _canvasGroup;
    private bool _isPlaying;

    void Start()
    {
        _screenWidth = Screen.width;
        _canvasGroup = fadeImage.GetComponent<CanvasGroup>();

        fadeImage.gameObject.SetActive(true);
        fadeImage.anchoredPosition = new Vector2(_screenWidth + halftoneOffset, 0);
        _canvasGroup.alpha = 1f;
    }
    
    public void PlayFade(float holdTime = 0f, Action loading = null, Color? bgColor = null, Color? logoColor = null)
    {
        if (_isPlaying) return;
        _isPlaying = true;
        
        Color bg = bgColor ?? Color.black;
        backgroundImage.color = bg;
        logoImage.color = logoColor ?? Color.white;
        foreach (Image halftone in halftoneImages)
            halftone.color = bg;
        
        float totalOffset = _screenWidth + halftoneOffset;
        fadeImage.anchoredPosition = new Vector2(totalOffset, 0);
        _canvasGroup.alpha = 0f;
        
        int randomIndex = UnityEngine.Random.Range(0, randomSprites.Length);
        randomImage.sprite = randomSprites[randomIndex];
        
        fadeImage.DOAnchorPosX(0, duration)
            .SetEase(easeType);
        _canvasGroup.DOFade(1f, duration)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                loading?.Invoke();

                DOVirtual.DelayedCall(holdTime, () =>
                {
                    fadeImage.DOAnchorPosX(-totalOffset, duration)
                        .SetEase(easeType);
                    _canvasGroup.DOFade(0f, duration)
                        .SetEase(easeType)
                        .OnComplete(() =>
                        {
                            _isPlaying = false;
                        });
                });
            });
    }
}
