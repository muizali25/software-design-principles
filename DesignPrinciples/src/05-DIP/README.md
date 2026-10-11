# Dependency Inversion Principle: Testable Order Fulfillment

## Problem
When an order ships, the application must mark it fulfilled and notify the customer. The policy should not be tied to SQL or SMTP.

In a production system, this requirement will change as the business evolves. The design should make moving from SQL to an API or swapping mail providers visible, local, and safe rather than allowing an unrelated workflow to become the change point.

## Bad Implementation
The bad code is centered on **OrderFulfillmentService.Fulfill constructs SqlOrderRepository and SmtpEmailSender itself.**

- The method or type has more responsibility than its stable role.
- `// PROBLEM: policy directly depends on concrete infrastructure and cannot be isolated in tests.`
- Policy and mechanics are coupled, so the behavior is hard to replace or verify in isolation.

## Why Is This a Problem?
- **What is wrong?** The code combines concerns or places a rule outside its proper owner.
- **What will happen when requirements change?** moving from SQL to an API or swapping mail providers forces edits in existing workflow code and raises regression risk.
- **How does this affect testing?** Tests must work around hidden behavior or concrete collaborators instead of using focused fakes.
- **How does this affect maintainability?** It is difficult to identify a single owner for the rule, so changes take longer to locate and review.
- **How does this affect scalability?** More variants, callers, and integrations accumulate conditional logic or conflicting behavior.
- **What kind of change would force us to modify this code?** moving from SQL to an API or swapping mail providers.

## Principle
### Principle
Dependency Inversion Principle organizes code so related behavior has a clear owner and changes have a limited blast radius.

### In simple words
Give each rule a clear home. Depend on the capability you need, keep public behavior honest, and do not build flexibility before it is needed.

### When to use it
Use it when business policy uses external systems. It is especially valuable at domain, infrastructure, and integration boundaries.

### When NOT to use it
Avoid introducing an interface for pure local logic with no alternate boundary. The goal is clarity and a cheaper change, not the maximum number of classes.

## Solution
OrderFulfillmentService depends on IOrderRepository and IEmailSender, with console implementations at the edge. The high-level workflow remains readable while each collaborator owns a focused responsibility.

## Before vs After
| Aspect | Before | After |
|---|---|---|
| Change location | Existing workflow is edited for moving from SQL to an API or swapping mail providers | The focused rule or collaborator changes locally |
| Dependencies | Policy and concrete mechanics are tangled | Collaboration is explicit and role-focused |
| Tests | Setup works around hidden behavior | A focused fake or direct unit test exercises the rule |
| Growth | New cases enlarge existing control flow | New behavior has an intentional extension or ownership point |

## Unit Tests
Tests supply fake ports and verify fulfillment persists and notifies. They document observable behavior rather than implementation trivia, so a safe refactor can retain confidence.

## Real-World Usage
ASP.NET Core dependency injection supplies repositories, HttpClient adapters, buses, and gateways.

## Interview Questions
1. **Beginner: What does Dependency Inversion Principle mean?** It means placing related behavior behind a clear owner so a change has a small, understandable impact.
2. **Practical: What is the smell in this example?** `OrderFulfillmentService` makes a policy or integration decision outside the correct focused boundary.
3. **Scenario-based: How would you handle moving from SQL to an API or swapping mail providers?** Change or add the focused collaborator or rule, then protect it with a targeted test instead of rewriting the whole workflow.
4. **Senior-level: How do you avoid over-design?** Add a seam only where variation, an external boundary, or a protected invariant is real; weigh it against the cost of indirection.
5. **When would you NOT use this principle?** Do not apply it mechanically when a direct, cohesive implementation is clearer and requirements are stable.

## Trade-offs
Applied excessively, Dependency Inversion Principle can produce indirection, many small types, registration complexity, and longer navigation paths. Prefer the smallest design that clearly expresses the current change boundary; consolidate abstractions that do not buy independent evolution.

## Design Decision
- **Why did we introduce this abstraction?** We introduced repository and email ports owned by policy so the business rule has an explicit, testable home.
- **Why didn't we use another approach?** We did not retain newing infrastructure inside the service because it hides requirements.
- **What would happen if the application became 10x larger?** infrastructure can change independently and contract tests protect adapters.

## Refactoring Commit Simulation
Commit 1: `feat: create naive 05-DIP implementation`  
Commit 2: `refactor: identify Dependency Inversion Principle design problem`  
Commit 3: `refactor: apply Dependency Inversion Principle to testable order fulfillment`  
Commit 4: `test: cover focused 05-DIP behavior`  
Commit 5: `docs: document Dependency Inversion Principle trade-offs`
