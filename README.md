CsvEnt 
=========

## Overview
**CsvEnt** is a simple library to make working with csv easier. It helps bind files to entities and create files based on entities.

CsvEnt can be useful for these scenarios:
- bind csv file to entities
- write entities to file

Usage
------

## Binding
```csharp
class CsvBinder<T>()
```
Create binding rules.
```csharp
binder.AddRule(0, e => e.Int, BindMappers.Int);
```
Get specific file rows (By default get all).
```csharp
binder.Skip(1);
binder.Take(2);
```
Set data delimeter (By default ';').
```csharp
binder.WithDelimiter(',');
```
Set error behavior (By default false).
```csharp
binder.ContinueOrError(true);
```
If error behavior is true, then all errors placed to Errors prop.
```csharp
binder.ContinueOrError(true);
binder.Bind('file.csv');
List<CsvBindException> bindErrs = binder.Errors;
```
Bind file to entities
```csharp
var entities = binder.Bind("test.csv", 0);
```

#### Binding funcs
**BindMappers** class contains some basic mapping funcs:
- BindMappers.Int
- BindMappers.NullInt
- BindMappers.Double
- BindMappers.NullDouble
- BindMappers.Bool
- BindMappers.NullBool
- BindMappers.Date
- BindMappers.NullDate
- BindMappers.Enum *(get enum by string description attr)*
- BindMappers.String

Custom mapper example: func that take file column data and return new value.
```csharp
/// Func<T, object>
(col) => col.ToString().Split('#')[1]
```
#### Example
```csharp
/// Bind file 0, 2, 8 column, skip 1 row and take all. 
/// Continue on bind err.
var binder = new CsvBinder<TestItem>()
    .AddRule(0, e => e.Int, BindMappers.Int)
    .AddRule(2, e => e.Double, BindMappers.Double)
    .AddRule(8, e => e.String, BindMappers.String)
    .ContinueOrError(true)
    .Skip(1);
    
var entities = binder.Bind(Expected.TestItemsDataFile);
var bindErrs = binder.Errors;
```

## Writing
```csharp
class CsvWriter<T>
```
Add write rule. 
Rules order is important.
Call entity prop value ToString default or custom func when map entity to file.
```csharp
writer.AddRule(e => e.Int); // Call e.Int.ToString() when map to csv
writer.AddRule(e => e.Date, v => v.ToString("dd.MM.yyyy")); // Call custom func when map to csv
```
Add columns titles.
```csharp
writer.AddColumnsTitles("Int", "Date");
writer.AddColumnsTitles(new string[]
{
    "Int", 
    "Date"
});
```
Set data delimeter (By default ';').
```csharp
writer.WithDelimiter(',');
```
Write to file
```csharp
writer.Write(entities, filePath);
```
#### Example
```csharp
// Write
// Result row example 1;2.5;11.10.2020
new CsvWriter<TestItem>()
    .AddRule(e => e.Int)
    .AddRule(e => e.Double)
    .AddRule(e => e.Date, v => v.ToString("dd.MM.yyyy"))
    .WithDelimiter(';')
    .Write(data, testFilePath);
```
