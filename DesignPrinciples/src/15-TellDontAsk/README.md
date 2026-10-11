# Tell, Do Not Ask: Order Shipping State Transitions

## Problem
An order may ship only after payment and after at least one item exists. The object that owns state should enforce transition rules.

In a production system, this requirement will change as the business evolves. The design should make adding cancellation, fraud review, partial shipment, or payment states visible, local, and safe rather than allowing an unrelated workflow to become the change point.

## Bad Implementation
The bad code is centered on **OrderShippingService reads public Order.Status, Paid, and Items, then changes Status externally.**

- The method or type has more responsibility than its stable role.
- `// PROBLEM: Rules and state changes are outside the object that owns the state.`
- Policy and mechanics are coupled, so the behavior is hard to replace or verify in isolation.

## Why Is This a Problem?
- **What is wrong?** The code combines concerns or places a rule outside its proper owner.
- **What will happen when requirements change?** adding cancellation, fraud review, partial shipment, or payment states forces edits in existing workflow code and raises regression risk.
- **How does this affect testing?** Tests must work around hidden behavior or concrete collaborators instead of using focused fakes.
- **How does this affect maintainability?** It is difficult to identify a single owner for the rule, so changes take longer to locate and review.
- **How does this affect scalability?** More variants, callers, and integrations accumulate conditional logic or conflicting behavior.
- **What kind of change would force us to modify this code?** adding cancellation, fraud review, partial shipment, or payment states.

## Principle
### Principle
Tell, Do Not Ask organizes code so related behavior has a clear owner and changes have a limited blast radius.

### In simple words
Give each rule a clear home. Depend on the capability you need, keep public behavior honest, and do not build flexibility before it is needed.

### When to use it
Use it when callers read an object only to decide how to mutate it. It is especially valuable at domain, infrastructure, and integration boundaries.

### When NOT to use it
Avoid moving orchestration involving several aggregates into one entity. The goal is clarity and a cheaper change, not the maximum number of classes.

## Solution
Order owns AddItem, MarkPaid, and Ship; OrderShippingService tells the order to ship. The high-level workflow remains readable while each collaborator owns a focused responsibility.

## Before vs After
| Aspect | Before | After |
|---|---|---|
| Change location | Existing workflow is edited for adding cancellation, fraud review, partial shipment, or payment states | The focused rule or collaborator changes locally |
| Dependencies | Policy and concrete mechanics are tangled | Collaboration is explicit and role-focused |
| Tests | Setup works around hidden behavior | A focused fake or direct unit test exercises the rule |
| Growth | New cases enlarge existing control flow | New behavior has an intentional extension or ownership point |

## Unit Tests
Tests prove a paid order ships and an unpaid order remains unchanged when rejected. They document observable behavior rather than implementation trivia, so a safe refactor can retain confidence.

## Real-World Usage
Domain entities in APIs and microservices should own transitions rather than expose setters.

## Interview Questions
1. **Beginner: What does Tell, Do Not Ask mean?** It means placing related behavior behind a clear owner so a change has a small, understandable impact.
2. **Practical: What is the smell in this example?** `OrderShippingService reads public Order` makes a policy or integration decision outside the correct focused boundary.
3. **Scenario-based: How would you handle adding cancellation, fraud review, partial shipment, or payment states?** Change or add the focused collaborator or rule, then protect it with a targeted test instead of rewriting the whole workflow.
4. **Senior-level: How do you avoid over-design?** Add a seam only where variation, an external boundary, or a protected invariant is real; weigh it against the cost of indirection.
5. **When would you NOT use this principle?** Do not apply it mechanically when a direct, cohesive implementation is clearer and requirements are stable.

## Trade-offs
Applied excessively, Tell, Do Not Ask can produce indirection, many small types, registration complexity, and longer navigation paths. Prefer the smallest design that clearly expresses the current change boundary; consolidate abstractions that do not buy independent evolution.

## Design Decision
- **Why did we introduce this abstraction?** We introduced Order transition methods and private state changes so the business rule has an explicit, testable home.
- **Why didn't we use another approach?** We did not retain external if statements because each caller can diverge.
- **What would happen if the application became 10x larger?** a richer state machine stays coherent.

## Refactoring Commit Simulation
Commit 1: `feat: create naive 15-TellDontAsk implementation`  
Commit 2: `refactor: identify Tell, Do Not Ask design problem`  
Commit 3: `refactor: apply Tell, Do Not Ask to order shipping state transitions`  
Commit 4: `test: cover focused 15-TellDontAsk behavior`  
Commit 5: `docs: document Tell, Do Not Ask trade-offs`
