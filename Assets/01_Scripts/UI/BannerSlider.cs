using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class BannerSlider : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [Header("배너 이미지")]
    [SerializeField] private Sprite[] bannerImages;

    [Header("참조")]
    [SerializeField] private GameObject bannerPrefab;
    [SerializeField] private RectTransform content;

    [Header("배너 애니메이션 설정")]
    [SerializeField] private float autoSlideInterval = 5f;
    [SerializeField] private float slideDuration = 0.3f;
    [SerializeField] private float dragThreshold = 50f;

    [Header("인디케이터")]
    [SerializeField] private Image[] indicatorFills;

    public Action<int> OnBannerClick;
    private Image[] _bannerImages = new Image[3];
    private float _bannerWidth;
    private int _currentIndex = 0;
    private bool _isDragging = false;
    private bool _isAnimating = false;
    private Vector2 _dragStartPos;
    
    void Start()
    {
        _bannerWidth = GetComponent<RectTransform>().rect.width;

        CreateBanners();

        StartAutoSlide();
    }
    
    private void CreateBanners()
    {
        content.sizeDelta = new Vector2(_bannerWidth * 3, content.sizeDelta.y);

        for (int i = 0; i < 3; i++)
        {
            GameObject banner = Instantiate(bannerPrefab, content);
            RectTransform rt = banner.GetComponent<RectTransform>();

            rt.anchorMin = new Vector2(0, 0.5f);
            rt.anchorMax = new Vector2(0, 0.5f);
            rt.pivot = new Vector2(0, 0.5f);
            rt.anchoredPosition = new Vector2(i * _bannerWidth, 0);
            rt.sizeDelta = new Vector2(_bannerWidth, content.sizeDelta.y);

            _bannerImages[i] = banner.GetComponent<Image>();
        }

        SetBannerImages();

        content.anchoredPosition = new Vector2(-_bannerWidth, 0);

        ResetCurrentIndicator();
    }
    
    private void SetBannerImages()
    {
        int prevIdx = (_currentIndex - 1 + bannerImages.Length) % bannerImages.Length;
        int nextIdx = (_currentIndex + 1) % bannerImages.Length;

        _bannerImages[0].sprite = bannerImages[prevIdx];
        _bannerImages[1].sprite = bannerImages[_currentIndex];
        _bannerImages[2].sprite = bannerImages[nextIdx];
    }
    
    private void StartAutoSlide()
    {
        if (indicatorFills == null || _currentIndex >= indicatorFills.Length) return;
        
        indicatorFills[_currentIndex]
            .DOFillAmount(1f, autoSlideInterval)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                if (_isDragging || _isAnimating) return;

                GoToNext();
            });
    }
    
    private void StopAutoSlide()
    {
        if (indicatorFills == null) return;
        
        foreach (var indicator in indicatorFills)
        {
            DOTween.Kill(indicator);
        }
    }
    
    public void GoToNext()
    {
        if (_isAnimating) return;
        _isAnimating = true;
        
        FadeOutIndicator(_currentIndex);

        float targetX = -_bannerWidth * 2;

        content.DOAnchorPosX(targetX, slideDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                _currentIndex = (_currentIndex + 1) % bannerImages.Length;

                SetBannerImages();
                content.anchoredPosition = new Vector2(-_bannerWidth, 0);
                
                ResetCurrentIndicator();
                _isAnimating = false;
                StartAutoSlide();
            });
    }
    
    public void GoToPrev()
    {
        if (_isAnimating) return;
        _isAnimating = true;
        
        FadeOutIndicator(_currentIndex);

        float targetX = 0;

        content.DOAnchorPosX(targetX, slideDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                _currentIndex = (_currentIndex - 1 + bannerImages.Length) % bannerImages.Length;

                SetBannerImages();
                content.anchoredPosition = new Vector2(-_bannerWidth, 0);
                
                ResetCurrentIndicator();
                _isAnimating = false;
                StartAutoSlide();
            });
    }
    
    private void FadeOutIndicator(int index)
    {
        if (indicatorFills == null || index >= indicatorFills.Length) return;

        indicatorFills[index].DOFillAmount(0f, slideDuration).SetEase(Ease.OutQuad);
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_isAnimating) return;

        _isDragging = true;
        _dragStartPos = eventData.position;
        
        StopAutoSlide();

        DOTween.Kill(content);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isDragging || _isAnimating) return;

        float deltaX = eventData.delta.x;
        Vector2 pos = content.anchoredPosition;
        pos.x += deltaX;

        pos.x = Mathf.Clamp(pos.x, -_bannerWidth * 2, 0);
        content.anchoredPosition = pos;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_isDragging) return;

        _isDragging = false;

        DOTween.Kill(content);

        float dragDistance = eventData.position.x - _dragStartPos.x;

        if (Mathf.Abs(dragDistance) > dragThreshold)
        {
            if (dragDistance < 0)
                GoToNext();
            else
                GoToPrev();
        }
        else
        {
            content.DOAnchorPosX(-_bannerWidth, slideDuration).SetEase(Ease.OutQuad);
            
            indicatorFills[_currentIndex]
                .DOFillAmount(0f, slideDuration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => StartAutoSlide());
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        float dragDistance = Vector2.Distance(eventData.position, eventData.pressPosition);
        if (dragDistance < 10f)
        {
            OnBannerClick?.Invoke(_currentIndex);
            Debug.Log($"배너 {_currentIndex} 클릭");
        }
    }
    
    private void ResetCurrentIndicator()
    {
        if (indicatorFills == null || _currentIndex >= indicatorFills.Length) return;

        indicatorFills[_currentIndex].fillAmount = 0f;
    }
}
