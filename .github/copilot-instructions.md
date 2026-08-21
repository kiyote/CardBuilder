# Copilot Instructions

## Project Guidelines
- Always use IDE-aware editing tools (create_file, replace_string_in_file, multi_replace_string_in_file, remove_file) to create and modify files. Do not write or delete file contents via terminal commands such as Set-Content, Out-File, or Remove-Item.

## Creating .axaml files
- `create_file` fails on `.axaml` files with "Could not get text view from file path" and leaves a 0-byte file behind. To author a new `.axaml`, call `replace_string_in_file` against the 0-byte file with an empty `oldString`; that writes the full content successfully.
- Never leave an empty or malformed `.axaml` in the project. The Avalonia resource task fails the whole build with `AVLN1001: Root element is missing`, which also suppresses XAML codegen project-wide. The visible symptom is misleading `CS0103` errors claiming `InitializeComponent` and every `x:Name` field "does not exist" in unrelated files. If those appear, look for a broken `.axaml` before debugging the C#.

## Verifying changes
- `run_build` is authoritative, but it can report success while a newly added, unreferenced view still has XAML errors. `get_errors` catches those. Conversely `get_errors` can return stale diagnostics pointing at lines that no longer contain the offending code. When the two disagree, re-run the build and trust it.

## Avalonia specifics
- Members bound from a control's own `.axaml` (for example `{Binding #Root.IsEditing}`) must be at least `internal`. The XAML compiler emits binding code into a separate generated class, so a `private` member resolves at compile time and the build succeeds, but the binding fails at runtime. Use `internal` to keep the member out of the public API and the designer property view while remaining reachable.
- A failed `IsVisible` binding leaves the property at its default of `true`. Inside a `Panel` (or overlapping `Grid` cell) that makes every child visible at once, and the last-declared child wins visually. The symptom looks like wrong state rather than a broken binding, so check for binding failures first.
- Compiled bindings are enabled via `AvaloniaUseCompiledBindingsByDefault`. Every view with bindings needs `x:DataType` on its root, and every `DataTemplate`/`TreeDataTemplate` needs its own `x:DataType` (the `x:` prefix matters; the plain `DataType` property does not drive compiled bindings). Element-name bindings (`{Binding #Name}`) also become compile errors when the name is not in scope.
- A successful build does NOT prove bindings work at runtime. Compile-time member resolution and runtime accessibility/DataContext correctness are separate concerns. Failures seen in this project that built cleanly: a missing `DataContext`, a non-collection bound to `ItemsSource`, and a `private` member bound from XAML.
- The behaviors library is packaged as `Xaml.Behaviors.Avalonia` for Avalonia 12. The older `Avalonia.Xaml.Behaviors` / `Avalonia.Xaml.Interactivity` package IDs stop at 11.3.x and must not be used here.
- `TryFindResource` is an extension method in the `Avalonia.Controls` namespace, not a member of `Application`.
