# BudgetBuddy – Database Integration Guide

This guide is for the group member(s) responsible for implementing the database for **BudgetBuddy**.

The current project is already uploaded to GitHub and the UI/system logic is considered complete. The database implementation should be added without unnecessarily changing the existing UI, calculations, validation, navigation, or business rules.

## Repository

Repository:

`https://github.com/dbgooomr/BudgetBuddy`

## Current Project Status

The current application uses an in-memory data store.

The project already contains:

- Windows Forms UI
- Forms and Designer files
- Business logic
- Models
- Services / managers
- Repository abstraction
- `DatabaseHelper`
- `InMemoryDataStore`
- SQLite schema blueprint

The remaining task is to implement the actual SQLite persistence layer.

## Important Rule

Do **not** work directly on the `main` branch.

Create a separate branch for the database work first.

Recommended branch name:

`database-sqlite`

## Step 1 – Clone the Repository

Open a terminal and run:

```bash
git clone https://github.com/dbgooomr/BudgetBuddy.git
```

Then enter the project folder:

```bash
cd BudgetBuddy
```

## Step 2 – Create the Database Branch

Run:

```bash
git checkout -b database-sqlite
```

Confirm the branch:

```bash
git branch
```

The active branch should be:

```text
* database-sqlite
  main
```

## Step 3 – Open the Project

Open:

`BudgetBuddy.sln`

in Visual Studio.

Before making database changes, make sure the current project builds and runs successfully.

## Step 4 – Implement SQLite

The database work should primarily stay inside the data/repository layer.

Files or folders that may need to be modified include:

```text
Data/
Interfaces/
DatabaseHelper.cs
Program.cs
BudgetBuddy.csproj
```

Additional database/repository classes may be created when necessary.

Examples:

```text
SqliteDataStore.cs
SqliteRepository.cs
DatabaseInitializer.cs
```

Use the existing project structure and interfaces where possible.

## Expected Database Tables

The current documentation defines the following planned SQLite tables:

- Categories
- Transactions
- Budgets
- AllowanceCycles
- SavingsGoals
- SavingsDeposits

The implementation should follow the existing project documentation and models.

## Important: Preserve Existing Logic

Do not unnecessarily modify:

- Safe to Spend calculations
- allowance-cycle calculations
- carry-over logic
- category-budget calculations
- savings calculations
- What-If calculations
- validation rules
- transaction behavior
- navigation
- UI control names
- event handlers
- Designer layout

The goal is to replace in-memory persistence with SQLite persistence, not redesign or rewrite the application.

## UI Files

Avoid editing the following unless database integration absolutely requires it:

```text
Forms/
Controls/
*.Designer.cs
```

The UI has already been prepared to work with the Visual Studio Windows Forms Designer.

Do not recreate or redesign controls as part of the database task.

## Program.cs

The current application may create or use `InMemoryDataStore`.

When the SQLite implementation is ready, `Program.cs` may need to be updated so the application uses the SQLite-backed repository instead.

Example concept:

```text
Current:
Forms
  ↓
Managers / Services
  ↓
DatabaseHelper
  ↓
InMemoryDataStore

Target:
Forms
  ↓
Managers / Services
  ↓
DatabaseHelper
  ↓
SQLite-backed repository
  ↓
SQLite database file
```

The Forms should not directly execute SQL.

## Database Initialization

The application should be able to create the required SQLite database/tables when necessary.

Avoid requiring group members or users to manually create tables before running the application.

If the database file does not exist, the application should initialize it safely.

## Persistence Requirement

After SQLite is implemented:

1. Add or edit data.
2. Close the application.
3. Open the application again.
4. The saved data should still exist.

This is the main difference from the current in-memory implementation.

## Before Committing

Test the following:

- Solution builds successfully.
- Application starts normally.
- Existing UI still works.
- Income records can be saved.
- Expense records can be saved.
- Categories persist.
- Budgets persist.
- Allowance cycles persist.
- Savings goals persist.
- Savings deposits persist.
- Editing works.
- Deleting works.
- Data remains after restarting the application.
- Dashboard calculations still work.
- Safe to Spend behavior is unchanged.
- What-If scenarios still do not save data.

## Step 5 – Check Git Changes

Run:

```bash
git status
```

Review the files carefully.

Make sure there are no accidental UI/Designer changes.

## Step 6 – Commit the Database Work

Stage all intended changes:

```bash
git add -A
```

Commit:

```bash
git commit -m "Implement SQLite database persistence"
```

## Step 7 – Push the Branch

Run:

```bash
git push -u origin database-sqlite
```

Do not force-push to `main`.

## Step 8 – Create a Pull Request

On GitHub, create a Pull Request:

```text
database-sqlite → main
```

The Pull Request should briefly explain:

- what database implementation was added
- what files were changed
- how the database is initialized
- where the database file is stored
- whether any NuGet package was added
- how persistence was tested

## Step 9 – Review Before Merge

Before merging into `main`, confirm:

- the project builds
- the UI is unchanged
- no Designer files were accidentally broken
- existing calculations still behave the same
- SQLite persistence works correctly
- no unnecessary refactoring was introduced

Only merge after testing.

## If Multiple Members Work on the Database

Each member may create their own branch, for example:

```text
database-schema
database-repository
database-testing
```

Then merge those changes into `database-sqlite` first.

After the complete database implementation is tested, create one Pull Request from:

```text
database-sqlite → main
```

## Git Safety Rules

Do not commit generated Visual Studio/build folders such as:

```text
.vs/
bin/
obj/
```

Do not commit personal Visual Studio user files such as:

```text
*.user
*.suo
```

Do not delete or replace the `.git` folder.

Do not commit passwords, API keys, or other secrets.

## Final Goal

The final `main` branch should contain:

- the completed BudgetBuddy UI
- the existing business logic
- the existing calculations and validation
- a working SQLite database implementation
- persistent records after application restart
- a project that builds and runs normally in Visual Studio

---

**Please preserve the existing system behavior. The database task is an integration/persistence task, not a UI redesign or business-logic rewrite.**
