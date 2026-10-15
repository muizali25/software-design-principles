# Repository Architecture

## Goal

Keep each principle **independently understandable**. A reader should be able to open one folder, read the README, and run the demo without learning the rest of the repository.

## Solution layout

```text
DesignPrinciples.sln
├── src/NN-PrincipleName/     # Console app + Bad/ + Good/
└── docs/                     # Cross-cutting portfolio docs
```

* **20 projects** total — one example per principle
* Each source project targets **.NET 9**
* No shared domain library, no database, no external HTTP clients

## Independence rules

1. Examples do **not** reference each other.
2. Namespaces are scoped per principle (`DesignPrinciples.SRP.Good`, etc.).
3. Infrastructure (email, SQL, SMTP) is simulated with console adapters or in-memory fakes.
4. `Program.cs` is a short demonstration, not a full application host.

## Example anatomy

| Path | Purpose |
|------|---------|
| `Bad/` | Realistic code a developer might write under deadline pressure |
| `Good/` | Focused refactor that applies the principle |
| `Program.cs` | Side-by-side demo output |
| `README.md` | Problem → principle → solution → trade-offs |

## Why not one giant application?

A single e-commerce monolith containing every principle would:

* force readers to learn unrelated modules first
* blur which design decision belongs to which principle
* make demos harder to isolate

Small examples optimize for **teaching clarity** and **portfolio navigation**.

## How to navigate

1. Start at the root [README](../README.md).
2. Pick one principle that matches what you want to study (payments → OCP, DI → DIP/Explicit Dependencies).
3. Open `Bad/`, then `Good/`.
4. Use the README **Trade-offs** and **Design Decision** sections for senior-level judgment.

## Scaling this repository

If the collection grew further, keep the same pattern:

* one folder per idea
* one demo
* no cross-project coupling unless teaching composition of principles explicitly
