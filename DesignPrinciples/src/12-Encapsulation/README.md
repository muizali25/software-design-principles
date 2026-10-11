# Encapsulation: Safe Bank Account State

## Problem
A banking domain must retain a reliable balance and ledger. Deposits and withdrawals must preserve positive amounts and no overdrafts.

In a production system, this requirement will change as the business evolves. The design should make enforcing overdraft policy, audit data, or ledger storage visible, local, and safe rather than allowing an unrelated workflow to become the change point.

## Bad Implementation
The bad code is centered on **BankAccount exposes public Balance and Transactions fields for any caller to overwrite.**

- The method or type has more responsibility than its stable role.
- `// PROBLEM: Any caller can create an impossible account state.`
- Policy and mechanics are coupled, so the behavior is hard to replace or verify in isolation.

## Why Is This a Problem?
- **What is wrong?** The code combines concerns or places a rule outside its proper owner.
- **What will happen when requirements change?** enforcing overdraft policy, audit data, or ledger storage forces edits in existing workflow code and raises regression risk.
- **How does this affect testing?** Tests must work around hidden behavior or concrete collaborators instead of using focused fakes.
- **How does this affect maintainability?** It is difficult to identify a single owner for the rule, so changes take longer to locate and review.
- **How does this affect scalability?** More variants, callers, and integrations accumulate conditional logic or conflicting behavior.
- **What kind of change would force us to modify this code?** enforcing overdraft policy, audit data, or ledger storage.

## Principle
### Principle
Encapsulation organizes code so related behavior has a clear owner and changes have a limited blast radius.

### In simple words
Give each rule a clear home. Depend on the capability you need, keep public behavior honest, and do not build flexibility before it is needed.

### When to use it
Use it when an object owns rules that must always hold for its state. It is especially valuable at domain, infrastructure, and integration boundaries.

### When NOT to use it
Avoid hiding fields in trivial passive DTOs with no behavior. The goal is clarity and a cheaper change, not the maximum number of classes.

## Solution
BankAccount keeps private state, exposes read-only views, and changes it through validated Deposit and Withdraw methods. The high-level workflow remains readable while each collaborator owns a focused responsibility.

## Before vs After
| Aspect | Before | After |
|---|---|---|
| Change location | Existing workflow is edited for enforcing overdraft policy, audit data, or ledger storage | The focused rule or collaborator changes locally |
| Dependencies | Policy and concrete mechanics are tangled | Collaboration is explicit and role-focused |
| Tests | Setup works around hidden behavior | A focused fake or direct unit test exercises the rule |
| Growth | New cases enlarge existing control flow | New behavior has an intentional extension or ownership point |

## Unit Tests
Tests prove deposits update balance and ledger and overdrafts preserve balance. They document observable behavior rather than implementation trivia, so a safe refactor can retain confidence.

## Real-World Usage
Enterprise aggregates protect invariants through methods rather than writable fields.

## Interview Questions
1. **Beginner: What does Encapsulation mean?** It means placing related behavior behind a clear owner so a change has a small, understandable impact.
2. **Practical: What is the smell in this example?** `BankAccount exposes public Balance and Transactions fields for any caller to overwrite` makes a policy or integration decision outside the correct focused boundary.
3. **Scenario-based: How would you handle enforcing overdraft policy, audit data, or ledger storage?** Change or add the focused collaborator or rule, then protect it with a targeted test instead of rewriting the whole workflow.
4. **Senior-level: How do you avoid over-design?** Add a seam only where variation, an external boundary, or a protected invariant is real; weigh it against the cost of indirection.
5. **When would you NOT use this principle?** Do not apply it mechanically when a direct, cohesive implementation is clearer and requirements are stable.

## Trade-offs
Applied excessively, Encapsulation can produce indirection, many small types, registration complexity, and longer navigation paths. Prefer the smallest design that clearly expresses the current change boundary; consolidate abstractions that do not buy independent evolution.

## Design Decision
- **Why did we introduce this abstraction?** We introduced the BankAccount behavior boundary so the business rule has an explicit, testable home.
- **Why didn't we use another approach?** We did not retain public fields because callers bypass every invariant.
- **What would happen if the application became 10x larger?** limits and holds remain localized to the aggregate.

## Refactoring Commit Simulation
Commit 1: `feat: create naive 12-Encapsulation implementation`  
Commit 2: `refactor: identify Encapsulation design problem`  
Commit 3: `refactor: apply Encapsulation to safe bank account state`  
Commit 4: `test: cover focused 12-Encapsulation behavior`  
Commit 5: `docs: document Encapsulation trade-offs`
