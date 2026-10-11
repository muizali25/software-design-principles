# Single Responsibility Principle: Order Processing Responsibilities

## Problem
An online store must calculate an order total, save it, email a receipt, and produce an invoice. These concerns have different owners and change schedules.

In a production system, this requirement will change as the business evolves. The design should make a tax rule, database provider, email template, or invoice format visible, local, and safe rather than allowing an unrelated workflow to become the change point.

## Bad Implementation
The bad code is centered on **OrderService.Process calculates pricing, persists output, emails, and invoices in one method.**

- The method or type has more responsibility than its stable role.
- `// PROBLEM: pricing, persistence, email, and invoicing change for unrelated reasons.`
- Policy and mechanics are coupled, so the behavior is hard to replace or verify in isolation.

## Why Is This a Problem?
- **What is wrong?** The code combines concerns or places a rule outside its proper owner.
- **What will happen when requirements change?** a tax rule, database provider, email template, or invoice format forces edits in existing workflow code and raises regression risk.
- **How does this affect testing?** Tests must work around hidden behavior or concrete collaborators instead of using focused fakes.
- **How does this affect maintainability?** It is difficult to identify a single owner for the rule, so changes take longer to locate and review.
- **How does this affect scalability?** More variants, callers, and integrations accumulate conditional logic or conflicting behavior.
- **What kind of change would force us to modify this code?** a tax rule, database provider, email template, or invoice format.

## Principle
### Principle
Single Responsibility Principle organizes code so related behavior has a clear owner and changes have a limited blast radius.

### In simple words
Give each rule a clear home. Depend on the capability you need, keep public behavior honest, and do not build flexibility before it is needed.

### When to use it
Use it when a class has unrelated business reasons to change. It is especially valuable at domain, infrastructure, and integration boundaries.

### When NOT to use it
Avoid splitting a tiny cohesive value object into many one-method services. The goal is clarity and a cheaper change, not the maximum number of classes.

## Solution
OrderTotalCalculator owns arithmetic while OrderProcessor coordinates IOrderRepository, IReceiptSender, and IInvoiceGenerator. The high-level workflow remains readable while each collaborator owns a focused responsibility.

## Before vs After
| Aspect | Before | After |
|---|---|---|
| Change location | Existing workflow is edited for a tax rule, database provider, email template, or invoice format | The focused rule or collaborator changes locally |
| Dependencies | Policy and concrete mechanics are tangled | Collaboration is explicit and role-focused |
| Tests | Setup works around hidden behavior | A focused fake or direct unit test exercises the rule |
| Growth | New cases enlarge existing control flow | New behavior has an intentional extension or ownership point |

## Unit Tests
Tests verify totals and prove Process uses each injected collaborator. They document observable behavior rather than implementation trivia, so a safe refactor can retain confidence.

## Real-World Usage
ASP.NET Core order handlers commonly orchestrate focused application services, repositories, mail gateways, and document generators.

## Interview Questions
1. **Beginner: What does Single Responsibility Principle mean?** It means placing related behavior behind a clear owner so a change has a small, understandable impact.
2. **Practical: What is the smell in this example?** `OrderService` makes a policy or integration decision outside the correct focused boundary.
3. **Scenario-based: How would you handle a tax rule, database provider, email template, or invoice format?** Change or add the focused collaborator or rule, then protect it with a targeted test instead of rewriting the whole workflow.
4. **Senior-level: How do you avoid over-design?** Add a seam only where variation, an external boundary, or a protected invariant is real; weigh it against the cost of indirection.
5. **When would you NOT use this principle?** Do not apply it mechanically when a direct, cohesive implementation is clearer and requirements are stable.

## Trade-offs
Applied excessively, Single Responsibility Principle can produce indirection, many small types, registration complexity, and longer navigation paths. Prefer the smallest design that clearly expresses the current change boundary; consolidate abstractions that do not buy independent evolution.

## Design Decision
- **Why did we introduce this abstraction?** We introduced focused collaborators for pricing, storage, receipt delivery, and invoicing so the business rule has an explicit, testable home.
- **Why didn't we use another approach?** We did not retain a single service with helpers because change boundaries would still be mixed.
- **What would happen if the application became 10x larger?** separate teams can evolve infrastructure and policy independently.

## Refactoring Commit Simulation
Commit 1: `feat: create naive 01-SRP implementation`  
Commit 2: `refactor: identify Single Responsibility Principle design problem`  
Commit 3: `refactor: apply Single Responsibility Principle to order processing responsibilities`  
Commit 4: `test: cover focused 01-SRP behavior`  
Commit 5: `docs: document Single Responsibility Principle trade-offs`
