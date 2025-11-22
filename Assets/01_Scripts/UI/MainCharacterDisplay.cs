using UnityEngine;
using UnityEngine.UI;

public class MainCharacterDisplay : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private Image characterImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private CharacterCard characterCard;

    [Header("기본값")]
    [SerializeField] private CharacterData defaultCharacter;

    void Start()
    {
        if (defaultCharacter != null)
        {
            SetCharacter(defaultCharacter);
        }
    }

    public void SetCharacter(CharacterData data)
    {
        characterImage.sprite = data.fullImage;
        backgroundImage.sprite = data.background;
        characterCard.SetDialogues(data.dialogues);
    }
}
