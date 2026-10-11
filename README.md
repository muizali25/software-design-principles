# Design Principles in C#

A practical collection of software design principles demonstrated through real-world C# examples.

Built as a **Senior .NET Backend Engineer** portfolio project. Each example shows not only definitions, but **how and why** to apply a principle—and when not to.

## What this repository demonstrates

* SOLID
* Clean code thinking
* Maintainability
* Testability
* Separation of concerns
* Dependency management
* Object-oriented design
* Refactoring
* Architectural trade-offs

## Learning Format

Every example follows:

```text
Problem
  ↓
Bad Implementation
  ↓
Why It Fails
  ↓
Principle
  ↓
Refactoring
  ↓
Solution
  ↓
Tests
  ↓
Real-world Usage
  ↓
Trade-offs
```

## Table of Contents

### SOLID

| # | Principle | Domain example | Folder |
|---|-----------|----------------|--------|
| 01 | [Single Responsibility (SRP)](DesignPrinciples/src/01-SRP) | Order processing | `DesignPrinciples/src/01-SRP` |
| 02 | [Open/Closed (OCP)](DesignPrinciples/src/02-OCP) | Payment processing | `DesignPrinciples/src/02-OCP` |
| 03 | [Liskov Substitution (LSP)](DesignPrinciples/src/03-LSP) | Refund policies | `DesignPrinciples/src/03-LSP` |
| 04 | [Interface Segregation (ISP)](DesignPrinciples/src/04-ISP) | Notification channels | `DesignPrinciples/src/04-ISP` |
| 05 | [Dependency Inversion (DIP)](DesignPrinciples/src/05-DIP) | Order fulfillment | `DesignPrinciples/src/05-DIP` |

### Other design principles

| # | Principle | Domain example | Folder |
|---|-----------|----------------|--------|
| 06 | [DRY](DesignPrinciples/src/06-DRY) | Checkout & invoicing pricing | `DesignPrinciples/src/06-DRY` |
| 07 | [KISS](DesignPrinciples/src/07-KISS) | Discount calculation | `DesignPrinciples/src/07-KISS` |
| 08 | [YAGNI](DesignPrinciples/src/08-YAGNI) | Inventory stock management | `DesignPrinciples/src/08-YAGNI` |
| 09 | [Composition over Inheritance](DesignPrinciples/src/09-CompositionOverInheritance) | Report generation | `DesignPrinciples/src/09-CompositionOverInheritance` |
| 10 | [Program to an Interface](DesignPrinciples/src/10-ProgramToInterface) | Product catalog / orders | `DesignPrinciples/src/10-ProgramToInterface` |
| 11 | [Separation of Concerns](DesignPrinciples/src/11-SeparationOfConcerns) | Employee onboarding | `DesignPrinciples/src/11-SeparationOfConcerns` |
| 12 | [Encapsulation](DesignPrinciples/src/12-Encapsulation) | Bank account | `DesignPrinciples/src/12-Encapsulation` |
| 13 | [Favor Immutability](DesignPrinciples/src/13-Immutability) | Money & order lines | `DesignPrinciples/src/13-Immutability` |
| 14 | [Law of Demeter](DesignPrinciples/src/14-LawOfDemeter) | Shipping labels | `DesignPrinciples/src/14-LawOfDemeter` |
| 15 | [Tell, Don't Ask](DesignPrinciples/src/15-TellDontAsk) | Order shipping workflow | `DesignPrinciples/src/15-TellDontAsk` |
| 16 | [Convention over Configuration](DesignPrinciples/src/16-ConventionOverConfiguration) | Report formatters | `DesignPrinciples/src/16-ConventionOverConfiguration` |
| 17 | [Fail Fast](DesignPrinciples/src/17-FailFast) | Order intake validation | `DesignPrinciples/src/17-FailFast` |
| 18 | [Single Level of Abstraction](DesignPrinciples/src/18-SingleLevelOfAbstraction) | Payroll processing | `DesignPrinciples/src/18-SingleLevelOfAbstraction` |
| 19 | [Explicit Dependencies](DesignPrinciples/src/19-ExplicitDependencies) | Invoice generation | `DesignPrinciples/src/19-ExplicitDependencies` |
| 20 | [Don't Repeat Knowledge](DesignPrinciples/src/20-DontRepeatKnowledge) | Order status rules | `DesignPrinciples/src/20-DontRepeatKnowledge` |

## Documentation

* [Principles overview](DesignPrinciples/docs/principles.md)
* [Repository architecture](DesignPrinciples/docs/architecture.md)
* [Interview questions](DesignPrinciples/docs/interview-questions.md)
* [Interview cheatsheet](DesignPrinciples/docs/interview-cheatsheet.md)
* [Portfolio presentation guide](DesignPrinciples/docs/portfolio-guide.md)

## How to run

Requires the [.NET 9 SDK](https://dotnet.microsoft.com/download).

```powershell
cd DesignPrinciples
dotnet build DesignPrinciples.sln
dotnet test DesignPrinciples.sln

# Run a single example
dotnet run --project src/01-SRP/01-SRP.csproj
dotnet run --project src/02-OCP/02-OCP.csproj
```

Open `DesignPrinciples/DesignPrinciples.sln` in **Visual Studio 2022**, or use the CLI above.

## Project structure

```text
software-design-principles/
├── README.md
└── DesignPrinciples/
    ├── DesignPrinciples.sln
    ├── docs/
    ├── src/
    │   ├── 01-SRP/
    │   ├── 02-OCP/
    │   └── ...
    └── tests/
        ├── 01-SRP.Tests/
        └── ...
```

Each principle is an independent console app with:

* `Bad/` — realistic violation
* `Good/` — focused refactor
* `Program.cs` — short demo
* `README.md` — full design write-up
* Matching xUnit project under `tests/`

## Design philosophy

Principles are tools, not commandments.

* Prefer the **smallest design that makes the next change safer**
* Introduce abstractions at **real boundaries**
* Document **trade-offs** when a principle would create noise
* Prove behavior with **meaningful tests**, not coverage theater

## Tech stack

* C# / .NET 9
* Console applications
* xUnit
* Nullable reference types
* No external APIs, databases, or unnecessary frameworks
