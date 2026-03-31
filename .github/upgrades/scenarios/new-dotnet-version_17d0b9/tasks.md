# Office365.PacUtil .NET 10.0 Upgrade Tasks

## Overview

This document tracks the execution of the Office365.PacUtil project upgrade from .NET 8.0 to .NET 10.0. The single project will be upgraded in an atomic operation, followed by build verification.

**Progress**: 3/3 tasks complete (100%) ![0%](https://progress-bar.xyz/100)

---

## Tasks

### [✓] TASK-001: Verify prerequisites *(Completed: 2026-03-31 23:48)*
**References**: Plan §Phase 1 Entry Criteria

- [✓] (1) Verify .NET 10 SDK installed on development machine
- [✓] (2) .NET 10 SDK meets minimum requirements (**Verify**)

---

### [✓] TASK-002: Atomic framework and package upgrade with compilation fixes *(Completed: 2026-03-31 23:49)*
**References**: Plan §Phase 1, Plan §Project-by-Project Plans, Plan §Package Update Reference, Plan §Breaking Changes Catalog

- [✓] (1) Update TargetFramework in Office365.PacUtil.csproj from net8.0 to net10.0
- [✓] (2) TargetFramework updated to net10.0 (**Verify**)
- [✓] (3) Update 5 package references in Office365.PacUtil.csproj: Microsoft.Extensions.Configuration 8.0.0→10.0.5, Microsoft.Extensions.Configuration.Json 8.0.0→10.0.5, Microsoft.Extensions.Hosting 8.0.0→10.0.5, Newtonsoft.Json 13.0.3→13.0.4, System.Configuration.ConfigurationManager 8.0.0→10.0.5
- [✓] (4) All 5 package references updated (**Verify**)
- [✓] (5) Restore all dependencies via dotnet restore
- [✓] (6) All dependencies restored successfully (**Verify**)
- [✓] (7) Build solution and fix all compilation errors per Plan §Breaking Changes Catalog (focus: System.CommandLine API incompatibilities - 62 instances, ConfigurationErrorsException handling - 18 instances, HttpContent behavioral changes - 4 instances)
- [✓] (8) Solution builds with 0 errors (**Verify**)

---

### [✓] TASK-003: Final commit *(Completed: 2026-03-31 23:50)*
**References**: Plan §Source Control Strategy

- [✓] (1) Commit all changes with message: "feat: Upgrade Office365.PacUtil to .NET 10.0 - Update TargetFramework net8.0→net10.0, Update 5 packages to .NET 10-compatible versions, Fix System.CommandLine API incompatibilities (62 instances), Address ConfigurationErrorsException compatibility (18 instances), Validate HttpContent behavioral changes (4 instances)"

---





