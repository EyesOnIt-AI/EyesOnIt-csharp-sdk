# EyesOnIt C# SDK Agent Rules

The central rules in `../eoi-ai-factory/standards/ai-agent-rules.md` apply when repos are checked out as siblings under `$EOI_WORKSPACE`.

## Repo-Specific Rules

- Keep public SDK classes and method signatures aligned with backend API contracts.
- Consider Genetec plugin impact for all public SDK changes.
- Do not hardcode hostnames, credentials, tokens, license keys, customer IDs, or environment values.
- Do not add dependencies without dependency/license review.
- Keep NuGet/package metadata accurate when release-facing files change.

