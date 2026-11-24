using System;
using System.Windows.Forms;
using BudgetBuddy.Data;
using BudgetBuddy.Forms;
using BudgetBuddy.Services;

namespace BudgetBuddy
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the BudgetBuddy application.
        /// Wires up the data layer, business service managers, and main UI shell.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // 1. Initialize Data Access Layer (in-memory store ready for SQLite swap)
            InMemoryDataStore dataStore = new InMemoryDataStore();
            DatabaseHelper dbHelper = new DatabaseHelper(dataStore);

            // 2. Initialize Business Logic Managers
            BudgetManager budgetManager = new BudgetManager(dbHelper);
            TransactionManager transactionManager = new TransactionManager(dbHelper, budgetManager);
            ScenarioSimulator scenarioSimulator = new ScenarioSimulator(transactionManager, budgetManager);

            // 3. Launch Main Application Shell
            Application.Run(new MainForm(dbHelper, transactionManager, budgetManager, scenarioSimulator));
        }
    }
}
