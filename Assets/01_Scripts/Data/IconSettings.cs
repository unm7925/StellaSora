using UnityEngine;

[CreateAssetMenu(fileName = "IconSettings", menuName = "Data/IconSettings")]
public class IconSettings : ScriptableObject
{
    [Header("속성 아이콘")]
    public Sprite[] elementIcons;

    [Header("공격 거리 아이콘")]
    public Sprite[] attackRangeIcons;
    
    public Sprite GetElementIcon(Element element) => elementIcons[(int)element];
    public Sprite GetAttackRangeIcon(AttackRange range) => attackRangeIcons[(int)range];
}
