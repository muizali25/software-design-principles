# Do Not Repeat Knowledge: Single Order Cancellation Rule

## Problem
Checkout and refund workflows both need to know when an order can be cancelled. Policy needs one authoritative meaning, not drifting string comparisons.

In a production system, this requirement will change as the business evolves. The design should make adding a status or changing cancellation eligibility visible, local, and safe rather than allowing an unrelated workflow to become the change point.

## Bad Implementation
The bad code is centered on **Checkout.CanCancel and Refunds.CanCancel duplicate cancellation rules using inconsistent status spellings.**

- The method or type has more responsibility than its stable role.
- `No // PROBLEM marker is present; divergent comparisons demonstrate duplicated business knowledge.`
- Policy and mechanics are coupled, so the behavior is hard to replace or verify in isolation.

## Why Is This a Problem?
- **What is wrong?** The code combines concerns or places a rule outside its proper owner.
- **What will happen when requirements change?** adding a status or changing cancellation eligibility forces edits in existing workflow code and raises regression risk.
- **How does this affect testing?** Tests must work around hidden behavior or concrete collaborators instead of using focused fakes.
- **How does this affect maintainability?** It is difficult to identify a single owner for the rule, so changes take longer to locate and review.
- **How does this affect scalability?** More variants, callers, and integrations accumulate conditional logic or conflicting behavior.
- **What kind of change would force us to modify this code?** adding a status or changing cancellation eligibility.

## Principle
### Principle
Do Not Repeat Knowledge organizes code so related behavior has a clear owner and changes have a limited blast radius.

### In simple words
Give each rule a clear home. Depend on the capability you need, keep public behavior honest, and do not build flexibility before it is needed.

### When to use it
Use it when multiple locations encode the same business fact. It is especially valuable at domain, infrastructure, and integration boundaries.

### When NOT to use it
Avoid deduplicating repeated syntax that does not represent shared knowledge. The goal is clarity and a cheaper change, not the maximum number of classes.

## Solution
OrderStatus provides named states and Order owns IsCancellable and Cancel. The high-level workflow remains readable while each collaborator owns a focused responsibility.

## Before vs After
| Aspect | Before | After |
|---|---|---|
| Change location | Existing workflow is edited for adding a status or changing cancellation eligibility | The focused rule or collaborator changes locally |
| Dependencies | Policy and concrete mechanics are tangled | Collaboration is explicit and role-focused |
| Tests | Setup works around hidden behavior | A focused fake or direct unit test exercises the rule |
| Growth | New cases enlarge existing control flow | New behavior has an intentional extension or ownership point |

## Unit Tests
Tests prove a paid order cancels through the single rule and a shipped order cannot. They document observable behavior rather than implementation trivia, so a safe refactor can retain confidence.

## Real-World Usage
Domain aggregates centralize lifecycle, entitlement, and approval policies used by APIs and workers.

## Interview Questions
1. **Beginner: What does Do Not Repeat Knowledge mean?** It means placing related behavior behind a clear owner so a change has a small, understandable impact.
2. **Practical: What is the smell in this example?** `Checkout` makes a policy or integration decision outside the correct focused boundary.
3. **Scenario-based: How would you handle adding a status or changing cancellation eligibility?** Change or add the focused collaborator or rule, then protect it with a targeted test instead of rewriting the whole workflow.
4. **Senior-level: How do you avoid over-design?** Add a seam only where variation, an external boundary, or a protected invariant is real; weigh it against the cost of indirection.
5. **When would you NOT use this principle?** Do not apply it mechanically when a direct, cohesive implementation is clearer and requirements are stable.

## Trade-offs
Applied excessively, Do Not Repeat Knowledge can produce indirection, many small types, registration complexity, and longer navigation paths. Prefer the smallest design that clearly expresses the current change boundary; consolidate abstractions that do not buy independent evolution.

## Design Decision
- **Why did we introduce this abstraction?** We introduced OrderStatus and Order cancellation behavior so the business rule has an explicit, testable home.
- **Why didn't we use another approach?** We did not retain a shared string helper because it would not own the state transition.
- **What would happen if the application became 10x larger?** new clients call the aggregate rule and policy forks are prevented.

## Refactoring Commit Simulation
Commit 1: `feat: create naive 20-DontRepeatKnowledge implementation`  
Commit 2: `refactor: identify Do Not Repeat Knowledge design problem`  
Commit 3: `refactor: apply Do Not Repeat Knowledge to single order cancellation rule`  
Commit 4: `test: cover focused 20-DontRepeatKnowledge behavior`  
Commit 5: `docs: document Do Not Repeat Knowledge trade-offs`
