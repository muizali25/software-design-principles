# Cross-Cutting Interview Questions

Concise model answers drawn from the examples in this repository.

## Fundamentals

**Q: What is the difference between a design principle and a design pattern?**  
A: A principle is a guideline for managing change and coupling (for example, SRP). A pattern is a reusable structure that may help apply a principle (for example, Strategy for OCP). Patterns are tools; principles explain *why* the tool helps.

**Q: Which SOLID principle is most often misunderstood?**  
A: SRP. It does not mean “one method per class.” It means one cohesive reason to change. An orchestrator that coordinates focused collaborators can still respect SRP.

**Q: Are SOLID principles absolute?**  
A: No. Apply them when they reduce the cost of a real change. Blind interface creation and premature plugin frameworks are common senior-level smells.

## Scenario questions

**Q: Checkout needs a new payment provider next sprint. Which principles matter first?**  
A: OCP (add a processor without rewriting existing ones), Program to an Interface / DIP (depend on `IPaymentProcessor`), and Explicit Dependencies (inject the registry or processors). See `02-OCP` and `05-DIP`.

**Q: Two services calculate tax differently and customers see mismatched totals. What went wrong?**  
A: Knowledge was repeated. DRY alone is not enough—Don't Repeat Knowledge says the business rule needs one authoritative owner. See `06-DRY` and `20-DontRepeatKnowledge`.

**Q: A subclass throws `NotSupportedException` for a base method clients always call. What principle is broken?**  
A: LSP. Clients of the base type can no longer substitute the subtype safely. See `03-LSP`.

**Q: A service constructs `new SmtpClient()` and `ServiceLocator.Get<IRepo>()` inside methods. Why is that risky?**  
A: Dependencies are implicit, hard to test, and tied to ambient runtime state. Prefer constructor injection. See `19-ExplicitDependencies` and `05-DIP`.

**Q: An API silently turns null customer IDs into `"guest"` and negative quantities into `1`. What do you recommend?**  
A: Fail Fast at the intake boundary with clear exceptions or validation results. Silent coercion creates corrupt orders that fail later in fulfillment. See `17-FailFast`.

## Senior judgment

**Q: When is creating an interface the wrong move?**  
A: When there is one stable implementation, no external boundary, and no meaningful test seam. Interfaces should mark variation or ownership boundaries, not decorate every class.

**Q: How do you decide between inheritance and composition?**  
A: Prefer composition when behaviors vary independently (formatting vs exporting). Inheritance is fine for a narrow, stable “is-a” relationship with an honest contract. See `09-CompositionOverInheritance`.

**Q: How do you keep principles from producing over-engineering?**  
A: Start from the change you expect next. If the abstraction does not make that change safer or clearer, delete it. KISS and YAGNI are the counterweights to SOLID enthusiasm.

**Q: How would you explain Tell, Don't Ask vs anemic models?**  
A: Asking for flags and mutating fields externally spreads invariants across services. Telling the aggregate to `Ship()` keeps rules next to state. See `15-TellDontAsk`.

## Portfolio meta-questions

**Q: Why show bad code in a portfolio?**  
A: Senior engineers diagnose designs, not only write happy-path samples. Showing a realistic violation and a measured refactor demonstrates review skill.

**Q: What would you improve next in this repository?**  
A: Possible honest answers: add mutation testing for domain examples, show one cross-principle composition case study, or add benchmark notes where immutability allocations matter.

## Quick practice set

1. Explain DIP without saying “dependency injection.”
2. Give a case where DRY made code worse.
3. Refactor a Law of Demeter chain out loud.
4. Name a production bug Fail Fast would have prevented.
5. Defend why YAGNI sometimes rejects a “future-proof” design.
