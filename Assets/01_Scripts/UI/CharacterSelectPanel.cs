using UnityEngine;
using DG.Tweening;

public class CharacterSelectPanel : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private Transform content;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private MainCharacterDisplay mainCharacterDisplay;
    [SerializeField] private IconSettings iconSettings;

    [Header("패널")]
    [SerializeField] private RectTransform panel;

    [Header("애니메이션")]
    [SerializeField] private float slideDuration = 0.5f;
    [SerializeField] private Ease slideEase = Ease.OutQuad;

    private bool isInitialized = false;
    private bool isPanelOpen = false;

    private readonly Vector2 shownOffset = new Vector2(0f, -80f);
    private Vector2 hiddenOffset;

    void Awake()
    {
        panel.gameObject.SetActive(true);
    }

    void Start()
    {
        float panelHeight = panel.rect.height;
        hiddenOffset = new Vector2(-panelHeight - 200f, -panelHeight - 300f);

        SetPanelOffset(hiddenOffset);
    }

    private void SetPanelOffset(Vector2 offset)
    {
        panel.offsetMin = new Vector2(panel.offsetMin.x, offset.x);
        panel.offsetMax = new Vector2(panel.offsetMax.x, offset.y);
    }

    void Update()
    {
        if (isPanelOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePanel();
        }
    }

    public void OpenPanel()
    {
        if (isPanelOpen) return;
        isPanelOpen = true;

        if (!isInitialized)
        {
            CreateSlots();
            isInitialized = true;
        }

        UIManager.Instance.HideUI();
        AnimatePanelTo(shownOffset);
    }

    private void AnimatePanelTo(Vector2 targetOffset)
    {
        Vector2 currentOffset = new Vector2(panel.offsetMin.y, panel.offsetMax.y);
        DOVirtual.Float(0f, 1f, slideDuration, t =>
        {
            Vector2 lerped = Vector2.Lerp(currentOffset, targetOffset, t);
            SetPanelOffset(lerped);
        }).SetEase(slideEase);
    }

    private void CreateSlots()
    {
        CharacterData[] characters = Resources.LoadAll<CharacterData>("ScriptableObject/CharacterData");

        foreach (CharacterData data in characters)
        {
            GameObject slotObj = Instantiate(slotPrefab, content);
            CharacterSlot slot = slotObj.GetComponent<CharacterSlot>();
            slot.Initialize(data, this, iconSettings);
        }
    }

    public void SelectCharacter(CharacterData data)
    {
        FadeManager.Instance.PlayFade(1f, () =>
        {
            mainCharacterDisplay.SetCharacter(data);

            SetPanelOffset(hiddenOffset);
            UIManager.Instance.ShowUI();
            isPanelOpen = false;
        }, Color.white, Color.black);
    }

    public void ClosePanel()
    {
        isPanelOpen = false;
        AnimatePanelTo(hiddenOffset);
        UIManager.Instance.ShowUI();
    }
}
