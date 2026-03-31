# .NET 10.0 Upgrade Plan - Office365.PacUtil

## Table of Contents

- [Executive Summary](#executive-summary)
- [Migration Strategy](#migration-strategy)
- [Detailed Dependency Analysis](#detailed-dependency-analysis)
- [Project-by-Project Plans](#project-by-project-plans)
  - [Office365.PacUtil.csproj](#office365pacutilcsproj)
- [Package Update Reference](#package-update-reference)
- [Breaking Changes Catalog](#breaking-changes-catalog)
- [Risk Management](#risk-management)
- [Testing & Validation Strategy](#testing--validation-strategy)
- [Complexity & Effort Assessment](#complexity--effort-assessment)
- [Source Control Strategy](#source-control-strategy)
- [Success Criteria](#success-criteria)

---

## Executive Summary

### Scenario Description
Upgrade Office365.PacUtil project from .NET 8.0 to .NET 10.0 (Long Term Support).

### Scope
**Single Project Solution:**
- **Office365.PacUtil.csproj**: Console application (DotNetCoreApp, SDK-style)
  - Current: net8.0
  - Target: net10.0
  - 1,093 lines of code
  - 10 code files
  - 7 NuGet packages (5 require updates)
  - 66+ estimated LOC to modify (~6.0% of codebase)

### Key Metrics
- **Total Projects**: 1
- **Dependency Depth**: 0 (standalone project)
- **Risk Indicators**: 
  - ✅ No security vulnerabilities
  - ✅ No circular dependencies
  - ✅ SDK-style project
  - ⚠️ 62 source-incompatible API issues (primarily System.CommandLine beta APIs)
  - ⚠️ 4 behavioral changes requiring runtime testing
- **Complexity**: 🟢 **Simple** - Small single-project solution with clear upgrade path

### Selected Strategy
**All-At-Once Strategy** - Single atomic upgrade operation.

**Rationale**: 
- Only 1 project (simplest possible scenario)
- No inter-project dependencies
- Small codebase (~1k LOC)
- Clear package upgrade path
- All packages have target framework versions available
- Low risk profile

### Critical Issues
- **API Incompatibilities**: 62 source-incompatible API calls, primarily in System.CommandLine (beta package)
- **Legacy Configuration**: 18 issues related to System.Configuration.ConfigurationErrorsException usage
- **Package Updates**: 5 packages need version updates for .NET 10 compatibility

### Iteration Strategy
**Simple Solution Approach**: 2-3 detail iterations (batch all sections together due to single project simplicity)

---

## Migration Strategy

### Approach Selection: All-At-Once Strategy

**Decision**: Upgrade all components of the solution simultaneously in a single atomic operation.

**Justification**:
- ✅ **Single Project**: Only 1 project to upgrade (Office365.PacUtil.csproj)
- ✅ **No Dependencies**: Zero inter-project dependencies
- ✅ **Small Codebase**: 1,093 LOC is manageable for single-operation upgrade
- ✅ **Clear Package Path**: All 5 package updates have known target versions
- ✅ **Low Risk**: No security vulnerabilities, SDK-style project, straightforward upgrade
- ✅ **Fast Completion**: Single atomic operation minimizes total timeline

**Alternative Considered**: Incremental migration is not applicable for single-project solutions.

### All-At-Once Strategy Rationale

**Why This Approach Fits:**
1. **Atomic Nature**: With only one project, there are no intermediate states to manage
2. **No Multi-Targeting**: Single project eliminates cross-version compatibility concerns
3. **Simplified Testing**: Single build/test cycle validates entire upgrade
4. **Clean Execution**: All changes applied together, no coordination complexity

**Execution Characteristics**:
- ✅ Fastest possible completion time
- ✅ No multi-targeting complexity
- ✅ Single comprehensive testing phase
- ✅ Simple rollback (single commit)

### Dependency-Based Ordering

**Not Applicable** - Single project has no dependency ordering requirements.

The upgrade proceeds as a single unified operation:
1. Update TargetFramework property in Office365.PacUtil.csproj
2. Update all 5 PackageReference versions simultaneously
3. Restore dependencies
4. Build and fix all compilation errors in one pass
5. Verify solution builds with 0 errors

### Parallel vs Sequential Execution

**Sequential Within Atomic Operation:**
The upgrade follows this mandatory sequence:
1. Project file updates (TargetFramework + PackageReferences)
2. Dependency restoration
3. Compilation + error fixing
4. Validation

**Rationale**: Each step depends on the previous step's completion. Package updates require project file changes; compilation requires restored packages; validation requires successful build.

### Phase Definitions

**Single Phase: Atomic Upgrade**

**Phase 1 Scope:**
- Update Office365.PacUtil.csproj TargetFramework: net8.0 → net10.0
- Update 5 NuGet packages to .NET 10-compatible versions
- Fix 62 source-incompatible API issues
- Address 4 behavioral changes
- Build solution to 0 errors
- Execute validation tests

**Phase 1 Entry Criteria:**
- ✅ .NET 10 SDK installed
- ✅ Current solution builds successfully on net8.0
- ✅ Working branch created (upgrade-to-NET10)

**Phase 1 Exit Criteria:**
- ✅ Project targets net10.0
- ✅ All packages updated to suggested versions
- ✅ Solution builds with 0 errors and 0 warnings
- ✅ All tests pass (if applicable)
- ✅ Application runs successfully

---

## Detailed Dependency Analysis

### Dependency Graph Summary

**Standalone Project Structure:**
```
Office365.PacUtil.csproj (net8.0 → net10.0)
└── (No project dependencies)
```

This is the simplest possible dependency structure: a single standalone project with zero inter-project dependencies.

### Project Groupings

**Single Migration Phase:**
- **Phase 1 (Atomic Upgrade)**: Office365.PacUtil.csproj
  - No dependencies to coordinate
  - All changes occur in single operation

### Critical Path

**No Critical Path Complexity:**
- Single project = single point of execution
- No dependency ordering required
- No blocking relationships

### Circular Dependencies

**None** - Not applicable for single-project solution.

### Migration Order Rationale

With only one project, the migration order is trivial:
1. Update Office365.PacUtil.csproj target framework to net10.0
2. Update all package references simultaneously
3. Fix compilation errors
4. Validate and test

The All-At-Once strategy is not just recommended—it's the only logical approach for a single-project solution.

---

## Project-by-Project Plans

### Office365.PacUtil.csproj

**Current State**: 
- Target Framework: net8.0
- Project Type: DotNetCoreApp (Console Application)
- SDK-Style: True
- Lines of Code: 1,093
- Code Files: 10 (3 with API incidents)
- Dependencies: 0 project dependencies
- Dependants: 0 projects depend on this
- Package Count: 7 NuGet packages
- Risk Level: 🟢 Low

**Current Packages**:
- Microsoft.Extensions.Configuration 8.0.0
- Microsoft.Extensions.Configuration.Json 8.0.0
- Microsoft.Extensions.Hosting 8.0.0
- Newtonsoft.Json 13.0.3
- System.CommandLine 2.0.0-beta4.22272.1
- System.CommandLine.Hosting 0.4.0-alpha.22272.1
- System.Configuration.ConfigurationManager 8.0.0

**Target State**: 
- Target Framework: net10.0
- Updated Packages: 5 packages upgraded to .NET 10-compatible versions

**Migration Steps**:

#### 1. Prerequisites
- ✅ .NET 10 SDK installed on development machine
- ✅ Working branch `upgrade-to-NET10` created and checked out
- ✅ Current project builds successfully on net8.0
- ✅ All changes committed or stashed

#### 2. Project File Update

**File**: `Office365.PacUtil.csproj`

**Change TargetFramework**:
```xml
<!-- Before -->
<TargetFramework>net8.0</TargetFramework>

<!-- After -->
<TargetFramework>net10.0</TargetFramework>
```

#### 3. Package Reference Updates

Update the following PackageReference elements in `Office365.PacUtil.csproj`:

| Package | Current Version | Target Version | Reason |
|---------|----------------|----------------|---------|
| Microsoft.Extensions.Configuration | 8.0.0 | 10.0.5 | .NET 10 compatibility |
| Microsoft.Extensions.Configuration.Json | 8.0.0 | 10.0.5 | .NET 10 compatibility |
| Microsoft.Extensions.Hosting | 8.0.0 | 10.0.5 | .NET 10 compatibility |
| Newtonsoft.Json | 13.0.3 | 13.0.4 | Minor update (patch version) |
| System.Configuration.ConfigurationManager | 8.0.0 | 10.0.5 | .NET 10 compatibility |

**Packages Remaining Unchanged** (Already Compatible):
- System.CommandLine 2.0.0-beta4.22272.1 (beta package - compatible)
- System.CommandLine.Hosting 0.4.0-alpha.22272.1 (alpha package - compatible)

#### 4. Expected Breaking Changes

**System.CommandLine API Changes** (62 source-incompatible calls):

**Affected APIs:**
- `CommandLineBuilder` class and constructor
- `ICommandHandler` interface
- `Option.IsRequired` property
- `Command.Add(Option)` method
- `Command.AddCommand(Command)` method
- `Command` class and constructor
- `CommandHandler` class
- `Command.Handler` property
- `CommandLineBuilderExtensions` methods
- `RootCommand` class and constructor
- `HostingExtensions.UseHost` method
- `Parser` class and extensions

**Impact**: These APIs are in beta packages (System.CommandLine 2.0.0-beta4) and may have signature or behavior changes between .NET 8 and .NET 10. The package itself is marked as compatible, but beta APIs are not guaranteed stable.

**Resolution Strategy**:
1. Restore packages and attempt build
2. Review compilation errors for System.CommandLine API usage
3. Consult System.CommandLine documentation for .NET 10
4. Update API calls to match current signatures
5. Consider upgrading to stable System.CommandLine release if available

**System.Configuration.ConfigurationErrorsException** (18 incompatible calls):

**Affected APIs:**
- `ConfigurationErrorsException` type and constructor

**Impact**: Legacy configuration exception used in modern .NET Core+ context. The System.Configuration.ConfigurationManager package (upgrading to 10.0.5) provides compatibility bridge.

**Resolution Strategy**:
1. Verify System.Configuration.ConfigurationManager 10.0.5 maintains exception compatibility
2. Build and test exception handling paths
3. If issues arise, consider migrating to Microsoft.Extensions.Configuration exception patterns

**System.Net.Http.HttpContent Behavioral Changes** (4 instances):

**Impact**: Behavioral changes in HttpContent class between .NET 8 and .NET 10.

**Resolution Strategy**:
1. Review .NET 10 breaking changes documentation for HttpContent
2. Test HTTP operations thoroughly
3. Validate response handling and content reading
4. Update code if behavioral assumptions have changed

#### 5. Code Modifications

**Files with API Incidents** (3 files):
- Review and update System.CommandLine API usages
- Verify ConfigurationErrorsException handling
- Test HttpContent usage patterns

**Specific Areas Requiring Review**:
1. **Command-line setup code**: Update CommandLineBuilder, RootCommand, Command construction
2. **Command handlers**: Verify ICommandHandler implementations and Command.Handler assignments
3. **Option definitions**: Check Option.IsRequired usage and Command.Add calls
4. **Host integration**: Review HostingExtensions.UseHost configuration
5. **Error handling**: Verify ConfigurationErrorsException catch blocks
6. **HTTP operations**: Test HttpContent reading and disposal

**Configuration Updates**:
- No appsettings.json changes expected
- Verify application configuration loading works correctly

#### 6. Testing Strategy

**Build Validation**:
- ✅ Solution builds with 0 errors
- ✅ Solution builds with 0 warnings
- ✅ NuGet package restore succeeds
- ✅ No dependency conflicts

**Functional Testing**:
- ✅ Command-line parsing works correctly (all commands, options, arguments)
- ✅ Command handlers execute as expected
- ✅ Help text displays correctly
- ✅ Configuration loading functions properly
- ✅ Error handling paths execute correctly
- ✅ HTTP operations complete successfully
- ✅ PAC file generation works (core functionality)

**Regression Testing**:
- Test all command-line scenarios
- Verify output matches expected format
- Validate proxy configuration generation
- Test error conditions and exception handling

**Behavioral Change Validation**:
- Specifically test HttpContent usage for any behavioral differences
- Verify application behavior matches .NET 8 version

#### 7. Validation Checklist

- [ ] Project file updated: TargetFramework = net10.0
- [ ] 5 packages updated to target versions
- [ ] `dotnet restore` completes successfully
- [ ] `dotnet build` completes with 0 errors
- [ ] `dotnet build` completes with 0 warnings
- [ ] No NuGet package conflicts
- [ ] All command-line commands execute correctly
- [ ] All command-line options work as expected
- [ ] Configuration loading succeeds
- [ ] Error handling paths tested
- [ ] HTTP operations validated
- [ ] PAC file generation produces correct output
- [ ] No behavioral regressions detected
- [ ] Application runs successfully end-to-end

---

## Package Update Reference

### Package Updates Required

All package updates apply to: **Office365.PacUtil.csproj**

| Package | Current Version | Target Version | Update Reason | Projects Affected |
|---------|----------------|----------------|---------------|-------------------|
| Microsoft.Extensions.Configuration | 8.0.0 | 10.0.5 | .NET 10 compatibility - framework alignment | 1 |
| Microsoft.Extensions.Configuration.Json | 8.0.0 | 10.0.5 | .NET 10 compatibility - framework alignment | 1 |
| Microsoft.Extensions.Hosting | 8.0.0 | 10.0.5 | .NET 10 compatibility - framework alignment | 1 |
| Newtonsoft.Json | 13.0.3 | 13.0.4 | Recommended upgrade - patch version update | 1 |
| System.Configuration.ConfigurationManager | 8.0.0 | 10.0.5 | .NET 10 compatibility - framework alignment | 1 |

### Packages Remaining Unchanged

| Package | Version | Reason |
|---------|---------|---------|
| System.CommandLine | 2.0.0-beta4.22272.1 | Already compatible with .NET 10 (beta package) |
| System.CommandLine.Hosting | 0.4.0-alpha.22272.1 | Already compatible with .NET 10 (alpha package) |

### Update Categories

#### Microsoft.Extensions.* Framework Packages (3 packages)
**Scope**: Configuration, Hosting infrastructure  
**Current**: 8.0.0  
**Target**: 10.0.5  
**Impact**: Low - Well-tested upgrade path from .NET 8 to .NET 10  
**Notes**: These packages move in lockstep with .NET framework versions

#### System.Configuration Package (1 package)
**Scope**: Legacy configuration compatibility bridge  
**Current**: 8.0.0  
**Target**: 10.0.5  
**Impact**: Low-Medium - Provides ConfigurationErrorsException compatibility  
**Notes**: Upgrade maintains backward compatibility for legacy configuration patterns

#### Third-Party Packages (1 package)
**Scope**: JSON serialization  
**Current**: 13.0.3  
**Target**: 13.0.4  
**Impact**: Low - Minor patch version update  
**Notes**: Newtonsoft.Json is stable and widely used

#### Beta/Alpha Packages (2 packages)
**Scope**: Command-line parsing and hosting integration  
**Status**: No update required (already compatible)  
**Impact**: Medium - Beta/Alpha APIs may have instability  
**Notes**: Assessment confirms compatibility, but thorough testing required due to beta status

### Package Update Implementation

**All packages updated simultaneously** in single atomic operation:

1. Open `Office365.PacUtil.csproj`
2. Locate each PackageReference element
3. Update Version attribute to target version
4. Save file
5. Run `dotnet restore`
6. Proceed to build and validation

**Example**:
```xml
<!-- Before -->
<PackageReference Include="Microsoft.Extensions.Configuration" Version="8.0.0" />

<!-- After -->
<PackageReference Include="Microsoft.Extensions.Configuration" Version="10.0.5" />
```

---

## Breaking Changes Catalog

### Overview

The assessment identified **66 API issues** across **3 code files**:
- 62 Source-Incompatible API calls
- 4 Behavioral Changes

**Estimated Impact**: 66+ lines of code requiring review/modification (~6% of codebase)

### Source-Incompatible API Changes (62 instances)

#### System.CommandLine API Changes (44 instances)

**Risk Level**: 🟡 Medium  
**Reason**: Beta package (2.0.0-beta4) - APIs not guaranteed stable across .NET versions

**Affected Types and Members**:

| API | Count | Category | Expected Issue |
|-----|-------|----------|----------------|
| `CommandLineBuilder` | 6 | Type/Constructor | Possible signature or namespace changes |
| `ICommandHandler` | 4 | Interface | Possible contract changes |
| `Option.IsRequired` | 4 | Property | Possible API surface changes |
| `Command.Add(Option)` | 4 | Method | Possible signature changes |
| `Command.AddCommand(Command)` | 3 | Method | Possible signature changes |
| `Command` | 3 | Type/Constructor | Possible signature changes |
| `CommandHandler` | 2 | Type | Possible API changes |
| `Command.Handler` | 2 | Property | Possible type or usage changes |
| `CommandLineBuilderExtensions` | 2 | Extension Methods | Possible method changes |
| `CommandLineBuilder` (ctor) | 1 | Constructor | Possible parameter changes |
| `RootCommand` | 1 | Type | Possible API changes |
| `RootCommand` (ctor) | 1 | Constructor | Possible parameter changes |
| `HostingExtensions` | 1 | Type | Possible API changes |
| `HostingExtensions.UseHost` | 1 | Method | Possible signature changes |
| `CommandLineBuilderExtensions.UseDefaults` | 1 | Extension Method | Possible changes |
| `CommandLineBuilderExtensions.CancelOnProcessTermination` | 1 | Extension Method | Possible changes |
| `Parser` | 1 | Type | Possible API changes |
| `CommandLineBuilder.Build` | 1 | Method | Possible signature changes |
| `ParserExtensions` | 1 | Type | Possible API changes |
| `ParserExtensions.InvokeAsync` | 1 | Method | Possible signature changes |

**Resolution Approach**:
1. **Build First**: Restore packages and attempt build to identify actual compilation errors
2. **Review Errors**: Examine compiler messages for specific API mismatches
3. **Consult Documentation**: Check System.CommandLine documentation for .NET 10 compatibility
4. **Update Code**: Modify API calls to match current signatures
5. **Consider Upgrade**: Evaluate upgrading to stable System.CommandLine release if available

**Common Fix Patterns** (expected based on beta API evolution):
- Update command/option builder syntax
- Adjust handler registration patterns
- Modify fluent API chains
- Update method signatures for extension methods

#### System.Configuration API Changes (18 instances)

**Risk Level**: 🟢 Low  
**Reason**: Compatibility package upgrade (ConfigurationManager 8.0.0 → 10.0.5)

**Affected API**:

| API | Count | Category | Expected Issue |
|-----|-------|----------|----------------|
| `ConfigurationErrorsException` | 9 | Type | Possible type availability or namespace changes |
| `ConfigurationErrorsException(string)` | 9 | Constructor | Possible constructor signature changes |

**Resolution Approach**:
1. **Verify Package**: Confirm System.Configuration.ConfigurationManager 10.0.5 includes ConfigurationErrorsException
2. **Test Exception Paths**: Execute code paths that throw/catch these exceptions
3. **Fallback**: If compatibility issues arise, migrate to Microsoft.Extensions.Configuration exception patterns

**Expected Outcome**: Low risk - ConfigurationManager package explicitly provides backward compatibility for legacy configuration APIs.

### Behavioral Changes (4 instances)

#### System.Net.Http.HttpContent Behavioral Changes

**Risk Level**: 🟡 Medium  
**Reason**: Runtime behavior changes require testing, not just compilation

**Affected API**:

| API | Count | Impact |
|-----|-------|--------|
| `HttpContent` | 4 | Behavioral changes in content handling between .NET 8 and .NET 10 |

**Potential Behavioral Differences**:
- Content reading patterns (stream vs. buffer behavior)
- Disposal semantics
- Header handling
- Content encoding behavior

**Resolution Approach**:
1. **Review Documentation**: Check .NET 10 breaking changes for HttpContent
2. **Test HTTP Operations**: Execute all HTTP request/response scenarios
3. **Validate Content Reading**: Verify content reading methods produce expected results
4. **Check Disposal**: Ensure proper resource cleanup
5. **Update Logic**: Modify code if behavioral assumptions have changed

**Testing Focus**:
- HTTP GET/POST operations
- Response content reading
- Header handling
- Error scenarios
- Resource disposal

### Breaking Changes by Technology

| Technology | Issues | Percentage | Migration Complexity |
|------------|--------|-----------|---------------------|
| System.CommandLine (Beta APIs) | 44 | 66.7% | Medium - Beta API evolution |
| Legacy Configuration System | 18 | 27.3% | Low - Compatibility package |
| HTTP Client | 4 | 6.0% | Low-Medium - Behavioral testing |

### Files Requiring Modification

**3 files with API incidents** (specific file names not provided in assessment):
- Review all files using System.CommandLine
- Review all files using ConfigurationErrorsException
- Review all files using HttpContent

**⚠️ Note**: Assessment does not specify exact file paths. During execution, use IDE/compiler errors to identify specific files requiring changes.

### Breaking Change Resolution Strategy

**Phase 1: Compilation**
1. Update project file and packages
2. Restore dependencies
3. Build solution
4. **Catalog compilation errors** - Document each error with file, line, and API

**Phase 2: Fix Source Incompatibilities**
1. Address System.CommandLine API mismatches (highest volume)
2. Verify ConfigurationErrorsException availability
3. Fix any other compilation errors
4. Build to 0 errors

**Phase 3: Test Behavioral Changes**
1. Execute comprehensive tests
2. Focus on HttpContent usage scenarios
3. Validate command-line functionality
4. Test configuration error handling
5. Address any runtime issues discovered

**Phase 4: Validation**
1. Run all tests
2. Perform end-to-end application testing
3. Verify no behavioral regressions
4. Document any behavioral differences observed

---

## Risk Management

### Risk Assessment

**Overall Risk Level**: 🟢 **Low**

### High-Risk Changes

**None Identified** - This upgrade presents no high-risk changes.

### Medium-Risk Changes

| Project | Risk Area | Description | Mitigation |
|---------|-----------|-------------|------------|
| Office365.PacUtil.csproj | System.CommandLine API Changes | 62 source-incompatible API calls in beta package (2.0.0-beta4) - API surface may have changed between .NET 8 and .NET 10 | Review System.CommandLine documentation for .NET 10; consider updating to stable release if available; test all command-line functionality thoroughly |
| Office365.PacUtil.csproj | Configuration Exception Handling | 18 uses of ConfigurationErrorsException which has limited support in .NET Core+ | Verify System.Configuration.ConfigurationManager package (upgrading to 10.0.5) maintains compatibility; test error handling paths |
| Office365.PacUtil.csproj | HttpContent Behavioral Changes | 4 behavioral changes in System.Net.Http.HttpContent | Review behavioral change documentation; test HTTP operations; validate response handling |

### Low-Risk Changes

| Project | Risk Area | Description | Mitigation |
|---------|-----------|-------------|------------|
| Office365.PacUtil.csproj | Microsoft.Extensions.* Packages | Standard Microsoft packages upgrading from 8.0.0 to 10.0.5 | Well-tested upgrade path; unlikely to cause issues |
| Office365.PacUtil.csproj | Newtonsoft.Json | Minor version update 13.0.3 → 13.0.4 | Patch version upgrade; minimal risk |

### Security Vulnerabilities

**None** - Assessment found no security vulnerabilities in current packages.

### Contingency Plans

**If System.CommandLine incompatibilities block upgrade:**
1. **Alternative 1**: Check for newer stable release of System.CommandLine compatible with .NET 10
2. **Alternative 2**: Migrate to System.CommandLine 2.0 stable release (if available)
3. **Alternative 3**: Implement minimal adapter layer to isolate breaking changes
4. **Rollback**: Revert to net8.0 if blocking issues cannot be resolved within reasonable effort

**If ConfigurationErrorsException compatibility issues arise:**
1. Verify System.Configuration.ConfigurationManager 10.0.5 documentation for breaking changes
2. Consider migrating to modern Microsoft.Extensions.Configuration exception patterns
3. Implement custom exception wrapper if necessary

**If behavioral changes cause test failures:**
1. Update tests to accommodate new .NET 10 behaviors
2. Review .NET 10 breaking changes documentation for HttpContent
3. Adjust application logic if necessary to work with new behaviors

---

## Testing & Validation Strategy

### Multi-Level Testing Approach

Given the single-project structure, testing follows a linear progression from compilation to comprehensive validation.

### Level 1: Compilation Validation

**Objective**: Ensure solution compiles cleanly with .NET 10

**Steps**:
1. Run `dotnet restore` - verify package restoration succeeds
2. Run `dotnet build` - verify build completes
3. Address all compilation errors (focus on System.CommandLine API mismatches)
4. Rebuild until 0 errors achieved
5. Address all warnings (target 0 warnings)

**Success Criteria**:
- ✅ `dotnet restore` exits with code 0
- ✅ No package conflicts reported
- ✅ `dotnet build` exits with code 0
- ✅ 0 compilation errors
- ✅ 0 warnings (or all warnings documented as acceptable)

### Level 2: Unit Testing (if applicable)

**Objective**: Verify individual components function correctly

**Scope**:
- Test command-line parsing logic
- Test configuration loading
- Test PAC file generation logic
- Test HTTP operations
- Test error handling

**Success Criteria**:
- ✅ All existing unit tests pass
- ✅ No test failures introduced by upgrade
- ✅ Test coverage maintained

**⚠️ Note**: Assessment does not indicate presence of unit test project. If no tests exist, proceed to integration testing.

### Level 3: Integration Testing

**Objective**: Verify components work together correctly

**Command-Line Functionality**:
- ✅ Help command displays correctly (`--help`)
- ✅ All defined commands execute successfully
- ✅ All command options parse correctly
- ✅ Required option validation works
- ✅ Optional parameters handle defaults correctly
- ✅ Command routing works as expected
- ✅ Error messages display appropriately

**Configuration Loading**:
- ✅ appsettings.json loads correctly
- ✅ Configuration values accessible
- ✅ Configuration error handling works
- ✅ ConfigurationErrorsException thrown appropriately (if applicable)

**HTTP Operations**:
- ✅ HTTP requests complete successfully
- ✅ Response content reads correctly
- ✅ Headers handled properly
- ✅ Error scenarios handled correctly

**PAC File Generation** (Core Functionality):
- ✅ PAC template loads correctly
- ✅ Office 365 endpoint data retrieves successfully
- ✅ PAC file generates with correct syntax
- ✅ Generated PAC file contains expected rules
- ✅ Output file writes successfully

### Level 4: End-to-End Testing

**Objective**: Validate complete application workflows

**Test Scenarios**:

1. **Scenario 1: Generate PAC File**
   - Execute command to generate PAC file
   - Verify file created
   - Validate file contents
   - Confirm output matches expected format

2. **Scenario 2: Report Command** (if `--report` option exists)
   - Execute report command
   - Verify report generates
   - Validate report format
   - Confirm data accuracy

3. **Scenario 3: Error Handling**
   - Test invalid command options
   - Test missing required parameters
   - Test network failure scenarios
   - Verify error messages are clear

4. **Scenario 4: Help Documentation**
   - Display help for root command
   - Display help for each subcommand
   - Verify help text is correct

**Success Criteria**:
- ✅ All scenarios execute without errors
- ✅ Application behavior matches .NET 8 version
- ✅ Output quality maintained
- ✅ User experience unchanged

### Level 5: Behavioral Change Validation

**Objective**: Specifically test areas with known behavioral changes

**HttpContent Behavioral Changes**:
- ✅ Test all code paths using HttpContent
- ✅ Verify content reading produces expected results
- ✅ Validate header handling
- ✅ Test content disposal/cleanup
- ✅ Compare behavior with .NET 8 version (document differences)

**System.CommandLine API Usage**:
- ✅ Thoroughly test all command-line scenarios
- ✅ Verify option parsing matches expected behavior
- ✅ Validate command routing
- ✅ Test handler execution

**Configuration Exception Handling**:
- ✅ Trigger configuration errors (invalid config)
- ✅ Verify ConfigurationErrorsException thrown and caught correctly
- ✅ Validate error messages

**Success Criteria**:
- ✅ No unexpected behavioral differences
- ✅ All behavioral changes documented
- ✅ Application functionality maintained

### Regression Testing

**Objective**: Ensure upgrade introduces no regressions

**Approach**:
1. **Baseline Capture**: Document .NET 8 behavior (if not already documented)
2. **Comparison Testing**: Execute identical scenarios on .NET 10 version
3. **Difference Analysis**: Document any behavioral differences
4. **Validation**: Confirm differences are expected or acceptable

**Key Areas**:
- Command-line parsing accuracy
- PAC file generation quality
- HTTP response handling
- Error message consistency
- Performance characteristics (optional)

### Testing Checklist

**Pre-Testing Setup**:
- [ ] .NET 10 SDK installed
- [ ] Project builds successfully
- [ ] All packages restored

**Compilation Testing**:
- [ ] `dotnet restore` succeeds
- [ ] `dotnet build` succeeds with 0 errors
- [ ] 0 warnings (or warnings documented)

**Functional Testing**:
- [ ] All commands execute successfully
- [ ] All options parse correctly
- [ ] Configuration loads correctly
- [ ] HTTP operations work
- [ ] PAC file generation succeeds

**Integration Testing**:
- [ ] End-to-end workflows complete
- [ ] Error handling functions correctly
- [ ] Help documentation displays properly

**Behavioral Validation**:
- [ ] HttpContent usage tested
- [ ] System.CommandLine functionality verified
- [ ] Configuration exceptions tested

**Regression Testing**:
- [ ] No functional regressions
- [ ] Output quality maintained
- [ ] Performance acceptable

**Final Validation**:
- [ ] Application runs successfully from command line
- [ ] All documented scenarios work
- [ ] User experience matches expectations

### Test Execution Order

1. **Compilation Validation** - Must pass before proceeding
2. **Unit Testing** - If tests exist
3. **Integration Testing** - Verify component interactions
4. **End-to-End Testing** - Validate complete workflows
5. **Behavioral Change Validation** - Focus on specific known changes
6. **Regression Testing** - Final comprehensive check

**Failure Handling**:
- If compilation fails: Fix all errors before proceeding to functional testing
- If tests fail: Investigate, fix, and retest before moving forward
- If behavioral differences found: Evaluate acceptability, document, and address if necessary
- If regressions detected: Identify root cause, fix, and re-validate

### Testing Notes

**No Test Project Identified**: Assessment does not indicate presence of separate test project. Testing strategy focuses on manual validation and integration testing.

**Beta Package Considerations**: Given System.CommandLine beta status, exhaustive command-line testing is critical to catch any subtle API behavior changes.

**Behavioral Changes**: HttpContent behavioral changes require specific focus - test all HTTP operations thoroughly.

---

## Complexity & Effort Assessment

### Overall Complexity: 🟢 Low

**Factors Contributing to Low Complexity:**
- Single project (no coordination required)
- Small codebase (1,093 LOC)
- SDK-style project (modern format)
- No security vulnerabilities
- Clear package upgrade path
- No circular dependencies
- Straightforward .NET 8 → .NET 10 upgrade path

**Complexity Factors Requiring Attention:**
- ⚠️ Beta package dependency (System.CommandLine) may require careful testing
- ⚠️ 62 source-incompatible API usages need addressing
- ⚠️ Legacy configuration pattern usage (ConfigurationErrorsException)

### Project Complexity

| Project | Complexity | LOC | Dependencies | Packages | Risk | Notes |
|---------|-----------|-----|--------------|----------|------|-------|
| Office365.PacUtil.csproj | 🟢 Low | 1,093 | 0 projects | 7 (5 updates) | Low | Small console app; beta API dependency requires testing |

### Phase Complexity

**Phase 1: Atomic Upgrade**
- **Complexity**: 🟢 Low
- **Scope**: Single project with straightforward upgrade
- **Dependencies**: None (standalone)
- **Risk**: Low (no vulnerabilities, clear package path)

**Effort Breakdown**:
1. **Project File Updates**: Low complexity
   - Update 1 TargetFramework property
   - Update 5 PackageReference versions

2. **Code Changes**: Medium complexity
   - Address 62 source-incompatible API calls
   - Most issues concentrated in System.CommandLine usage
   - Review 18 ConfigurationErrorsException usages

3. **Testing**: Low-Medium complexity
   - Verify 4 behavioral changes at runtime
   - Test command-line functionality
   - Validate HTTP operations
   - Confirm configuration error handling

### Resource Requirements

**Skills Required:**
- .NET developer familiar with .NET 8 → .NET 10 migration
- Understanding of System.CommandLine library
- Experience with Microsoft.Extensions.Configuration patterns

**Parallel Capacity:**
- Not applicable (single project)
- Single developer can complete entire upgrade

**Relative Effort:**
- **Project File Updates**: Minimal (5-10 minutes)
- **Dependency Restoration & Build**: Minimal (2-5 minutes)
- **Code Changes**: Low-Medium (focus on System.CommandLine APIs)
- **Testing & Validation**: Low-Medium (thorough command-line and HTTP testing)

**Note**: No time estimates provided per planning guidelines. Actual duration depends on developer experience, tooling, and unforeseen issues.

---

## Source Control Strategy

### Branch Strategy

**Working Branch**: `upgrade-to-NET10`  
**Source Branch**: `net10-upgrade`  
**Merge Target**: `net10-upgrade` (or main branch per repository conventions)

**Branching Approach**:
1. ✅ Created feature branch `upgrade-to-NET10` from `net10-upgrade`
2. All upgrade work performed on `upgrade-to-NET10`
3. Merge back to `net10-upgrade` after validation
4. Follow repository's standard process for merging to main/production

### Commit Strategy

**Recommended Approach**: **Single Atomic Commit**

Given the All-At-Once strategy and single-project scope, a single commit provides:
- ✅ Clean upgrade history
- ✅ Easy rollback (single revert)
- ✅ Clear upgrade boundary
- ✅ Simplified review

**Commit Structure**:

**Single Commit: Complete .NET 10 Upgrade**
```
feat: Upgrade Office365.PacUtil to .NET 10.0

- Update TargetFramework: net8.0 → net10.0
- Update Microsoft.Extensions.* packages: 8.0.0 → 10.0.5
- Update Newtonsoft.Json: 13.0.3 → 13.0.4
- Update System.Configuration.ConfigurationManager: 8.0.0 → 10.0.5
- Fix System.CommandLine API incompatibilities (62 instances)
- Address ConfigurationErrorsException compatibility (18 instances)
- Validate HttpContent behavioral changes (4 instances)
- All tests passing
- Application validated end-to-end

Breaking Changes:
- System.CommandLine beta APIs updated for .NET 10 compatibility
- HttpContent behavior aligned with .NET 10

BREAKING CHANGE: Requires .NET 10 SDK
```

**Alternative Approach**: **Checkpoint Commits** (if preferred)

If incremental commits are desired for tracking progress:

**Commit 1: Project File and Package Updates**
```
feat: Update project file and packages for .NET 10

- Update TargetFramework: net8.0 → net10.0
- Update 5 NuGet packages to .NET 10-compatible versions
```

**Commit 2: Code Changes**
```
fix: Resolve .NET 10 API incompatibilities

- Fix System.CommandLine API calls (62 instances)
- Update ConfigurationErrorsException handling (18 instances)
- Address HttpContent usage for behavioral changes (4 instances)
```

**Commit 3: Validation and Cleanup**
```
test: Validate .NET 10 upgrade

- All tests passing
- End-to-end validation complete
- Application runs successfully
```

**Recommended**: Use **single atomic commit** for simplicity and clean history.

### Commit Message Format

Follow conventional commit format:
- **Type**: `feat` (new .NET version capability) or `chore` (maintenance upgrade)
- **Scope**: Optional - `upgrade`, `dependencies`, or omit
- **Description**: Clear summary of upgrade
- **Body**: Detailed list of changes
- **Footer**: Breaking change notices

### Review and Merge Process

**Pull Request Requirements**:
- ✅ Title: "Upgrade to .NET 10.0"
- ✅ Description: Link to this plan.md, summary of changes, testing performed
- ✅ Checklist: Include validation checklist (from Success Criteria)
- ✅ Reviewers: Assign appropriate team members

**PR Checklist** (include in PR description):
```markdown
## .NET 10 Upgrade Validation

### Build & Compilation
- [ ] Solution builds with 0 errors
- [ ] Solution builds with 0 warnings
- [ ] All packages restored successfully

### Functional Testing
- [ ] All command-line commands work
- [ ] Configuration loading succeeds
- [ ] PAC file generation succeeds
- [ ] HTTP operations validated

### Code Quality
- [ ] All API incompatibilities resolved
- [ ] No code regressions introduced
- [ ] Code review completed

### Documentation
- [ ] Breaking changes documented
- [ ] README updated if necessary
- [ ] Changelog updated
```

**Merge Criteria**:
- ✅ All checklist items completed
- ✅ Code review approved
- ✅ CI/CD pipeline passes (if applicable)
- ✅ No merge conflicts

**Merge Method**:
- **Recommended**: Squash and merge (if using checkpoint commits)
- **Alternative**: Merge commit (if using single atomic commit)

### Rollback Strategy

**If Upgrade Must Be Reverted**:

**Single Commit Approach**:
```bash
git revert <commit-hash>
```

**Multiple Commits Approach**:
```bash
git revert <commit-hash-1> <commit-hash-2> <commit-hash-3>
# Or
git revert HEAD~3..HEAD
```

**Complete Branch Rollback**:
```bash
git checkout net10-upgrade
git branch -D upgrade-to-NET10
```

### Git Workflow Summary

1. ✅ Branch created: `upgrade-to-NET10`
2. Perform upgrade work
3. Commit changes (single atomic or checkpoints)
4. Push branch to remote
5. Create pull request
6. Code review
7. Validation confirmation
8. Merge to `net10-upgrade`
9. Follow standard process for production deployment

**Repository Conventions**: Adapt this strategy to match existing repository conventions (branch naming, commit message format, PR process).

---

## Success Criteria

### Technical Criteria

The .NET 10 upgrade is considered technically successful when:

#### Project Migration
- ✅ **Office365.PacUtil.csproj** targets `net10.0`
- ✅ Project file is valid and well-formed
- ✅ SDK-style project structure maintained

#### Package Updates
- ✅ **All 5 package updates applied**:
  - Microsoft.Extensions.Configuration: 10.0.5
  - Microsoft.Extensions.Configuration.Json: 10.0.5
  - Microsoft.Extensions.Hosting: 10.0.5
  - Newtonsoft.Json: 13.0.4
  - System.Configuration.ConfigurationManager: 10.0.5
- ✅ No package dependency conflicts
- ✅ No security vulnerabilities introduced
- ✅ Package restore succeeds without errors

#### Build Success
- ✅ `dotnet restore` completes successfully
- ✅ `dotnet build` completes with **0 errors**
- ✅ `dotnet build` completes with **0 warnings** (or warnings documented as acceptable)
- ✅ No compilation errors related to API incompatibilities
- ✅ Build output is clean and valid

#### Code Quality
- ✅ **All 62 source-incompatible API calls resolved**:
  - System.CommandLine API usage updated
  - ConfigurationErrorsException handling verified
  - No compilation errors remain
- ✅ **All 4 behavioral changes addressed**:
  - HttpContent usage validated
  - Behavioral differences documented
  - No runtime errors introduced
- ✅ No code regressions introduced
- ✅ Code follows existing style and patterns

#### Testing
- ✅ All existing tests pass (if test suite exists)
- ✅ No new test failures introduced
- ✅ Integration testing completed successfully
- ✅ End-to-end scenarios validated

### Functional Criteria

The upgrade is functionally successful when:

#### Application Execution
- ✅ Application starts successfully
- ✅ Application runs without runtime errors
- ✅ No unexpected exceptions during execution
- ✅ Application exits cleanly

#### Command-Line Interface
- ✅ All commands execute correctly
- ✅ All options parse correctly
- ✅ Required option validation works
- ✅ Help text displays properly
- ✅ Error messages display appropriately
- ✅ Command routing functions correctly

#### Core Functionality
- ✅ **Configuration loading works**:
  - appsettings.json loads successfully
  - Configuration values accessible
  - Configuration errors handled correctly
- ✅ **HTTP operations succeed**:
  - HTTP requests complete successfully
  - Response content reads correctly
  - Headers handled properly
  - Error scenarios handled appropriately
- ✅ **PAC file generation works**:
  - PAC template loads correctly
  - Office 365 endpoint data retrieved successfully
  - PAC file generates with correct syntax
  - Generated output is valid and functional

#### Behavioral Validation
- ✅ HttpContent usage produces expected results
- ✅ System.CommandLine functionality matches expectations
- ✅ ConfigurationErrorsException handling works correctly
- ✅ No unexpected behavioral changes detected

### Quality Criteria

#### Code Quality Maintained
- ✅ Code style consistent with existing codebase
- ✅ No unnecessary changes introduced
- ✅ Code readability maintained
- ✅ Comments updated if necessary

#### Test Coverage Maintained
- ✅ Existing test coverage percentage maintained
- ✅ No tests removed or disabled
- ✅ Test quality maintained

#### Documentation Updated
- ✅ README.md updated with .NET 10 requirement (if applicable)
- ✅ CHANGELOG.md updated with upgrade entry (if maintained)
- ✅ Breaking changes documented
- ✅ Behavioral changes documented

### Process Criteria

#### Strategy Followed
- ✅ **All-At-Once Strategy applied**:
  - Single atomic upgrade operation completed
  - All project file and package updates applied simultaneously
  - Compilation and fixes performed in one coordinated effort
  - No intermediate multi-targeting states

#### Source Control
- ✅ **All changes committed**:
  - Work performed on `upgrade-to-NET10` branch
  - Commit message(s) follow conventions
  - Commit history is clean and meaningful
- ✅ Pull request created (if applicable)
- ✅ Code review completed (if required)

#### Validation Completed
- ✅ All validation checklist items completed
- ✅ No outstanding issues or warnings
- ✅ Rollback plan tested (optional)

### Definition of Done

**The .NET 10 upgrade is COMPLETE when**:

1. ✅ **Technical criteria met**: Project targets net10.0, all packages updated, builds with 0 errors/warnings
2. ✅ **Functional criteria met**: Application runs successfully, all core functionality works, no behavioral regressions
3. ✅ **Quality criteria met**: Code quality maintained, tests passing, documentation updated
4. ✅ **Process criteria met**: All-At-Once strategy followed, source control completed, validation done

**Acceptance Gate**: All above criteria must be satisfied before considering the upgrade complete and production-ready.

### Post-Upgrade Validation

After merge to target branch:
- ✅ CI/CD pipeline passes (if applicable)
- ✅ Deployment to staging environment succeeds (if applicable)
- ✅ Staging validation confirms functionality
- ✅ Production deployment plan ready

**Final Sign-Off**: Obtain team/stakeholder approval before deploying to production.

---

## Plan Completion

This comprehensive plan provides:
- ✅ Clear migration strategy (All-At-Once)
- ✅ Detailed project upgrade specifications
- ✅ Complete package update reference
- ✅ Comprehensive breaking changes catalog
- ✅ Risk assessment and mitigation strategies
- ✅ Multi-level testing strategy
- ✅ Source control workflow
- ✅ Explicit success criteria

**Plan Status**: ✅ **COMPLETE** - Ready for execution stage

**Next Step**: Proceed to execution stage where this plan will be transformed into actionable tasks and executed systematically.
