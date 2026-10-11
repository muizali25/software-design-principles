# Single Level of Abstraction: Readable Payroll Processing

## Problem
Payroll validates pay, calculates tax, persists a result, and notifies the employee. The top method should describe workflow rather than SQL and SMTP mechanics.

In a production system, this requirement will change as the business evolves. The design should make changing tax calculation, database technology, email transport, or validation visible, local, and safe rather than allowing an unrelated workflow to become the change point.

## Bad Implementation
The bad code is centered on **PayrollProcessor.ProcessPayroll mixes tax arithmetic, SQL string construction, SMTP host details, and business output.**

- The method or type has more responsibility than its stable role.
- `No // PROBLEM marker is present; mixed abstraction levels make the method difficult to read and change.`
- Policy and mechanics are coupled, so the behavior is hard to replace or verify in isolation.

## Why Is This a Problem?
- **What is wrong?** The code combines concerns or places a rule outside its proper owner.
- **What will happen when requirements change?** changing tax calculation, database technology, email transport, or validation forces edits in existing workflow code and raises regression risk.
- **How does this affect testing?** Tests must work around hidden behavior or concrete collaborators instead of using focused fakes.
- **How does this affect maintainability?** It is difficult to identify a single owner for the rule, so changes take longer to locate and review.
- **How does this affect scalability?** More variants, callers, and integrations accumulate conditional logic or conflicting behavior.
- **What kind of change would force us to modify this code?** changing tax calculation, database technology, email transport, or validation.

## Principle
### Principle
Single Level of Abstraction organizes code so related behavior has a clear owner and changes have a limited blast radius.

### In simple words
Give each rule a clear home. Depend on the capability you need, keep public behavior honest, and do not build flexibility before it is needed.

### When to use it
Use it when a method mixes policy decisions with low-level mechanics. It is especially valuable at domain, infrastructure, and integration boundaries.

### When NOT to use it
Avoid extracting every two-line expression until workflow becomes fragmented. The goal is clarity and a cheaper change, not the maximum number of classes.

## Solution
PayrollProcessor reads as Validate, CalculatePayroll, Persist, and NotifyEmployee with tax, repository, and sender ports. The high-level workflow remains readable while each collaborator owns a focused responsibility.

## Before vs After
| Aspect | Before | After |
|---|---|---|
| Change location | Existing workflow is edited for changing tax calculation, database technology, email transport, or validation | The focused rule or collaborator changes locally |
| Dependencies | Policy and concrete mechanics are tangled | Collaboration is explicit and role-focused |
| Tests | Setup works around hidden behavior | A focused fake or direct unit test exercises the rule |
| Growth | New cases enlarge existing control flow | New behavior has an intentional extension or ownership point |

## Unit Tests
Tests verify calculated tax and net pay, then confirm the same Payroll is saved and sent. They document observable behavior rather than implementation trivia, so a safe refactor can retain confidence.

## Real-World Usage
Use-case handlers should read as business steps while adapters contain protocol details.

## Interview Questions
1. **Beginner: What does Single Level of Abstraction mean?** It means placing related behavior behind a clear owner so a change has a small, understandable impact.
2. **Practical: What is the smell in this example?** `PayrollProcessor` makes a policy or integration decision outside the correct focused boundary.
3. **Scenario-based: How would you handle changing tax calculation, database technology, email transport, or validation?** Change or add the focused collaborator or rule, then protect it with a targeted test instead of rewriting the whole workflow.
4. **Senior-level: How do you avoid over-design?** Add a seam only where variation, an external boundary, or a protected invariant is real; weigh it against the cost of indirection.
5. **When would you NOT use this principle?** Do not apply it mechanically when a direct, cohesive implementation is clearer and requirements are stable.

## Trade-offs
Applied excessively, Single Level of Abstraction can produce indirection, many small types, registration complexity, and longer navigation paths. Prefer the smallest design that clearly expresses the current change boundary; consolidate abstractions that do not buy independent evolution.

## Design Decision
- **Why did we introduce this abstraction?** We introduced named workflow steps and infrastructure interfaces so the business rule has an explicit, testable home.
- **Why didn't we use another approach?** We did not retain one long method because its narrative is buried in mechanics.
- **What would happen if the application became 10x larger?** steps can gain retries and auditing without obscuring the use case.

## Refactoring Commit Simulation
Commit 1: `feat: create naive 18-SingleLevelOfAbstraction implementation`  
Commit 2: `refactor: identify Single Level of Abstraction design problem`  
Commit 3: `refactor: apply Single Level of Abstraction to readable payroll processing`  
Commit 4: `test: cover focused 18-SingleLevelOfAbstraction behavior`  
Commit 5: `docs: document Single Level of Abstraction trade-offs`
