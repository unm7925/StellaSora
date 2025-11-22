using UnityEngine;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    [System.Serializable]
    public class UICorner
    {
        public RectTransform rect;
        [Tooltip("숨길 방향 (-1=왼쪽, 1=오른쪽)")]
        public float hideDirectionX;
    }

    [Header("참조")]
    [SerializeField] private UICorner[] corners;

    [Header("설정")]
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private float hideOffset = 500f;
    [SerializeField] private Ease easeType = Ease.InOutQuart;

    private CanvasGroup[] _canvasGroups;
    private Vector2[] _originalPositions;
    private bool _isUIHidden = false;

    void Start()
    {
        int count = corners.Length;
        _canvasGroups = new CanvasGroup[count];
        _originalPositions = new Vector2[count];

        for (int i = 0; i < count; i++)
        {
            _originalPositions[i] = corners[i].rect.anchoredPosition;
            _canvasGroups[i] = corners[i].rect.GetComponent<CanvasGroup>();
        }
    }

    public void HideUI(System.Action onComplete = null)
    {
        if (_isUIHidden) return;
        _isUIHidden = true;

        for (int i = 0; i < corners.Length; i++)
        {
            float targetX = _originalPositions[i].x + (corners[i].hideDirectionX * hideOffset);
            corners[i].rect.DOAnchorPosX(targetX, duration).SetEase(easeType);
            _canvasGroups[i].DOFade(0f, duration);
        }
        
        DOVirtual.DelayedCall(duration, () => onComplete?.Invoke());
    }

    public void ShowUI(System.Action onComplete = null)
    {
        if (!_isUIHidden) return;
        _isUIHidden = false;

        for (int i = 0; i < corners.Length; i++)
        {
            corners[i].rect.DOAnchorPos(_originalPositions[i], duration).SetEase(easeType);
            _canvasGroups[i].DOFade(1f, duration);
        }

        DOVirtual.DelayedCall(duration, () => onComplete?.Invoke());
    }
}
