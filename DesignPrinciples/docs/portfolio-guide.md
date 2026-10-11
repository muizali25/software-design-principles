# Portfolio Presentation Guide

Use this repository in interviews as evidence of **design judgment**, not memorization.

## Why did you create this repository?

> I wanted a portable set of examples that show how I reason about change. Each principle has a realistic bad design, a focused refactor, tests, and an explicit trade-off section. That mirrors how I approach production code reviews.

## What is your favorite example?

Suggested favorites by interviewer focus:

| Interviewer focus | Example |
|-------------------|---------|
| SOLID / payments | `02-OCP` |
| Dependency injection | `05-DIP` or `19-ExplicitDependencies` |
| Domain modeling | `15-TellDontAsk` or `20-DontRepeatKnowledge` |
| Over-engineering | `07-KISS` or `08-YAGNI` |
| Refactoring story | `01-SRP` |

Walkthrough pattern:

1. State the business requirement in one sentence.
2. Show the bad code and name the change that would hurt.
3. Show the good design and the test that protects it.
4. End with the trade-off: when you would *not* add more abstraction.

## Show me a principle you would NOT blindly apply

Strong answers:

* **Program to an Interface** — do not create `IOrderTotalCalculator` if there is only one implementation and no boundary.
* **OCP** — do not invent a plugin system for two stable discount rules.
* **Composition over Inheritance** — a simple sealed hierarchy can be clearer than five tiny collaborators.
* **Convention over Configuration** — hide magic only when the convention is widely understood and exceptions are rare.

## Give me an example where abstraction made the code worse

Use `07-KISS` or `08-YAGNI`:

> The bad discount engine introduced Strategy, Factory, and an event bus for a percentage discount. The abstraction increased navigation cost without buying a real variation point. The good version is a small calculator with tests—enough for the current requirement.

## How does this relate to production systems?

Map examples to real change:

| Principle | Production change |
|-----------|-------------------|
| OCP | Adding Apple Pay without rewriting checkout |
| DIP / Explicit Dependencies | Swapping SMTP for SendGrid behind `IEmailSender` |
| Fail Fast | Rejecting invalid messages at a queue consumer boundary |
| Don't Repeat Knowledge | One `OrderStatus` model used by API, workers, and UI |
| Tell, Don't Ask | Preventing illegal shipment transitions in domain logic |

Emphasize outcomes: smaller PRs, clearer failures, safer tests, fewer inconsistent rules.

## Suggested 5-minute demo script

1. Clone / open `DesignPrinciples.sln`.
2. Run `dotnet test`.
3. Open `src/02-OCP` Bad vs Good.
4. Run `dotnet run --project src/02-OCP/02-OCP.csproj`.
5. Mention Trade-offs from the README.
6. Offer a second example if they care about DI or domain modeling.

## Closing statement

> I use principles to make the next change safer. I start with the smallest design that expresses the current boundary, verify it with tests, and accept additional abstraction only when it buys a clear capability.
