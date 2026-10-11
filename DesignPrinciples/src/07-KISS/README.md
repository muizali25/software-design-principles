# KISS: Straightforward Discount Calculation

## Problem
A storefront needs a percentage discount for a cart. There is one fixed formula and no current need for dynamic policies or events.

In a production system, this requirement will change as the business evolves. The design should make changing a fixed formula or validating discount bounds visible, local, and safe rather than allowing an unrelated workflow to become the change point.

## Bad Implementation
The bad code is centered on **DiscountEngine uses strategy and factory interfaces plus an event bus for one percentage formula.**

- The method or type has more responsibility than its stable role.
- `// PROBLEM: Three abstractions and an event are used for one fixed formula.`
- Policy and mechanics are coupled, so the behavior is hard to replace or verify in isolation.

## Why Is This a Problem?
- **What is wrong?** The code combines concerns or places a rule outside its proper owner.
- **What will happen when requirements change?** changing a fixed formula or validating discount bounds forces edits in existing workflow code and raises regression risk.
- **How does this affect testing?** Tests must work around hidden behavior or concrete collaborators instead of using focused fakes.
- **How does this affect maintainability?** It is difficult to identify a single owner for the rule, so changes take longer to locate and review.
- **How does this affect scalability?** More variants, callers, and integrations accumulate conditional logic or conflicting behavior.
- **What kind of change would force us to modify this code?** changing a fixed formula or validating discount bounds.

## Principle
### Principle
KISS organizes code so related behavior has a clear owner and changes have a limited blast radius.

### In simple words
Give each rule a clear home. Depend on the capability you need, keep public behavior honest, and do not build flexibility before it is needed.

### When to use it
Use it when the requirement is small, stable, and easily understood. It is especially valuable at domain, infrastructure, and integration boundaries.

### When NOT to use it
Avoid removing seams where several real providers already exist. The goal is clarity and a cheaper change, not the maximum number of classes.

## Solution
DiscountCalculator validates input and calculates the rounded percentage directly. The high-level workflow remains readable while each collaborator owns a focused responsibility.

## Before vs After
| Aspect | Before | After |
|---|---|---|
| Change location | Existing workflow is edited for changing a fixed formula or validating discount bounds | The focused rule or collaborator changes locally |
| Dependencies | Policy and concrete mechanics are tangled | Collaboration is explicit and role-focused |
| Tests | Setup works around hidden behavior | A focused fake or direct unit test exercises the rule |
| Growth | New cases enlarge existing control flow | New behavior has an intentional extension or ownership point |

## Unit Tests
Tests cover representative rounding outcomes and reject invalid percentage. They document observable behavior rather than implementation trivia, so a safe refactor can retain confidence.

## Real-World Usage
API endpoints should keep deterministic calculations direct until variation appears.

## Interview Questions
1. **Beginner: What does KISS mean?** It means placing related behavior behind a clear owner so a change has a small, understandable impact.
2. **Practical: What is the smell in this example?** `DiscountEngine uses strategy and factory interfaces plus an event bus for one percentage formula` makes a policy or integration decision outside the correct focused boundary.
3. **Scenario-based: How would you handle changing a fixed formula or validating discount bounds?** Change or add the focused collaborator or rule, then protect it with a targeted test instead of rewriting the whole workflow.
4. **Senior-level: How do you avoid over-design?** Add a seam only where variation, an external boundary, or a protected invariant is real; weigh it against the cost of indirection.
5. **When would you NOT use this principle?** Do not apply it mechanically when a direct, cohesive implementation is clearer and requirements are stable.

## Trade-offs
Applied excessively, KISS can produce indirection, many small types, registration complexity, and longer navigation paths. Prefer the smallest design that clearly expresses the current change boundary; consolidate abstractions that do not buy independent evolution.

## Design Decision
- **Why did we introduce this abstraction?** We introduced one calculator method so the business rule has an explicit, testable home.
- **Why didn't we use another approach?** We did not retain factory and event abstractions because no requirement consumes flexibility.
- **What would happen if the application became 10x larger?** add a strategy only when genuine variants arrive.

## Refactoring Commit Simulation
Commit 1: `feat: create naive 07-KISS implementation`  
Commit 2: `refactor: identify KISS design problem`  
Commit 3: `refactor: apply KISS to straightforward discount calculation`  
Commit 4: `test: cover focused 07-KISS behavior`  
Commit 5: `docs: document KISS trade-offs`
