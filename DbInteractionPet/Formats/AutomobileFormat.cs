namespace DbInteractionPet.Formats
{
    /// <summary>
    /// Класс, определюящий формат объектов типа Automobile
    /// </summary>
    public static class AutomobileFormat
    {
        public const string TableName = "Automobiles";

        public const int    MinRegisterNumber = 100000;
        public const int    MaxRegisterNumber = 999999;

        public const string MarkPattern  = @"^[A-Z][a-z]{0,}$";
        public const string ColorPattern = @"^[A-Z][a-z]{0,}$";

        public const int    MinReleaseYear = 1900;
        public const int    MaxReleaseYear = 2021;
    }
}
