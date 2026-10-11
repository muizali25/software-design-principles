# Separation of Concerns: Employee Onboarding Workflow

## Problem
Onboarding validates data, calculates monthly pay, stores an employee, and sends a welcome message. Each concern has distinct policy and infrastructure drivers.

In a production system, this requirement will change as the business evolves. The design should make a payroll formula, persistence migration, email template, or validation rule visible, local, and safe rather than allowing an unrelated workflow to become the change point.

## Bad Implementation
The bad code is centered on **EmployeeOnboardingController.Onboard validates, calculates, writes a file, and sends console output together.**

- The method or type has more responsibility than its stable role.
- `// PROBLEM: One class owns validation, payroll policy, storage, and communication.`
- Policy and mechanics are coupled, so the behavior is hard to replace or verify in isolation.

## Why Is This a Problem?
- **What is wrong?** The code combines concerns or places a rule outside its proper owner.
- **What will happen when requirements change?** a payroll formula, persistence migration, email template, or validation rule forces edits in existing workflow code and raises regression risk.
- **How does this affect testing?** Tests must work around hidden behavior or concrete collaborators instead of using focused fakes.
- **How does this affect maintainability?** It is difficult to identify a single owner for the rule, so changes take longer to locate and review.
- **How does this affect scalability?** More variants, callers, and integrations accumulate conditional logic or conflicting behavior.
- **What kind of change would force us to modify this code?** a payroll formula, persistence migration, email template, or validation rule.

## Principle
### Principle
Separation of Concerns organizes code so related behavior has a clear owner and changes have a limited blast radius.

### In simple words
Give each rule a clear home. Depend on the capability you need, keep public behavior honest, and do not build flexibility before it is needed.

### When to use it
Use it when a workflow combines UI, policy, storage, or communication concerns. It is especially valuable at domain, infrastructure, and integration boundaries.

### When NOT to use it
Avoid separating code only by technical layer when one operation is clearer. The goal is clarity and a cheaper change, not the maximum number of classes.

## Solution
EmployeeOnboardingService orchestrates EmployeeValidator, PayrollService, IEmployeeRepository, and IEmployeeNotification. The high-level workflow remains readable while each collaborator owns a focused responsibility.

## Before vs After
| Aspect | Before | After |
|---|---|---|
| Change location | Existing workflow is edited for a payroll formula, persistence migration, email template, or validation rule | The focused rule or collaborator changes locally |
| Dependencies | Policy and concrete mechanics are tangled | Collaboration is explicit and role-focused |
| Tests | Setup works around hidden behavior | A focused fake or direct unit test exercises the rule |
| Growth | New cases enlarge existing control flow | New behavior has an intentional extension or ownership point |

## Unit Tests
Tests verify valid onboarding persists calculated pay and invalid email fails before persistence. They document observable behavior rather than implementation trivia, so a safe refactor can retain confidence.

## Real-World Usage
Controllers delegate to application services while validators, repositories, and notifications own their concerns.

## Interview Questions
1. **Beginner: What does Separation of Concerns mean?** It means placing related behavior behind a clear owner so a change has a small, understandable impact.
2. **Practical: What is the smell in this example?** `EmployeeOnboardingController` makes a policy or integration decision outside the correct focused boundary.
3. **Scenario-based: How would you handle a payroll formula, persistence migration, email template, or validation rule?** Change or add the focused collaborator or rule, then protect it with a targeted test instead of rewriting the whole workflow.
4. **Senior-level: How do you avoid over-design?** Add a seam only where variation, an external boundary, or a protected invariant is real; weigh it against the cost of indirection.
5. **When would you NOT use this principle?** Do not apply it mechanically when a direct, cohesive implementation is clearer and requirements are stable.

## Trade-offs
Applied excessively, Separation of Concerns can produce indirection, many small types, registration complexity, and longer navigation paths. Prefer the smallest design that clearly expresses the current change boundary; consolidate abstractions that do not buy independent evolution.

## Design Decision
- **Why did we introduce this abstraction?** We introduced services for validation, payroll, storage, and notification so the business rule has an explicit, testable home.
- **Why didn't we use another approach?** We did not retain a larger controller because unrelated changes collide.
- **What would happen if the application became 10x larger?** teams can replace adapters and expand policies independently.

## Refactoring Commit Simulation
Commit 1: `feat: create naive 11-SeparationOfConcerns implementation`  
Commit 2: `refactor: identify Separation of Concerns design problem`  
Commit 3: `refactor: apply Separation of Concerns to employee onboarding workflow`  
Commit 4: `test: cover focused 11-SeparationOfConcerns behavior`  
Commit 5: `docs: document Separation of Concerns trade-offs`
