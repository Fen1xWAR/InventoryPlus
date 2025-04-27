namespace InventoryPlus.Domain.CustomModels;

public class CabinetFullInfo
{
    public string BuildingName { get; set; }
    public string BuildingAddress { get; set; }
    public string CabinetNumber { get; set; }
    public EmployeeReportInfo ResponsibleEmployee { get; set; }
    public string Description { get; set; }
}