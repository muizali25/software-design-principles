# Open/Closed Principle: Extensible Payment Processing

## Problem
Checkout supports cards, PayPal, and bank transfers today, while new payment providers are expected. Existing checkout flow should remain stable when one is added.

In a production system, this requirement will change as the business evolves. The design should make adding crypto, a regional wallet, or a new acquiring bank visible, local, and safe rather than allowing an unrelated workflow to become the change point.

## Bad Implementation
The bad code is centered on **PaymentService.Process switches on PaymentMethod and contains provider-specific behavior.**

- The method or type has more responsibility than its stable role.
- `// PROBLEM: every new payment method requires modifying this switch.`
- Policy and mechanics are coupled, so the behavior is hard to replace or verify in isolation.

## Why Is This a Problem?
- **What is wrong?** The code combines concerns or places a rule outside its proper owner.
- **What will happen when requirements change?** adding crypto, a regional wallet, or a new acquiring bank forces edits in existing workflow code and raises regression risk.
- **How does this affect testing?** Tests must work around hidden behavior or concrete collaborators instead of using focused fakes.
- **How does this affect maintainability?** It is difficult to identify a single owner for the rule, so changes take longer to locate and review.
- **How does this affect scalability?** More variants, callers, and integrations accumulate conditional logic or conflicting behavior.
- **What kind of change would force us to modify this code?** adding crypto, a regional wallet, or a new acquiring bank.

## Principle
### Principle
Open/Closed Principle organizes code so related behavior has a clear owner and changes have a limited blast radius.

### In simple words
Give each rule a clear home. Depend on the capability you need, keep public behavior honest, and do not build flexibility before it is needed.

### When to use it
Use it when new variants are expected and share a stable operation. It is especially valuable at domain, infrastructure, and integration boundaries.

### When NOT to use it
Avoid creating plugins for a closed set of two stable cases. The goal is clarity and a cheaper change, not the maximum number of classes.

## Solution
IPaymentProcessor defines Method and Process. PaymentService creates a lookup from registered processors and delegates. The high-level workflow remains readable while each collaborator owns a focused responsibility.

## Before vs After
| Aspect | Before | After |
|---|---|---|
| Change location | Existing workflow is edited for adding crypto, a regional wallet, or a new acquiring bank | The focused rule or collaborator changes locally |
| Dependencies | Policy and concrete mechanics are tangled | Collaboration is explicit and role-focused |
| Tests | Setup works around hidden behavior | A focused fake or direct unit test exercises the rule |
| Growth | New cases enlarge existing control flow | New behavior has an intentional extension or ownership point |

## Unit Tests
Tests prove a registered processor is selected and an unregistered method is rejected. They document observable behavior rather than implementation trivia, so a safe refactor can retain confidence.

## Real-World Usage
Web APIs use strategy handlers for payment providers, serializers, authentication schemes, and message consumers.

## Interview Questions
1. **Beginner: What does Open/Closed Principle mean?** It means placing related behavior behind a clear owner so a change has a small, understandable impact.
2. **Practical: What is the smell in this example?** `PaymentService` makes a policy or integration decision outside the correct focused boundary.
3. **Scenario-based: How would you handle adding crypto, a regional wallet, or a new acquiring bank?** Change or add the focused collaborator or rule, then protect it with a targeted test instead of rewriting the whole workflow.
4. **Senior-level: How do you avoid over-design?** Add a seam only where variation, an external boundary, or a protected invariant is real; weigh it against the cost of indirection.
5. **When would you NOT use this principle?** Do not apply it mechanically when a direct, cohesive implementation is clearer and requirements are stable.

## Trade-offs
Applied excessively, Open/Closed Principle can produce indirection, many small types, registration complexity, and longer navigation paths. Prefer the smallest design that clearly expresses the current change boundary; consolidate abstractions that do not buy independent evolution.

## Design Decision
- **Why did we introduce this abstraction?** We introduced IPaymentProcessor as a stable extension point so the business rule has an explicit, testable home.
- **Why didn't we use another approach?** We did not retain a growing switch because every extension edits trusted workflow code.
- **What would happen if the application became 10x larger?** new providers can be deployed and tested as isolated implementations.

## Refactoring Commit Simulation
Commit 1: `feat: create naive 02-OCP implementation`  
Commit 2: `refactor: identify Open/Closed Principle design problem`  
Commit 3: `refactor: apply Open/Closed Principle to extensible payment processing`  
Commit 4: `test: cover focused 02-OCP behavior`  
Commit 5: `docs: document Open/Closed Principle trade-offs`
