# Explicit Dependencies: Transparent Invoice Delivery

## Problem
Sending an invoice requires persistence and email delivery. A reader should see those requirements from construction and tests should supply them directly.

In a production system, this requirement will change as the business evolves. The design should make swapping mail delivery, adding logging, or testing failure visible, local, and safe rather than allowing an unrelated workflow to become the change point.

## Bad Implementation
The bad code is centered on **InvoiceService resolves a repository through ServiceLocator and constructs SmtpClient inside Send.**

- The method or type has more responsibility than its stable role.
- `No // PROBLEM marker is present; locator lookup and new SmtpClient hide dependencies.`
- Policy and mechanics are coupled, so the behavior is hard to replace or verify in isolation.

## Why Is This a Problem?
- **What is wrong?** The code combines concerns or places a rule outside its proper owner.
- **What will happen when requirements change?** swapping mail delivery, adding logging, or testing failure forces edits in existing workflow code and raises regression risk.
- **How does this affect testing?** Tests must work around hidden behavior or concrete collaborators instead of using focused fakes.
- **How does this affect maintainability?** It is difficult to identify a single owner for the rule, so changes take longer to locate and review.
- **How does this affect scalability?** More variants, callers, and integrations accumulate conditional logic or conflicting behavior.
- **What kind of change would force us to modify this code?** swapping mail delivery, adding logging, or testing failure.

## Principle
### Principle
Explicit Dependencies organizes code so related behavior has a clear owner and changes have a limited blast radius.

### In simple words
Give each rule a clear home. Depend on the capability you need, keep public behavior honest, and do not build flexibility before it is needed.

### When to use it
Use it when a class relies on collaborators outside its pure logic. It is especially valuable at domain, infrastructure, and integration boundaries.

### When NOT to use it
Avoid injecting stable global runtime facilities that need no substitution. The goal is clarity and a cheaper change, not the maximum number of classes.

## Solution
InvoiceService receives IInvoiceRepository and IEmailSender in its constructor. The high-level workflow remains readable while each collaborator owns a focused responsibility.

## Before vs After
| Aspect | Before | After |
|---|---|---|
| Change location | Existing workflow is edited for swapping mail delivery, adding logging, or testing failure | The focused rule or collaborator changes locally |
| Dependencies | Policy and concrete mechanics are tangled | Collaboration is explicit and role-focused |
| Tests | Setup works around hidden behavior | A focused fake or direct unit test exercises the rule |
| Growth | New cases enlarge existing control flow | New behavior has an intentional extension or ownership point |

## Unit Tests
Tests supply fakes and prove the invoice is persisted and the email recipient is used. They document observable behavior rather than implementation trivia, so a safe refactor can retain confidence.

## Real-World Usage
Constructor injection is standard ASP.NET Core practice for database, mail, logging, and gateways.

## Interview Questions
1. **Beginner: What does Explicit Dependencies mean?** It means placing related behavior behind a clear owner so a change has a small, understandable impact.
2. **Practical: What is the smell in this example?** `InvoiceService resolves a repository through ServiceLocator and constructs SmtpClient inside Send` makes a policy or integration decision outside the correct focused boundary.
3. **Scenario-based: How would you handle swapping mail delivery, adding logging, or testing failure?** Change or add the focused collaborator or rule, then protect it with a targeted test instead of rewriting the whole workflow.
4. **Senior-level: How do you avoid over-design?** Add a seam only where variation, an external boundary, or a protected invariant is real; weigh it against the cost of indirection.
5. **When would you NOT use this principle?** Do not apply it mechanically when a direct, cohesive implementation is clearer and requirements are stable.

## Trade-offs
Applied excessively, Explicit Dependencies can produce indirection, many small types, registration complexity, and longer navigation paths. Prefer the smallest design that clearly expresses the current change boundary; consolidate abstractions that do not buy independent evolution.

## Design Decision
- **Why did we introduce this abstraction?** We introduced constructor parameters for repository and email sender so the business rule has an explicit, testable home.
- **Why didn't we use another approach?** We did not retain service locator because it conceals required setup.
- **What would happen if the application became 10x larger?** dependency graphs remain inspectable and composition roots manage lifetimes.

## Refactoring Commit Simulation
Commit 1: `feat: create naive 19-ExplicitDependencies implementation`  
Commit 2: `refactor: identify Explicit Dependencies design problem`  
Commit 3: `refactor: apply Explicit Dependencies to transparent invoice delivery`  
Commit 4: `test: cover focused 19-ExplicitDependencies behavior`  
Commit 5: `docs: document Explicit Dependencies trade-offs`
