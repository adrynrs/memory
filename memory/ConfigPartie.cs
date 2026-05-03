namespace memory
{
    internal static class ConfigPartie
    {
        public static int NombrePaires   { get; set; } = 8;
        public static int NombreColonnes { get; set; } = 4;
        public static int TempsLimite    => NombreColonnes <= 4 ? 60 : 150;
    }
}
