using UnityEngine;
using DG.Tweening;

public class FullScreenToggle : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private RectTransform topLeft;
    [SerializeField] private RectTransform topRight;
    [SerializeField] private RectTransform bottomLeft;
    [SerializeField] private RectTransform bottomRight;

    private CanvasGroup topLeftCG;
    private CanvasGroup topRightCG;
    private CanvasGroup bottomLeftCG;
    private CanvasGroup bottomRightCG;

    [Header("Click Panel")]
    [SerializeField] private GameObject clickPanel;

    [Header("Settings")]
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private float hideOffset = 500f;
    [SerializeField] private Ease easeType = Ease.InOutQuart;

    private Vector2 topLeftOriginal;
    private Vector2 topRightOriginal;
    private Vector2 bottomLeftOriginal;
    private Vector2 bottomRightOriginal;

    private bool isFullScreen = false;

    void Start()
    {
        // 원래 위치 저장
        topLeftOriginal = topLeft.anchoredPosition;
        topRightOriginal = topRight.anchoredPosition;
        bottomLeftOriginal = bottomLeft.anchoredPosition;
        bottomRightOriginal = bottomRight.anchoredPosition;

        // CanvasGroup 참조
        topLeftCG = topLeft.GetComponent<CanvasGroup>();
        topRightCG = topRight.GetComponent<CanvasGroup>();
        bottomLeftCG = bottomLeft.GetComponent<CanvasGroup>();
        bottomRightCG = bottomRight.GetComponent<CanvasGroup>();

        clickPanel.SetActive(false);
    }
    
    public void OnFullScreenButtonClick()
    {
        if (isFullScreen) return;

        isFullScreen = true;
        
        topLeft.DOAnchorPosX(topLeftOriginal.x - hideOffset, duration).SetEase(easeType);
        topLeftCG.DOFade(0f, duration);
        bottomLeft.DOAnchorPosX(bottomLeftOriginal.x - hideOffset, duration).SetEase(easeType);
        bottomLeftCG.DOFade(0f, duration);
        
        topRight.DOAnchorPosX(topRightOriginal.x + hideOffset, duration).SetEase(easeType);
        topRightCG.DOFade(0f, duration);
        bottomRight.DOAnchorPosX(bottomRightOriginal.x + hideOffset, duration)
            .SetEase(easeType)
            .OnComplete(() => clickPanel.SetActive(true));
        bottomRightCG.DOFade(0f, duration);
    }
    
    public void OnClickPanelClick()
    {
        if (!isFullScreen) return;

        clickPanel.SetActive(false);
        isFullScreen = false;
        
        topLeft.DOAnchorPos(topLeftOriginal, duration).SetEase(easeType);
        topLeftCG.DOFade(1f, duration);
        topRight.DOAnchorPos(topRightOriginal, duration).SetEase(easeType);
        topRightCG.DOFade(1f, duration);
        bottomLeft.DOAnchorPos(bottomLeftOriginal, duration).SetEase(easeType);
        bottomLeftCG.DOFade(1f, duration);
        bottomRight.DOAnchorPos(bottomRightOriginal, duration).SetEase(easeType);
        bottomRightCG.DOFade(1f, duration);
    }
}
