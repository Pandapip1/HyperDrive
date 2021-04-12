namespace tk.pandapip1.AmongUsRoles
{
    public static class RoleApi
    {
        private static List<string> Roles = new List<string>();
        private static List<string> Teams = new List<string>();
        private static Dictionary<byte, string> Pid2Role = new List<string>();
        private static Dictionary<byte, string> Role2Team = new List<string>();
        
        public static void CreateRole(string guid)
        {
            Roles.Add(guid);
        }
        
        public static void CreateTeam(string guid)
        {
            Teams.Add(guid);
        }
        
        public static void AssignRoleToTeam(string role, string team)
        {
            Role2Team[role] = team;
        }
        
        public static void AssignRoleToPlayer(string role, string team)
        {
        }
        
        public static string GetUserRole(byte playerId)
        {
            if (Pid2Role.ContainsKey(playerId)) return Pid2Role[playerId];
            return null;
        }
        
        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.HandleRpc))]
        private static class RPCHandler
        {
            public static bool Prefix([HarmonyArgument(0)] byte callId, [HarmonyArgument(1)] MessageReader reader)
            {
                if (callId == 69)
                {
                    Pid2Role[reader.ReadString()] = reader.ReadByte();
                }
            }
        }
    }
}