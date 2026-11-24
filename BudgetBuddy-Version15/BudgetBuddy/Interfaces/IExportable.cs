namespace BudgetBuddy.Interfaces
{
    /// <summary>
    /// Interface implemented by report classes capable of exporting data to CSV files.
    /// </summary>
    public interface IExportable
    {
        void ExportToCsv(string filePath);
    }
}
