using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CharacterCard : MonoBehaviour, IPointerDownHandler
{
    [Header("Dialogue Settings")]
    [SerializeField] private string[] dialogue;
    
    [Header("Animation Settings")]
    [SerializeField] private float bounceDistance = 20f;
    [SerializeField] private float bounceSpeed = 0.3f;
    [SerializeField] private float bubbleDisplayTime = 2f;
    [SerializeField] private float bubbleFadeSpeed = 0.3f;
    
    [Header("References")]
    [SerializeField] private RectTransform characterImage;
    [SerializeField] private GameObject speechBubble;
    [SerializeField] private TextMeshProUGUI text;

    
    private CanvasGroup _canvasGroup;
    private Sequence _bubbleSequence;
    private Tween _bounceAnimation;
    private float _defaultY;
    private bool _isAnimating;
    private int _lastDialogueIndex = -1;
    
    private void Awake()
    {
        _defaultY = characterImage.anchoredPosition.y;
        _canvasGroup = speechBubble.GetComponent<CanvasGroup>();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isAnimating) return;
        
        PlayBounceAnimation();
        ShowSpeechBubble();
    }
    
    private void PlayBounceAnimation()
    {
        _bounceAnimation?.Kill();
        _isAnimating = true;
        
        characterImage.anchoredPosition = new Vector2(characterImage.anchoredPosition.x, _defaultY);
        
        _bounceAnimation = characterImage.DOAnchorPosY(_defaultY - bounceDistance, bounceSpeed / 2)
            .SetLoops(2, LoopType.Yoyo)
            .OnComplete(() => _isAnimating = false);
    }

    private void ShowSpeechBubble()
    {
        int number;
        if (dialogue.Length > 1)
        {
            do
            {
                number = Random.Range(0, dialogue.Length);
            } while (number == _lastDialogueIndex);
        }
        else
        {
            number = 0;
        }
        _lastDialogueIndex = number;
        text.text = dialogue[number];
        LayoutRebuilder.ForceRebuildLayoutImmediate(speechBubble.GetComponent<RectTransform>());
        BubbleFade();
    }

    private void BubbleFade()
    {
        _bubbleSequence?.Kill();
        _bubbleSequence = DOTween.Sequence();
        _bubbleSequence.Append(_canvasGroup.DOFade(1f, bubbleFadeSpeed));
        _bubbleSequence.AppendInterval(bubbleDisplayTime);
        _bubbleSequence.Append(_canvasGroup.DOFade(0f, bubbleFadeSpeed));
    }
}