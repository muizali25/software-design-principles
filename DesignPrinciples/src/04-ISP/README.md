# Interface Segregation Principle: Focused Notification Channels

## Problem
Receipts are sent by email, while delivery updates may use SMS and push. A channel integration should promise only capabilities it supports.

In a production system, this requirement will change as the business evolves. The design should make adding a channel or replacing an email provider visible, local, and safe rather than allowing an unrelated workflow to become the change point.

## Bad Implementation
The bad code is centered on **EmailNotificationService implements INotificationService and throws for SMS, push, and fax.**

- The method or type has more responsibility than its stable role.
- `// PROBLEM: an email-only client must implement channels it cannot support.`
- Policy and mechanics are coupled, so the behavior is hard to replace or verify in isolation.

## Why Is This a Problem?
- **What is wrong?** The code combines concerns or places a rule outside its proper owner.
- **What will happen when requirements change?** adding a channel or replacing an email provider forces edits in existing workflow code and raises regression risk.
- **How does this affect testing?** Tests must work around hidden behavior or concrete collaborators instead of using focused fakes.
- **How does this affect maintainability?** It is difficult to identify a single owner for the rule, so changes take longer to locate and review.
- **How does this affect scalability?** More variants, callers, and integrations accumulate conditional logic or conflicting behavior.
- **What kind of change would force us to modify this code?** adding a channel or replacing an email provider.

## Principle
### Principle
Interface Segregation Principle organizes code so related behavior has a clear owner and changes have a limited blast radius.

### In simple words
Give each rule a clear home. Depend on the capability you need, keep public behavior honest, and do not build flexibility before it is needed.

### When to use it
Use it when clients need distinct subsets of a broad capability. It is especially valuable at domain, infrastructure, and integration boundaries.

### When NOT to use it
Avoid fragmenting an interface every consumer genuinely needs. The goal is clarity and a cheaper change, not the maximum number of classes.

## Solution
Small IEmailSender, ISmsSender, IPushSender, and IFaxSender interfaces let notifiers request only needed channels. The high-level workflow remains readable while each collaborator owns a focused responsibility.

## Before vs After
| Aspect | Before | After |
|---|---|---|
| Change location | Existing workflow is edited for adding a channel or replacing an email provider | The focused rule or collaborator changes locally |
| Dependencies | Policy and concrete mechanics are tangled | Collaboration is explicit and role-focused |
| Tests | Setup works around hidden behavior | A focused fake or direct unit test exercises the rule |
| Growth | New cases enlarge existing control flow | New behavior has an intentional extension or ownership point |

## Unit Tests
Tests use a fake IEmailSender to show OrderReceiptNotifier has no unrelated channel dependency. They document observable behavior rather than implementation trivia, so a safe refactor can retain confidence.

## Real-World Usage
Web APIs separate storage, cache, queue, and notification ports so consumers do not receive unused methods.

## Interview Questions
1. **Beginner: What does Interface Segregation Principle mean?** It means placing related behavior behind a clear owner so a change has a small, understandable impact.
2. **Practical: What is the smell in this example?** `EmailNotificationService implements INotificationService and throws for SMS, push, and fax` makes a policy or integration decision outside the correct focused boundary.
3. **Scenario-based: How would you handle adding a channel or replacing an email provider?** Change or add the focused collaborator or rule, then protect it with a targeted test instead of rewriting the whole workflow.
4. **Senior-level: How do you avoid over-design?** Add a seam only where variation, an external boundary, or a protected invariant is real; weigh it against the cost of indirection.
5. **When would you NOT use this principle?** Do not apply it mechanically when a direct, cohesive implementation is clearer and requirements are stable.

## Trade-offs
Applied excessively, Interface Segregation Principle can produce indirection, many small types, registration complexity, and longer navigation paths. Prefer the smallest design that clearly expresses the current change boundary; consolidate abstractions that do not buy independent evolution.

## Design Decision
- **Why did we introduce this abstraction?** We introduced channel-specific sender interfaces so the business rule has an explicit, testable home.
- **Why didn't we use another approach?** We did not retain one universal notifier because unsupported methods become runtime failures.
- **What would happen if the application became 10x larger?** new channels and consumers can be composed without widening contracts.

## Refactoring Commit Simulation
Commit 1: `feat: create naive 04-ISP implementation`  
Commit 2: `refactor: identify Interface Segregation Principle design problem`  
Commit 3: `refactor: apply Interface Segregation Principle to focused notification channels`  
Commit 4: `test: cover focused 04-ISP behavior`  
Commit 5: `docs: document Interface Segregation Principle trade-offs`
