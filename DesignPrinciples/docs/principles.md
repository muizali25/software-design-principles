# Principles Overview

Short descriptions with links to the full examples.

## SOLID

| Principle | Intent | Example |
|-----------|--------|---------|
| [SRP](../src/01-SRP) | A unit should have one cohesive reason to change | Order total, persistence, receipt, and invoice split apart |
| [OCP](../src/02-OCP) | Open for extension, closed for modification | New payment methods without editing a switch statement |
| [LSP](../src/03-LSP) | Subtypes must be safely substitutable | Refund policies that preserve client expectations |
| [ISP](../src/04-ISP) | Prefer small, client-focused interfaces | Separate email, SMS, and push contracts |
| [DIP](../src/05-DIP) | High-level policy depends on abstractions | Fulfillment depends on repository/email interfaces |

## Simplicity and knowledge

| Principle | Intent | Example |
|-----------|--------|---------|
| [DRY](../src/06-DRY) | Avoid repeating the same knowledge | Shared pricing used by checkout and invoices |
| [KISS](../src/07-KISS) | Prefer the simplest adequate design | Percentage discounts without ceremony |
| [YAGNI](../src/08-YAGNI) | Build for evidenced needs | Inventory without speculative platform features |
| [Don't Repeat Knowledge](../src/20-DontRepeatKnowledge) | One authoritative business rule | Order status enum + domain methods |

## Structure and collaboration

| Principle | Intent | Example |
|-----------|--------|---------|
| [Composition over Inheritance](../src/09-CompositionOverInheritance) | Assemble independent behaviors | Report pipeline components |
| [Program to an Interface](../src/10-ProgramToInterface) | Depend on capabilities | Order service uses `IProductCatalog` |
| [Separation of Concerns](../src/11-SeparationOfConcerns) | Keep roles and layers distinct | Employee onboarding orchestration |
| [Encapsulation](../src/12-Encapsulation) | Protect state behind behavior | Bank account invariants |
| [Law of Demeter](../src/14-LawOfDemeter) | Avoid deep object graph digging | Shipping destination API |
| [Tell, Don't Ask](../src/15-TellDontAsk) | Put behavior with the data | `Order.Ship()` / `MarkPaid()` |
| [Explicit Dependencies](../src/19-ExplicitDependencies) | Make collaborators visible | Constructor-injected invoice service |

## Robustness and readability

| Principle | Intent | Example |
|-----------|--------|---------|
| [Immutability](../src/13-Immutability) | Prefer stable values | Immutable `Money` and order lines |
| [Convention over Configuration](../src/16-ConventionOverConfiguration) | Reduce repetitive wiring | Report formatter naming convention |
| [Fail Fast](../src/17-FailFast) | Reject invalid input early | Order intake validation |
| [Single Level of Abstraction](../src/18-SingleLevelOfAbstraction) | Keep methods at one level | Payroll orchestration |

## How these principles relate

They overlap on purpose:

* Explicit Dependencies supports DIP and testability.
* Encapsulation and Tell, Don't Ask protect domain invariants.
* DRY and Don't Repeat Knowledge both fight duplicated rules—one at the code level, one at the knowledge level.
* KISS and YAGNI constrain over-application of SOLID.

Use the **smallest combination** that clarifies the next change.
