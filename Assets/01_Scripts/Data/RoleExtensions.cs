public static class RoleExtensions
{
    public static string ToKorean(this Role role)
    {
        return role switch
        {
            Role.Dealer => "딜러",
            Role.Balance => "밸런스",
            Role.Supporter => "서포터",
            _ => ""
        };
    }
}
