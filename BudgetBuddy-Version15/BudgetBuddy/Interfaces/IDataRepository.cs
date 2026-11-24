using System;
using System.Collections.Generic;
using BudgetBuddy.Models;

namespace BudgetBuddy.Interfaces
{
    /// <summary>
    /// Abstraction for data persistence operations.
    /// Implemented by InMemoryDataStore for the prototype and ready for SqliteDatabaseRepository.
    /// </summary>
    public interface IDataRepository
    {
        // Categories
        List<Category> GetAllCategories();
        Category GetCategoryById(int categoryId);
        Category AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int categoryId);
        int CountTransactionsByCategory(int categoryId);
        void ReassignTransactionCategory(int oldCategoryId, int newCategoryId);

        // Transactions
        List<Transaction> GetAllTransactions();
        Transaction GetTransactionById(int transactionId);
        Transaction AddTransaction(Transaction transaction);
        void UpdateTransaction(Transaction transaction);
        void DeleteTransaction(int transactionId);

        // Budgets
        List<Budget> GetBudgetsForMonth(int month, int year);
        Budget GetBudget(int categoryId, int month, int year);
        Budget SaveOrUpdateBudget(Budget budget);
        void DeleteBudget(int budgetId);

        // Allowance Cycles
        List<AllowanceCycle> GetAllCycles();
        AllowanceCycle GetCurrentCycle();
        AllowanceCycle AddCycle(AllowanceCycle cycle);
        void UpdateCycle(AllowanceCycle cycle);

        // Savings Goals & Deposits
        List<SavingsGoal> GetAllSavingsGoals();
        SavingsGoal GetSavingsGoalById(int goalId);
        SavingsGoal AddSavingsGoal(SavingsGoal goal);
        void UpdateSavingsGoal(SavingsGoal goal);
        void DeleteSavingsGoal(int goalId);

        List<SavingsDeposit> GetDepositsForGoal(int goalId);
        SavingsDeposit GetDepositByTransactionId(int transactionId);
        SavingsDeposit AddSavingsDeposit(SavingsDeposit deposit);
        void DeleteSavingsDeposit(int depositId);

        // Demo / Utility
        void ResetSampleData();
    }
}
