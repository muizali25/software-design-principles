# Immutability: Stable Order Line Values

## Problem
Order lines use money to calculate totals. Shared mutable prices can make totals change unexpectedly after a line is passed elsewhere.

In a production system, this requirement will change as the business evolves. The design should make revising quantity or sharing prices across workflows visible, local, and safe rather than allowing an unrelated workflow to become the change point.

## Bad Implementation
The bad code is centered on **Money exposes mutable Amount and Currency fields; OrderLine.Total reads shared Money state.**

- The method or type has more responsibility than its stable role.
- `// PROBLEM: shared UnitPrice mutations silently change totals.`
- Policy and mechanics are coupled, so the behavior is hard to replace or verify in isolation.

## Why Is This a Problem?
- **What is wrong?** The code combines concerns or places a rule outside its proper owner.
- **What will happen when requirements change?** revising quantity or sharing prices across workflows forces edits in existing workflow code and raises regression risk.
- **How does this affect testing?** Tests must work around hidden behavior or concrete collaborators instead of using focused fakes.
- **How does this affect maintainability?** It is difficult to identify a single owner for the rule, so changes take longer to locate and review.
- **How does this affect scalability?** More variants, callers, and integrations accumulate conditional logic or conflicting behavior.
- **What kind of change would force us to modify this code?** revising quantity or sharing prices across workflows.

## Principle
### Principle
Immutability organizes code so related behavior has a clear owner and changes have a limited blast radius.

### In simple words
Give each rule a clear home. Depend on the capability you need, keep public behavior honest, and do not build flexibility before it is needed.

### When to use it
Use it when a value is a fact, is shared, or crosses thread boundaries. It is especially valuable at domain, infrastructure, and integration boundaries.

### When NOT to use it
Avoid making large mutable aggregates immutable when controlled updates are clearer. The goal is clarity and a cheaper change, not the maximum number of classes.

## Solution
readonly Money validates construction and OrderLine uses init-only state; WithQuantity creates a revised copy. The high-level workflow remains readable while each collaborator owns a focused responsibility.

## Before vs After
| Aspect | Before | After |
|---|---|---|
| Change location | Existing workflow is edited for revising quantity or sharing prices across workflows | The focused rule or collaborator changes locally |
| Dependencies | Policy and concrete mechanics are tangled | Collaboration is explicit and role-focused |
| Tests | Setup works around hidden behavior | A focused fake or direct unit test exercises the rule |
| Growth | New cases enlarge existing control flow | New behavior has an intentional extension or ownership point |

## Unit Tests
Tests prove WithQuantity preserves the original and Money rejects negative values. They document observable behavior rather than implementation trivia, so a safe refactor can retain confidence.

## Real-World Usage
Immutable request models, value objects, events, and messages reduce race conditions in distributed systems.

## Interview Questions
1. **Beginner: What does Immutability mean?** It means placing related behavior behind a clear owner so a change has a small, understandable impact.
2. **Practical: What is the smell in this example?** `Money exposes mutable Amount and Currency fields; OrderLine` makes a policy or integration decision outside the correct focused boundary.
3. **Scenario-based: How would you handle revising quantity or sharing prices across workflows?** Change or add the focused collaborator or rule, then protect it with a targeted test instead of rewriting the whole workflow.
4. **Senior-level: How do you avoid over-design?** Add a seam only where variation, an external boundary, or a protected invariant is real; weigh it against the cost of indirection.
5. **When would you NOT use this principle?** Do not apply it mechanically when a direct, cohesive implementation is clearer and requirements are stable.

## Trade-offs
Applied excessively, Immutability can produce indirection, many small types, registration complexity, and longer navigation paths. Prefer the smallest design that clearly expresses the current change boundary; consolidate abstractions that do not buy independent evolution.

## Design Decision
- **Why did we introduce this abstraction?** We introduced immutable Money and OrderLine value objects so the business rule has an explicit, testable home.
- **Why didn't we use another approach?** We did not retain defensive copying everywhere because it is easy to forget.
- **What would happen if the application became 10x larger?** immutable messages are safer to cache, retry, and distribute.

## Refactoring Commit Simulation
Commit 1: `feat: create naive 13-Immutability implementation`  
Commit 2: `refactor: identify Immutability design problem`  
Commit 3: `refactor: apply Immutability to stable order line values`  
Commit 4: `test: cover focused 13-Immutability behavior`  
Commit 5: `docs: document Immutability trade-offs`
