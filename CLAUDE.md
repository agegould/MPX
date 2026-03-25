# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Run the app (HTTP on port 5226)
cd TaskManager && dotnet run

# Run with HTTPS (port 7199)
cd TaskManager && dotnet run --launch-profile https

# Build
cd TaskManager && dotnet build
```

## Architecture

Single-project Blazor Server app targeting .NET 10.0. All rendering uses `InteractiveServer` mode (`AddInteractiveServerComponents()` in `Program.cs`).

The app is a task manager — all business logic lives in one component: `Pages/Index.razor`. There are no services, no database, and no persistence; task state is held in-memory inside the component.

**Key types defined inline in `Index.razor`:**
- `TaskItem` — the data model with fields: `Title`, `Owner`, `Status`, `Details`, `DateCompleted`, `Condition`, `AutomationKey`, `Group`, `Type`
- `Status` enum — `Created`, `Ready`, `InProgress`, `Complete`, `Failed`, `NotApplicable`, `Rejected`

**Routing/layout chain:** `App.razor` → `Routes.razor` → `MainLayout.razor` (sidebar + content) → page components under `Pages/`

**`_Imports.razor`** pulls in `TaskManager` and `TaskManager.Shared` namespaces globally, so those don't need explicit `@using` in individual components.

**wwwroot** uses Bootstrap and Open Iconic (icon font) for styling — no npm/node build step.
