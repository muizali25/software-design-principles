# YAGNI: Right-Sized Inventory Service

## Problem
A single warehouse needs to adjust available stock and prevent negative quantities. Plugins, cache invalidation, events, and multi-warehouse synchronization are not approved requirements.

In a production system, this requirement will change as the business evolves. The design should make enabling a real second warehouse or a measured cache requirement visible, local, and safe rather than allowing an unrelated workflow to become the change point.

## Bad Implementation
The bad code is centered on **InventoryService requires plugin, cache, event bus, and synchronizer dependencies before its basic stock operation.**

- The method or type has more responsibility than its stable role.
- `// PROBLEM: A single-warehouse service pays for features it does not use.`
- Policy and mechanics are coupled, so the behavior is hard to replace or verify in isolation.

## Why Is This a Problem?
- **What is wrong?** The code combines concerns or places a rule outside its proper owner.
- **What will happen when requirements change?** enabling a real second warehouse or a measured cache requirement forces edits in existing workflow code and raises regression risk.
- **How does this affect testing?** Tests must work around hidden behavior or concrete collaborators instead of using focused fakes.
- **How does this affect maintainability?** It is difficult to identify a single owner for the rule, so changes take longer to locate and review.
- **How does this affect scalability?** More variants, callers, and integrations accumulate conditional logic or conflicting behavior.
- **What kind of change would force us to modify this code?** enabling a real second warehouse or a measured cache requirement.

## Principle
### Principle
YAGNI organizes code so related behavior has a clear owner and changes have a limited blast radius.

### In simple words
Give each rule a clear home. Depend on the capability you need, keep public behavior honest, and do not build flexibility before it is needed.

### When to use it
Use it when a proposed capability has no current consumer or acceptance criterion. It is especially valuable at domain, infrastructure, and integration boundaries.

### When NOT to use it
Avoid ignoring known contractual or regulatory requirements. The goal is clarity and a cheaper change, not the maximum number of classes.

## Solution
InventoryService keeps case-insensitive in-memory stock and implements only GetAvailable and validated Adjust. The high-level workflow remains readable while each collaborator owns a focused responsibility.

## Before vs After
| Aspect | Before | After |
|---|---|---|
| Change location | Existing workflow is edited for enabling a real second warehouse or a measured cache requirement | The focused rule or collaborator changes locally |
| Dependencies | Policy and concrete mechanics are tangled | Collaboration is explicit and role-focused |
| Tests | Setup works around hidden behavior | A focused fake or direct unit test exercises the rule |
| Growth | New cases enlarge existing control flow | New behavior has an intentional extension or ownership point |

## Unit Tests
Tests show stock changes are retained and negative stock is rejected. They document observable behavior rather than implementation trivia, so a safe refactor can retain confidence.

## Real-World Usage
Start a Web API or microservice with its actual boundary, then add queues and caches when evidence requires them.

## Interview Questions
1. **Beginner: What does YAGNI mean?** It means placing related behavior behind a clear owner so a change has a small, understandable impact.
2. **Practical: What is the smell in this example?** `InventoryService requires plugin, cache, event bus, and synchronizer dependencies before its basic stock operation` makes a policy or integration decision outside the correct focused boundary.
3. **Scenario-based: How would you handle enabling a real second warehouse or a measured cache requirement?** Change or add the focused collaborator or rule, then protect it with a targeted test instead of rewriting the whole workflow.
4. **Senior-level: How do you avoid over-design?** Add a seam only where variation, an external boundary, or a protected invariant is real; weigh it against the cost of indirection.
5. **When would you NOT use this principle?** Do not apply it mechanically when a direct, cohesive implementation is clearer and requirements are stable.

## Trade-offs
Applied excessively, YAGNI can produce indirection, many small types, registration complexity, and longer navigation paths. Prefer the smallest design that clearly expresses the current change boundary; consolidate abstractions that do not buy independent evolution.

## Design Decision
- **Why did we introduce this abstraction?** We introduced no abstraction beyond current inventory state so the business rule has an explicit, testable home.
- **Why didn't we use another approach?** We did not retain a speculative plugin platform because it adds failure modes now.
- **What would happen if the application became 10x larger?** at 10x load, measure then add a targeted adapter.

## Refactoring Commit Simulation
Commit 1: `feat: create naive 08-YAGNI implementation`  
Commit 2: `refactor: identify YAGNI design problem`  
Commit 3: `refactor: apply YAGNI to right-sized inventory service`  
Commit 4: `test: cover focused 08-YAGNI behavior`  
Commit 5: `docs: document YAGNI trade-offs`
