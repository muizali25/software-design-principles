# Interview Cheatsheet

One-breath definitions plus a memorable example and a “do not overdo it” note.

| Principle | One-liner | Example in this repo | Don't overdo |
|-----------|-----------|----------------------|--------------|
| SRP | One cohesive reason to change | Order processor vs pricing/email/invoice | Tiny one-method classes |
| OCP | Extend without editing stable code | Payment processors via strategy | Plugin frameworks for two cases |
| LSP | Subtypes must honor the contract | Refund policies that stay substitutable | Inheritance that weakens guarantees |
| ISP | Small client-specific interfaces | Email/SMS/Push segregation | Interface per method |
| DIP | Policy depends on abstractions | Fulfillment injects repo/email | Interface around every DTO |
| DRY | Don't duplicate the same knowledge | Shared pricing calculator | Wrong abstraction from coincidental similarity |
| KISS | Simplest design that works | Plain discount calculator | Patterns as decoration |
| YAGNI | Don't build speculative features | Stock adjust/check only | “Future multi-warehouse” scaffolding |
| Composition > Inheritance | Assemble behaviors | Report source + formatter + exporter | Deep report class trees |
| Program to Interface | Depend on capabilities | `IProductCatalog` | Interface with one forever-implementation |
| Separation of Concerns | Keep layers/roles distinct | Onboarding orchestration | Ceremony layers with no benefit |
| Encapsulation | Protect invariants | Bank account deposit/withdraw | Public setters everywhere |
| Immutability | Values don't change after create | `Money` / order lines | Cloning pressure with no shared-mutation risk |
| Law of Demeter | Talk to neighbors | Shipping destination VO | Getters for every nested field “just in case” |
| Tell, Don't Ask | Ask objects to act | `order.Ship()` | God aggregates that know the world |
| Convention over Config | Sensible defaults by naming | `SalesReport` → `SalesReportFormatter` | Hidden magic with many exceptions |
| Fail Fast | Reject bad input early | Order intake validation | Crashing deep inside unrelated code without context |
| Single Level of Abstraction | One conceptual level per method | Payroll orchestration | Extracting methods that only rename noise |
| Explicit Dependencies | Collaborators are visible | Constructor-injected invoice service | Service locator / ambient context |
| Don't Repeat Knowledge | One source of business truth | `OrderStatus` + domain rules | Duplicate string status checks |

## Answer pattern

1. **Define** the principle in plain English.  
2. **Name the change cost** it reduces.  
3. **Give a small example** from this repo or production.  
4. **State a trade-off** or when you would skip it.

## Sound bites

* “Principles manage change; they are not trophies.”
* “Abstractions need a paying customer: variation, a boundary, or an invariant.”
* “If the next requirement is imaginary, YAGNI wins.”
* “If two teams own different reasons to change a class, SRP is talking to you.”
* “If a subtype surprises clients, LSP is broken—even if it compiles.”
