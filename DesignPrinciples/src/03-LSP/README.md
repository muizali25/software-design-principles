# Liskov Substitution Principle: Consistent Refund Policies

## Problem
Support must refund an order through its original payment method or as store credit. Whichever policy is chosen, callers need a complete result rather than a runtime surprise.

In a production system, this requirement will change as the business evolves. The design should make supporting a different refund destination or payout provider visible, local, and safe rather than allowing an unrelated workflow to become the change point.

## Bad Implementation
The bad code is centered on **StoreCreditRefundProcessor inherits RefundProcessor but throws from Refund for a valid request.**

- The method or type has more responsibility than its stable role.
- `// PROBLEM: this subtype rejects a valid base operation and breaks callers.`
- Policy and mechanics are coupled, so the behavior is hard to replace or verify in isolation.

## Why Is This a Problem?
- **What is wrong?** The code combines concerns or places a rule outside its proper owner.
- **What will happen when requirements change?** supporting a different refund destination or payout provider forces edits in existing workflow code and raises regression risk.
- **How does this affect testing?** Tests must work around hidden behavior or concrete collaborators instead of using focused fakes.
- **How does this affect maintainability?** It is difficult to identify a single owner for the rule, so changes take longer to locate and review.
- **How does this affect scalability?** More variants, callers, and integrations accumulate conditional logic or conflicting behavior.
- **What kind of change would force us to modify this code?** supporting a different refund destination or payout provider.

## Principle
### Principle
Liskov Substitution Principle organizes code so related behavior has a clear owner and changes have a limited blast radius.

### In simple words
Give each rule a clear home. Depend on the capability you need, keep public behavior honest, and do not build flexibility before it is needed.

### When to use it
Use it when multiple implementations must be safely interchangeable. It is especially valuable at domain, infrastructure, and integration boundaries.

### When NOT to use it
Avoid forcing unrelated operations into an inheritance hierarchy. The goal is clarity and a cheaper change, not the maximum number of classes.

## Solution
IRefundPolicy returns RefundResult for OriginalPaymentRefundPolicy and StoreCreditRefundPolicy. RefundService depends only on that contract. The high-level workflow remains readable while each collaborator owns a focused responsibility.

## Before vs After
| Aspect | Before | After |
|---|---|---|
| Change location | Existing workflow is edited for supporting a different refund destination or payout provider | The focused rule or collaborator changes locally |
| Dependencies | Policy and concrete mechanics are tangled | Collaboration is explicit and role-focused |
| Tests | Setup works around hidden behavior | A focused fake or direct unit test exercises the rule |
| Growth | New cases enlarge existing control flow | New behavior has an intentional extension or ownership point |

## Unit Tests
Tests run both policies through IRefundPolicy and verify amount and destination. They document observable behavior rather than implementation trivia, so a safe refactor can retain confidence.

## Real-World Usage
Payment adapters and service gateways must preserve interface contracts across implementations.

## Interview Questions
1. **Beginner: What does Liskov Substitution Principle mean?** It means placing related behavior behind a clear owner so a change has a small, understandable impact.
2. **Practical: What is the smell in this example?** `StoreCreditRefundProcessor inherits RefundProcessor but throws from Refund for a valid request` makes a policy or integration decision outside the correct focused boundary.
3. **Scenario-based: How would you handle supporting a different refund destination or payout provider?** Change or add the focused collaborator or rule, then protect it with a targeted test instead of rewriting the whole workflow.
4. **Senior-level: How do you avoid over-design?** Add a seam only where variation, an external boundary, or a protected invariant is real; weigh it against the cost of indirection.
5. **When would you NOT use this principle?** Do not apply it mechanically when a direct, cohesive implementation is clearer and requirements are stable.

## Trade-offs
Applied excessively, Liskov Substitution Principle can produce indirection, many small types, registration complexity, and longer navigation paths. Prefer the smallest design that clearly expresses the current change boundary; consolidate abstractions that do not buy independent evolution.

## Design Decision
- **Why did we introduce this abstraction?** We introduced IRefundPolicy with a result that represents both destinations so the business rule has an explicit, testable home.
- **Why didn't we use another approach?** We did not retain a cash-only base class because store credit violates its implied contract.
- **What would happen if the application became 10x larger?** policy implementations can grow without special caller branches.

## Refactoring Commit Simulation
Commit 1: `feat: create naive 03-LSP implementation`  
Commit 2: `refactor: identify Liskov Substitution Principle design problem`  
Commit 3: `refactor: apply Liskov Substitution Principle to consistent refund policies`  
Commit 4: `test: cover focused 03-LSP behavior`  
Commit 5: `docs: document Liskov Substitution Principle trade-offs`
