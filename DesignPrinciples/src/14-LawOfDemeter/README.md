# Law of Demeter: Focused Shipping Labels

## Problem
A shipping label needs an order number and readable destination. The printer should not understand every internal object between order and address.

In a production system, this requirement will change as the business evolves. The design should make restructuring customer or address internals visible, local, and safe rather than allowing an unrelated workflow to become the change point.

## Bad Implementation
The bad code is centered on **ShippingLabelPrinter.Print navigates order.Customer.Address.City and Zip directly.**

- The method or type has more responsibility than its stable role.
- `// PROBLEM: The printer knows the internal navigation of Order, Customer, and Address.`
- Policy and mechanics are coupled, so the behavior is hard to replace or verify in isolation.

## Why Is This a Problem?
- **What is wrong?** The code combines concerns or places a rule outside its proper owner.
- **What will happen when requirements change?** restructuring customer or address internals forces edits in existing workflow code and raises regression risk.
- **How does this affect testing?** Tests must work around hidden behavior or concrete collaborators instead of using focused fakes.
- **How does this affect maintainability?** It is difficult to identify a single owner for the rule, so changes take longer to locate and review.
- **How does this affect scalability?** More variants, callers, and integrations accumulate conditional logic or conflicting behavior.
- **What kind of change would force us to modify this code?** restructuring customer or address internals.

## Principle
### Principle
Law of Demeter organizes code so related behavior has a clear owner and changes have a limited blast radius.

### In simple words
Give each rule a clear home. Depend on the capability you need, keep public behavior honest, and do not build flexibility before it is needed.

### When to use it
Use it when a client traverses several objects just to complete one task. It is especially valuable at domain, infrastructure, and integration boundaries.

### When NOT to use it
Avoid adding forwarding methods for every property when a DTO is the true boundary. The goal is clarity and a cheaper change, not the maximum number of classes.

## Solution
Order exposes GetShippingDestination and ShippingAddress owns Format; the printer asks direct collaborators. The high-level workflow remains readable while each collaborator owns a focused responsibility.

## Before vs After
| Aspect | Before | After |
|---|---|---|
| Change location | Existing workflow is edited for restructuring customer or address internals | The focused rule or collaborator changes locally |
| Dependencies | Policy and concrete mechanics are tangled | Collaboration is explicit and role-focused |
| Tests | Setup works around hidden behavior | A focused fake or direct unit test exercises the rule |
| Growth | New cases enlarge existing control flow | New behavior has an intentional extension or ownership point |

## Unit Tests
Tests verify the printer produces a label through the order destination API. They document observable behavior rather than implementation trivia, so a safe refactor can retain confidence.

## Real-World Usage
Controllers and handlers should call domain methods instead of traversing deep ORM graphs.

## Interview Questions
1. **Beginner: What does Law of Demeter mean?** It means placing related behavior behind a clear owner so a change has a small, understandable impact.
2. **Practical: What is the smell in this example?** `ShippingLabelPrinter` makes a policy or integration decision outside the correct focused boundary.
3. **Scenario-based: How would you handle restructuring customer or address internals?** Change or add the focused collaborator or rule, then protect it with a targeted test instead of rewriting the whole workflow.
4. **Senior-level: How do you avoid over-design?** Add a seam only where variation, an external boundary, or a protected invariant is real; weigh it against the cost of indirection.
5. **When would you NOT use this principle?** Do not apply it mechanically when a direct, cohesive implementation is clearer and requirements are stable.

## Trade-offs
Applied excessively, Law of Demeter can produce indirection, many small types, registration complexity, and longer navigation paths. Prefer the smallest design that clearly expresses the current change boundary; consolidate abstractions that do not buy independent evolution.

## Design Decision
- **Why did we introduce this abstraction?** We introduced Order destination behavior and ShippingAddress formatting so the business rule has an explicit, testable home.
- **Why didn't we use another approach?** We did not retain continued navigation because internal model changes leak outward.
- **What would happen if the application became 10x larger?** aggregates can evolve without changing label consumers.

## Refactoring Commit Simulation
Commit 1: `feat: create naive 14-LawOfDemeter implementation`  
Commit 2: `refactor: identify Law of Demeter design problem`  
Commit 3: `refactor: apply Law of Demeter to focused shipping labels`  
Commit 4: `test: cover focused 14-LawOfDemeter behavior`  
Commit 5: `docs: document Law of Demeter trade-offs`
