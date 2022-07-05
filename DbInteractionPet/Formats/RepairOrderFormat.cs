namespace DbInteractionPet.Formats
{
    /// <summary>
    /// Класс, определюящий формат объектов типа RepairOrder
    /// </summary>
    public static class RepairOrderFormat
    {
        public const string  TableName = "RepairOrders";

        public const decimal MinCost = 0;

        public const string  PlannedEndDatePattern = @"^\d{2}\.\d{2}\.\d{4}$";
        public const string  RealEndDatePattern    = @"^\d{2}\.\d{2}\.\d{4}$";
    }
}
