# Design Principles in C#

A practical collection of software design principles demonstrated through real-world C# examples.

This repository is built for a **Senior .NET Backend Engineer** portfolio. It shows not only definitions, but **how and why** to apply each principle—and when not to.

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
| 01 | [Single Responsibility (SRP)](src/01-SRP) | Order processing | `src/01-SRP` |
| 02 | [Open/Closed (OCP)](src/02-OCP) | Payment processing | `src/02-OCP` |
| 03 | [Liskov Substitution (LSP)](src/03-LSP) | Refund policies | `src/03-LSP` |
| 04 | [Interface Segregation (ISP)](src/04-ISP) | Notification channels | `src/04-ISP` |
| 05 | [Dependency Inversion (DIP)](src/05-DIP) | Order fulfillment | `src/05-DIP` |

### Other design principles

| # | Principle | Domain example | Folder |
|---|-----------|----------------|--------|
| 06 | [DRY](src/06-DRY) | Checkout & invoicing pricing | `src/06-DRY` |
| 07 | [KISS](src/07-KISS) | Discount calculation | `src/07-KISS` |
| 08 | [YAGNI](src/08-YAGNI) | Inventory stock management | `src/08-YAGNI` |
| 09 | [Composition over Inheritance](src/09-CompositionOverInheritance) | Report generation | `src/09-CompositionOverInheritance` |
| 10 | [Program to an Interface](src/10-ProgramToInterface) | Product catalog / orders | `src/10-ProgramToInterface` |
| 11 | [Separation of Concerns](src/11-SeparationOfConcerns) | Employee onboarding | `src/11-SeparationOfConcerns` |
| 12 | [Encapsulation](src/12-Encapsulation) | Bank account | `src/12-Encapsulation` |
| 13 | [Favor Immutability](src/13-Immutability) | Money & order lines | `src/13-Immutability` |
| 14 | [Law of Demeter](src/14-LawOfDemeter) | Shipping labels | `src/14-LawOfDemeter` |
| 15 | [Tell, Don't Ask](src/15-TellDontAsk) | Order shipping workflow | `src/15-TellDontAsk` |
| 16 | [Convention over Configuration](src/16-ConventionOverConfiguration) | Report formatters | `src/16-ConventionOverConfiguration` |
| 17 | [Fail Fast](src/17-FailFast) | Order intake validation | `src/17-FailFast` |
| 18 | [Single Level of Abstraction](src/18-SingleLevelOfAbstraction) | Payroll processing | `src/18-SingleLevelOfAbstraction` |
| 19 | [Explicit Dependencies](src/19-ExplicitDependencies) | Invoice generation | `src/19-ExplicitDependencies` |
| 20 | [Don't Repeat Knowledge](src/20-DontRepeatKnowledge) | Order status rules | `src/20-DontRepeatKnowledge` |

## Documentation

* [Principles overview](docs/principles.md)
* [Repository architecture](docs/architecture.md)
* [Interview questions](docs/interview-questions.md)
* [Interview cheatsheet](docs/interview-cheatsheet.md)
* [Portfolio presentation guide](docs/portfolio-guide.md)

## How to run

Requires the [.NET 9 SDK](https://dotnet.microsoft.com/download).

```powershell
# From the DesignPrinciples folder
dotnet build DesignPrinciples.sln
dotnet test DesignPrinciples.sln

# Run a single example
dotnet run --project src/01-SRP/01-SRP.csproj
dotnet run --project src/02-OCP/02-OCP.csproj
```

Open the solution in **Visual Studio 2022** (`DesignPrinciples.sln`) or use the CLI above.

## Project structure

```text
DesignPrinciples/
├── README.md
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
