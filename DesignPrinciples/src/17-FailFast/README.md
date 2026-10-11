# Fail Fast: Valid Order Intake

## Problem
An order intake API requires a customer name and a positive quantity. Invalid input should not become a misleading accepted order.

In a production system, this requirement will change as the business evolves. The design should make tightening required fields or enforcing a new invariant visible, local, and safe rather than allowing an unrelated workflow to become the change point.

## Bad Implementation
The bad code is centered on **OrderIntakeService.Accept replaces missing customers with a default and clamps negative quantity to zero.**

- The method or type has more responsibility than its stable role.
- `No // PROBLEM marker is present; silent normalization hides invalid caller input.`
- Policy and mechanics are coupled, so the behavior is hard to replace or verify in isolation.

## Why Is This a Problem?
- **What is wrong?** The code combines concerns or places a rule outside its proper owner.
- **What will happen when requirements change?** tightening required fields or enforcing a new invariant forces edits in existing workflow code and raises regression risk.
- **How does this affect testing?** Tests must work around hidden behavior or concrete collaborators instead of using focused fakes.
- **How does this affect maintainability?** It is difficult to identify a single owner for the rule, so changes take longer to locate and review.
- **How does this affect scalability?** More variants, callers, and integrations accumulate conditional logic or conflicting behavior.
- **What kind of change would force us to modify this code?** tightening required fields or enforcing a new invariant.

## Principle
### Principle
Fail Fast organizes code so related behavior has a clear owner and changes have a limited blast radius.

### In simple words
Give each rule a clear home. Depend on the capability you need, keep public behavior honest, and do not build flexibility before it is needed.

### When to use it
Use it when invalid input cannot produce a meaningful safe result. It is especially valuable at domain, infrastructure, and integration boundaries.

### When NOT to use it
Avoid throwing for optional inputs with a documented default. The goal is clarity and a cheaper change, not the maximum number of classes.

## Solution
OrderIntakeService validates inputs immediately and returns AcceptedOrder only for valid data. The high-level workflow remains readable while each collaborator owns a focused responsibility.

## Before vs After
| Aspect | Before | After |
|---|---|---|
| Change location | Existing workflow is edited for tightening required fields or enforcing a new invariant | The focused rule or collaborator changes locally |
| Dependencies | Policy and concrete mechanics are tangled | Collaboration is explicit and role-focused |
| Tests | Setup works around hidden behavior | A focused fake or direct unit test exercises the rule |
| Growth | New cases enlarge existing control flow | New behavior has an intentional extension or ownership point |

## Unit Tests
Tests verify valid input succeeds and missing customers and non-positive quantities throw immediately. They document observable behavior rather than implementation trivia, so a safe refactor can retain confidence.

## Real-World Usage
ASP.NET Core model validation and command handlers reject invalid requests before persistence.

## Interview Questions
1. **Beginner: What does Fail Fast mean?** It means placing related behavior behind a clear owner so a change has a small, understandable impact.
2. **Practical: What is the smell in this example?** `OrderIntakeService` makes a policy or integration decision outside the correct focused boundary.
3. **Scenario-based: How would you handle tightening required fields or enforcing a new invariant?** Change or add the focused collaborator or rule, then protect it with a targeted test instead of rewriting the whole workflow.
4. **Senior-level: How do you avoid over-design?** Add a seam only where variation, an external boundary, or a protected invariant is real; weigh it against the cost of indirection.
5. **When would you NOT use this principle?** Do not apply it mechanically when a direct, cohesive implementation is clearer and requirements are stable.

## Trade-offs
Applied excessively, Fail Fast can produce indirection, many small types, registration complexity, and longer navigation paths. Prefer the smallest design that clearly expresses the current change boundary; consolidate abstractions that do not buy independent evolution.

## Design Decision
- **Why did we introduce this abstraction?** We introduced guard clauses at the service boundary so the business rule has an explicit, testable home.
- **Why didn't we use another approach?** We did not retain coercion because it creates corrupt downstream state.
- **What would happen if the application became 10x larger?** invalid traffic fails cheaply before consuming workflow capacity.

## Refactoring Commit Simulation
Commit 1: `feat: create naive 17-FailFast implementation`  
Commit 2: `refactor: identify Fail Fast design problem`  
Commit 3: `refactor: apply Fail Fast to valid order intake`  
Commit 4: `test: cover focused 17-FailFast behavior`  
Commit 5: `docs: document Fail Fast trade-offs`
