using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Tooltip("캐릭터 이름")] public string characterName;
    [Tooltip("역할")] public Role role;
    [Tooltip("속성")] public Element element;
    [Tooltip("공격 거리")] public AttackRange attackRange;
    [Tooltip("프로필 이미지(300x350)")] public Sprite thumbnail;
    [Tooltip("전신 이미지")] public Sprite fullImage;
    [Tooltip("배경 이미지")] public Sprite background;

    [TextArea]
    [Tooltip("터치 시 대사들")] public string[] dialogues;
}
