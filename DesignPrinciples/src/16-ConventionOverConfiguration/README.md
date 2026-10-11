# Convention Over Configuration: Discoverable Report Formatters

## Problem
A reporting module needs a formatter for each report type. Repeating string-to-class mappings becomes fragile as types grow.

In a production system, this requirement will change as the business evolves. The design should make adding a formatter or renaming a report type visible, local, and safe rather than allowing an unrelated workflow to become the change point.

## Bad Implementation
The bad code is centered on **ReportFormatterConfiguration maintains a dictionary of report names and string class names.**

- The method or type has more responsibility than its stable role.
- `No // PROBLEM marker is present; duplicated naming configuration is the design smell.`
- Policy and mechanics are coupled, so the behavior is hard to replace or verify in isolation.

## Why Is This a Problem?
- **What is wrong?** The code combines concerns or places a rule outside its proper owner.
- **What will happen when requirements change?** adding a formatter or renaming a report type forces edits in existing workflow code and raises regression risk.
- **How does this affect testing?** Tests must work around hidden behavior or concrete collaborators instead of using focused fakes.
- **How does this affect maintainability?** It is difficult to identify a single owner for the rule, so changes take longer to locate and review.
- **How does this affect scalability?** More variants, callers, and integrations accumulate conditional logic or conflicting behavior.
- **What kind of change would force us to modify this code?** adding a formatter or renaming a report type.

## Principle
### Principle
Convention Over Configuration organizes code so related behavior has a clear owner and changes have a limited blast radius.

### In simple words
Give each rule a clear home. Depend on the capability you need, keep public behavior honest, and do not build flexibility before it is needed.

### When to use it
Use it when a stable obvious naming convention can replace repetitive metadata. It is especially valuable at domain, infrastructure, and integration boundaries.

### When NOT to use it
Avoid using convention when names are ambiguous or user-configurable. The goal is clarity and a cheaper change, not the maximum number of classes.

## Solution
ReportFormatterRegistry derives a report key from each IReportFormatter class name ending in ReportFormatter. The high-level workflow remains readable while each collaborator owns a focused responsibility.

## Before vs After
| Aspect | Before | After |
|---|---|---|
| Change location | Existing workflow is edited for adding a formatter or renaming a report type | The focused rule or collaborator changes locally |
| Dependencies | Policy and concrete mechanics are tangled | Collaboration is explicit and role-focused |
| Tests | Setup works around hidden behavior | A focused fake or direct unit test exercises the rule |
| Growth | New cases enlarge existing control flow | New behavior has an intentional extension or ownership point |

## Unit Tests
Tests prove SalesReportFormatter resolves by convention and unknown types fail clearly. They document observable behavior rather than implementation trivia, so a safe refactor can retain confidence.

## Real-World Usage
ASP.NET Core uses conventions for controller discovery, routing, and options binding.

## Interview Questions
1. **Beginner: What does Convention Over Configuration mean?** It means placing related behavior behind a clear owner so a change has a small, understandable impact.
2. **Practical: What is the smell in this example?** `ReportFormatterConfiguration maintains a dictionary of report names and string class names` makes a policy or integration decision outside the correct focused boundary.
3. **Scenario-based: How would you handle adding a formatter or renaming a report type?** Change or add the focused collaborator or rule, then protect it with a targeted test instead of rewriting the whole workflow.
4. **Senior-level: How do you avoid over-design?** Add a seam only where variation, an external boundary, or a protected invariant is real; weigh it against the cost of indirection.
5. **When would you NOT use this principle?** Do not apply it mechanically when a direct, cohesive implementation is clearer and requirements are stable.

## Trade-offs
Applied excessively, Convention Over Configuration can produce indirection, many small types, registration complexity, and longer navigation paths. Prefer the smallest design that clearly expresses the current change boundary; consolidate abstractions that do not buy independent evolution.

## Design Decision
- **Why did we introduce this abstraction?** We introduced the ReportFormatter naming convention and registry so the business rule has an explicit, testable home.
- **Why didn't we use another approach?** We did not retain string configuration because typos and renames are not compiler-safe.
- **What would happen if the application became 10x larger?** new formatters need almost no per-type metadata.

## Refactoring Commit Simulation
Commit 1: `feat: create naive 16-ConventionOverConfiguration implementation`  
Commit 2: `refactor: identify Convention Over Configuration design problem`  
Commit 3: `refactor: apply Convention Over Configuration to discoverable report formatters`  
Commit 4: `test: cover focused 16-ConventionOverConfiguration behavior`  
Commit 5: `docs: document Convention Over Configuration trade-offs`
