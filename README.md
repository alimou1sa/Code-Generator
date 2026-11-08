# Code Generator

A small WinForms tool that reads a SQL Server database schema and writes the C# three-tier
boilerplate for it: one business class and one data-access class per table, plus the shared
connection-settings and error-log helpers.

![C#](https://img.shields.io/badge/C%23-.NET%20Framework%204.7.2-239120?logo=csharp&logoColor=white)
![WinForms](https://img.shields.io/badge/UI-WinForms-512BD4)
![SQL Server](https://img.shields.io/badge/Source-SQL%20Server%20catalog-CC2927?logo=microsoftsqlserver&logoColor=white)

## Why

Every table in a layered desktop app needs the same five methods: get all, get by id, insert,
update, delete. Writing them by hand is slow and, more importantly, inconsistent - one class
returns `DataTable`, the next one returns `List<T>`, one forgets `await`, another forgets to
dispose a connection.

This tool makes that layer generated, not typed: point it at a database, press **Generate**, and
get the same structure and the same ADO.NET patterns for every table.

## How it works

```
1. Connect      Database name + SQL credentials (optionally remembered)
2. Read schema  sys.tables, sys.columns, sys.types, sys.foreign_keys,
                INFORMATION_SCHEMA - table names, columns, data types, FK targets
3. Build model  clsTable / clsProperty / clsForeignProperty
4. Render       string templates per file
5. Write        {ProjectName}Layer\cls{Table}.cs
                {ProjectName}DataAccessLayer\cls{Table}Data.cs
                {ProjectName}DataAccessLayer\clsDataAccessSettings.cs
                {ProjectName}DataAccessLayer\clsErrorEventLog.cs
```

The generation time is measured with a `Stopwatch` and shown when it finishes, so the cost of
re-generating after a schema change is visible.

## What gets generated

For a table named `Orders`, the data-access class contains the standard five operations, all
parameterized:

```csharp
public static async Task<DataTable> GetAllOrders()
public static async Task<int> AddNewOrders(...)          // returns the new identity
public static async Task<bool> UpdateOrders(...)
public static bool GetOrdersInfoByID(int id, ref ...)    // fills the entity by id
public static async Task<bool> DeleteOrders(...)
```

The companion business class holds the properties, the `enMode` flag (`AddNew` / `Update`) and a
`Save()` that routes to the right data-access call. Column types from the catalog are mapped to
C# types; foreign keys are kept so the generated class can expose the related id.

## Project structure

| Project | Role |
| --- | --- |
| `Code_Generator` | WinForms UI: login/connection form, generate button, status output |
| `CodeGenerator_BusinessLayer` | Schema model (`clsTable`, `clsProperty`), template rendering, `Generate()` |
| `CodeGenerater_DataAcessLayer` | Reads `sys` catalog views, writes files (`clsData.CreatFileCs`) |

## Getting started

1. Open `Code_Generator/Code_Generator.csproj` in Visual Studio (needs .NET Framework 4.7.2).
2. Run the app, enter the database name and SQL credentials, press **Generate**.
3. Generated code is written next to the output path configured in `clsGenerator.Path`.

## Limitations

- The output path is hard-coded (`C:\Users\...\source\C#\`) and should be a folder picker.
- Only SQL Server is supported, because the schema queries use `sys.*` catalog views.
- The templates produce the data/business layer only - no controllers or migrations.
- No tests; the generated output is verified by compiling it.

## Next steps

- Replace the hard-coded path with a folder browser dialog.
- Let the user pick which tables to generate.
- Add a "regenerate and diff" mode so existing files are updated instead of overwritten.
