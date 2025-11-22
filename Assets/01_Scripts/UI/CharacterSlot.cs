using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSlot : MonoBehaviour
{
    [SerializeField] private Image thumbnailImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI positionText;
    [SerializeField] private Image elementIcon;
    [SerializeField] private Image attackRangeIcon;

    private CharacterData _characterData;
    private CharacterSelectPanel _panel;

    public void Initialize(CharacterData data, CharacterSelectPanel selectPanel, IconSettings iconSettings)
    {
        _characterData = data;
        _panel = selectPanel;
        thumbnailImage.sprite = data.thumbnail;
        nameText.text = data.characterName;
        positionText.text = data.role.ToKorean();
        elementIcon.sprite = iconSettings.GetElementIcon(data.element);
        attackRangeIcon.sprite = iconSettings.GetAttackRangeIcon(data.attackRange);

        GetComponent<Button>().onClick.AddListener(() => _panel.SelectCharacter(_characterData));
    }
}