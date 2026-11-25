using System;
using System.Collections.Generic;
using BudgetBuddy.Interfaces;
using BudgetBuddy.Models;

namespace BudgetBuddy.Data
{
    public class DatabaseHelper
    {
        private readonly IDataRepository _repository;

        public const string SqliteSchemaScript = @"
CREATE TABLE IF NOT EXISTS Categories (
    CategoryId INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL UNIQUE,
    IsPreset INTEGER NOT NULL DEFAULT 0
);
CREATE TABLE IF NOT EXISTS Transactions (
    TransactionId INTEGER PRIMARY KEY AUTOINCREMENT,
    TransactionType TEXT NOT NULL,
    Amount INTEGER NOT NULL CHECK (Amount > 0),
    TransactionDate TEXT NOT NULL,
    CategoryId INTEGER NULL REFERENCES Categories(CategoryId),
    Notes TEXT,
    Source TEXT,
    PaymentMethod TEXT,
    ExpenseKind TEXT
);
CREATE TABLE IF NOT EXISTS Budgets (
    BudgetId INTEGER PRIMARY KEY AUTOINCREMENT,
    CategoryId INTEGER NOT NULL REFERENCES Categories(CategoryId),
    Month INTEGER NOT NULL CHECK (Month BETWEEN 1 AND 12),
    Year INTEGER NOT NULL,
    PeriodType TEXT NOT NULL DEFAULT 'Monthly' CHECK (PeriodType IN ('Weekly', 'Monthly')),
    PeriodStart TEXT NOT NULL,
    PeriodEnd TEXT NOT NULL,
    LimitAmount INTEGER NOT NULL CHECK (LimitAmount > 0),
    UNIQUE(CategoryId, PeriodType, PeriodStart, PeriodEnd)
);
CREATE TABLE IF NOT EXISTS AllowanceCycles (
    CycleId INTEGER PRIMARY KEY AUTOINCREMENT,
    StartDate TEXT NOT NULL,
    EndDate TEXT NOT NULL,
    AllowanceAmount INTEGER NOT NULL CHECK (AllowanceAmount > 0),
    CarryOver INTEGER NOT NULL DEFAULT 0
);
CREATE TABLE IF NOT EXISTS SavingsGoals (
    GoalId INTEGER PRIMARY KEY AUTOINCREMENT,
    GoalName TEXT NOT NULL,
    TargetAmount INTEGER NOT NULL CHECK (TargetAmount > 0),
    SavedAmount INTEGER NOT NULL DEFAULT 0 CHECK (SavedAmount >= 0),
    TargetDate TEXT
);
CREATE TABLE IF NOT EXISTS SavingsDeposits (
    DepositId INTEGER PRIMARY KEY AUTOINCREMENT,
    GoalId INTEGER NOT NULL REFERENCES SavingsGoals(GoalId),
    TransactionId INTEGER NOT NULL REFERENCES Transactions(TransactionId),
    Amount INTEGER NOT NULL CHECK (Amount > 0),
    DepositDate TEXT NOT NULL,
    IsExtraAllowance INTEGER NOT NULL DEFAULT 0, -- 1 = ignored by Safe to Spend Today
    Notes TEXT
);";

        public DatabaseHelper(IDataRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public static long ToCentavos(decimal pesos) => (long)Math.Round(pesos * 100m);
        public static decimal FromCentavos(long centavos) => Math.Round(centavos / 100m, 2);

        public List<Category> GetAllCategories() => _repository.GetAllCategories();
        public Category GetCategoryById(int id) => _repository.GetCategoryById(id);
        public Category AddCategory(Category category) => _repository.AddCategory(category);
        public void UpdateCategory(Category category) => _repository.UpdateCategory(category);
        public void DeleteCategory(int categoryId) => _repository.DeleteCategory(categoryId);
        public int CountTransactionsByCategory(int categoryId) => _repository.CountTransactionsByCategory(categoryId);
        public void ReassignTransactionCategory(int oldCategoryId, int newCategoryId) =>
            _repository.ReassignTransactionCategory(oldCategoryId, newCategoryId);

        public List<Transaction> GetAllTransactions() => _repository.GetAllTransactions();
        public Transaction GetTransactionById(int id) => _repository.GetTransactionById(id);
        public Transaction AddTransaction(Transaction tx) => _repository.AddTransaction(tx);
        public void UpdateTransaction(Transaction tx) => _repository.UpdateTransaction(tx);
        public void DeleteTransaction(int id) => _repository.DeleteTransaction(id);

        public List<Budget> GetBudgetsForMonth(int month, int year) => _repository.GetBudgetsForMonth(month, year);
        public Budget GetBudget(int categoryId, int month, int year) => _repository.GetBudget(categoryId, month, year);
        public Budget SaveOrUpdateBudget(Budget budget) => _repository.SaveOrUpdateBudget(budget);
        public void DeleteBudget(int budgetId) => _repository.DeleteBudget(budgetId);

        public List<AllowanceCycle> GetAllCycles() => _repository.GetAllCycles();
        public AllowanceCycle GetCurrentCycle() => _repository.GetCurrentCycle();
        public AllowanceCycle AddCycle(AllowanceCycle cycle) => _repository.AddCycle(cycle);
        public void UpdateCycle(AllowanceCycle cycle) => _repository.UpdateCycle(cycle);

        public List<SavingsGoal> GetAllSavingsGoals() => _repository.GetAllSavingsGoals();
        public SavingsGoal GetSavingsGoalById(int goalId) => _repository.GetSavingsGoalById(goalId);
        public SavingsGoal AddSavingsGoal(SavingsGoal goal) => _repository.AddSavingsGoal(goal);
        public void UpdateSavingsGoal(SavingsGoal goal) => _repository.UpdateSavingsGoal(goal);
        public void DeleteSavingsGoal(int goalId) => _repository.DeleteSavingsGoal(goalId);

        public List<SavingsDeposit> GetDepositsForGoal(int goalId) => _repository.GetDepositsForGoal(goalId);
        public SavingsDeposit GetDepositByTransactionId(int txId) => _repository.GetDepositByTransactionId(txId);
        public SavingsDeposit AddSavingsDeposit(SavingsDeposit deposit) => _repository.AddSavingsDeposit(deposit);
        public void DeleteSavingsDeposit(int depositId) => _repository.DeleteSavingsDeposit(depositId);

        public void ResetSampleData() => _repository.ResetSampleData();
    }
}
