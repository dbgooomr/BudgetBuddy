using System;
using System.Windows.Forms;
using BudgetBuddy.Data;
using BudgetBuddy.Forms;
using BudgetBuddy.Services;

namespace BudgetBuddy
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            InMemoryDataStore dataStore = new InMemoryDataStore();
            DatabaseHelper dbHelper = new DatabaseHelper(dataStore);

            BudgetManager budgetManager = new BudgetManager(dbHelper);
            TransactionManager transactionManager = new TransactionManager(dbHelper, budgetManager);
            ScenarioSimulator scenarioSimulator = new ScenarioSimulator(transactionManager, budgetManager);

            Application.Run(new MainForm(dbHelper, transactionManager, budgetManager, scenarioSimulator));
        }
    }
}
