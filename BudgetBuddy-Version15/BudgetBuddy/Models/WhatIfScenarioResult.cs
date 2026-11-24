using System;

namespace BudgetBuddy.Models
{
    /// <summary>
    /// Holds computed simulation results from ScenarioSimulator without modifying saved records.
    /// </summary>
    public class WhatIfScenarioResult
    {
        public string ScenarioType { get; set; }
        public decimal TotalPlannedAmount { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal NewBalance { get; set; }
        public decimal CurrentSafeDailyAmount { get; set; }
        public decimal NewSafeDailyAmount { get; set; }
        public string CategoryName { get; set; }
        public string BudgetStatusAfter { get; set; }
        public string DateLabel { get; set; }
        public string EstimatedDateText { get; set; }
        public string AdviceSummary { get; set; }
    }
}
