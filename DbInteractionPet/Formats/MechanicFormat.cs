namespace DbInteractionPet.Formats
{
    /// <summary>
    /// Класс, определюящий формат объектов типа Mechanic
    /// </summary>
    public static class MechanicFormat
    {
        public const string TableName = "Mechanics";

        public const int    MinId = 0;

        public const string FullNamePattern = @"^[A-Z][a-z]{0,} [A-Z][a-z]{0,}$";
        public const string AddressPattern  = @"(\w | \d | \s){0,}";
        public const string NumberPattern   = @"^\+\d{11}$";

        public const int    MinCategory = 1;
        public const int    MaxCategory = 7;
    }
}
