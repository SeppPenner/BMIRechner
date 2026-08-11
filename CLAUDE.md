# Project rules for Claude

## What this is

BMIRechner is a small Windows Forms application that calculates the body mass index from a size in
centimeters and a weight in kilograms, shows the matching WHO category as text and colors both
result boxes accordingly. The user interface can be switched between German and English at runtime.
The repository is an application, it is **not** a library and **not** published as a NuGet package:
no `GeneratePackageOnBuild`, no push script. What gets released is an Inno Setup installer that is
tracked in this repository.

One solution `src/BMIRechner.sln` with exactly two projects:

- `src/BMIRechner/BMIRechner.csproj`, `OutputType` `WinExe`, `TargetFramework` `net10.0-windows`,
  `UseWindowsForms`, `ApplicationIcon` `BMI.ico`, the application.
- `src/BMIRechner.Tests/BMIRechner.Tests.csproj`, MSTest, added in version 1.0.8.0.

Layout inside `src/BMIRechner`:

- `Program.cs`: `[STAThread] Main`, enables visual styles and runs the `Main` form. Nothing else.
- `BmiCalculator.cs`: the calculation and everything that can be decided without a form.
  `Calculate` returns the body mass index rounded to two decimals, `DetermineCategory` maps it to a
  `BmiCategory` and `GetLanguageKey` maps a category to the key of the matching word in the language
  files. No user interface code goes in here, that is what makes it testable.
- `BmiCategory.cs`: the eight WHO categories. The names are the keys of the language files.
- `Main.cs`: everything that needs the form. The constructor initializes the designer controls, the
  language manager and the combo box, `ButtonResultClick` reads the two numeric up downs and writes
  the result, `DetermineColor` maps a category to a color, `CheckColor` picks the foreground color,
  `LoadTitle` builds the window title, `OnLanguageChanged` retranslates the whole form. Keep new
  logic in that shape, one method per concern.
- `Main.Designer.cs` and `Main.resx`: designer generated, German comments, do not reformat.
- `GlobalUsings.cs`: all usings of the project.
- `languages/de-DE.xml` and `languages/en-US.xml`: the translations, copied to the output directory
  with `CopyToOutputDirectory=Always`.
- `License.txt`: copied to the output directory as well, the installer shows it.
- `BMI.ico`: the application and installer icon.

Layout inside `src/BMIRechner.Tests`:

- `BmiCalculatorTests.cs`: the rounding, the value of the readme screenshots, the guard against a
  mass or size of zero, every band bound of `DetermineCategory` from both sides, the range the form
  can produce and the mapping to the language keys.
- `LanguageFilesTests.cs`: both language files are found and read, every category and every user
  interface key has a non empty word in both languages, both files define the same keys, and the
  English file does not contain the German words it used to contain.
- `GlobalUsings.cs`: all usings of the test project.
- The two language files are **linked** into the test project (`None Include` with `Link`), not
  copied, because the language library reads them from the output directory. Do not duplicate them.

Repository root: `README.md` (the only user documentation, badges and the two screenshots),
`Changelog.md`, `License.txt` (MIT), `Screenshot_DE.PNG`, `Screenshot_EN.PNG`, `.gitattributes`,
`.gitignore` and the `Setup` folder. `src` holds `.editorconfig` and
`BMIRechner.sln.DotSettings` next to the solution. There is no `Updating.md`, no `HowToUse.md`
and no `.github` folder.

`Setup` holds the release machinery:

- `build-setup-files.bat`: deletes every `bin` and `obj` below `src`, publishes the application
  **self contained** for `win-x64` to `src/BMIRechner/bin/publish` and removes the `*.pdb` files.
  It does **not** compile the installer.
- `BMIRechner-Setup.iss`: the Inno Setup script, packs the whole publish folder.
- `BMIRechner-Setup.exe`: the built installer, tracked in git.

## Build

```powershell
dotnet build src/BMIRechner.sln -c Release
```

```powershell
dotnet test src/BMIRechner.sln -c Release
```

- Single target framework `net10.0-windows` in both projects, no multi-targeting.
  `RuntimeIdentifiers` is `win-x64` in the application. The application is Windows only, it is a
  Windows Forms executable, and the test project has to carry the same `-windows` framework to be
  allowed to reference it.
- All build properties live directly in the two `.csproj` files and are duplicated there. There is
  **no** `Directory.Build.props` in this repository.
- `TreatWarningsAsErrors` is enabled, so every warning breaks the build, NuGet warnings (`NU****`)
  from restore included. A clean build reports zero warnings, keep it that way.
- `NU1803` (HTTP source usage during restore) is the one warning suppressed via `NoWarn`. Fix
  warnings instead of extending that list. `NuGetAudit` and `NuGetAuditMode=all` are on, so a
  vulnerable transitive package fails the build too.
- Versions come from GitVersion.MsBuild out of the git tags, for example `1.0.8-1` for the first
  commit after tag `1.0.7`. Never edit a version property or an assembly version by hand.
- Restore needs nuget.org. If a private feed is configured globally on the machine and answers 404
  for public packages, restore fails with `NU1301`. Then build with an explicit source:
  `dotnet build src/BMIRechner.sln --source https://api.nuget.org/v3/index.json`.
- Tests are MSTest, in the single test project `src/BMIRechner.Tests`, which follows the same
  package set as the sibling repositories: `Microsoft.NET.Test.Sdk`, `MSTest.TestAdapter`,
  `MSTest.TestFramework`, `coverlet.collector` and `GitVersion.MsBuild`. `dotnet test` runs 12
  tests, they need no network and they write nothing. Never claim a test run happened without
  running it.
- The tests cover the calculation and the language files, they cannot cover the form. Beyond them, a
  behaviour change is verified by starting the executable: the window has to come up, its title has
  to read the translated name plus the version, the combo box has to hold both languages, and a
  calculation has to produce the expected value and color. The window title can be read without a
  screenshot via `(Start-Process <exe> -PassThru).MainWindowTitle`.

## Code conventions

Follow the surrounding code, it is consistent throughout every hand written file:

- File header comment block with `<copyright file="..." company="Hämmer Electronics">` and a
  `<summary>`, then the file-scoped namespace. `Main.Designer.cs` is exempt, it is generated.
- XML doc comments on every type and every member, private members included, no exceptions.
- `Nullable`, `ImplicitUsings` and `LangVersion latest` are enabled.
- New `using` directives go into the `GlobalUsings.cs` of the respective project, inside the
  existing `#pragma warning disable
  IDE0065` block, never at the top of a file. The editorconfig requires usings inside the namespace
  (`csharp_using_directive_placement=inside_namespace:warning`), which global usings cannot satisfy,
  that is what the pragma is for. Do not add other pragmas. The comment text in that block is
  German because Visual Studio generated it, leave it alone.
- Fields, properties, methods and events are always accessed with `this.` qualification
  (`dotnet_style_qualification_for_*` at severity `warning`).
- `src/.editorconfig` also enforces braces everywhere, no multiple blank lines, four spaces, CRLF,
  UTF-8, file scoped namespaces, `System` usings sorted first and `IDE0005` as warning. Analyzer
  warnings are fixed, not silenced.

## Known quirks

Do not silently "clean up" these, they are existing behaviour:

- **The window title comes from the language file.** `LoadTitle` is the only place that assigns
  `Text`, it writes the translated `Title` word, a blank and `Application.ProductVersion`.
  `OnLanguageChanged` calls it again after a language switch. `Screenshot_DE.PNG` shows the expected
  result: `BMI Rechner 1.0.0.1`.
- **`Application.ProductVersion` is the GitVersion informational version.** On an untagged commit
  the title therefore reads something like `BMI Rechner 1.0.8-1+Branch.master.Sha...`, on a tagged
  one just `1.0.8`.
- **The language changed event is subscribed late on purpose.** `LoadLanguagesToCombo` sets
  `SelectedIndex = 0`, which raises `SelectedIndexChanged`, which sets the language, which raises
  `OnLanguageChanged`, which calls `ButtonResultClick`. The constructor therefore subscribes the
  handler after the combo box is filled. Moving that line back into
  `InitializeLanguageManager` makes the application show a BMI of 10000 (the default 1 cm and 1 kg)
  in dark red before the user has entered anything.
- **The result is formatted with `CultureInfo.InvariantCulture`.** The German user interface shows
  `24.07`, not `24,07`. That is what the released screenshot shows, do not switch it to the current
  culture without asking.
- **`GetWord` returns `null` for an unknown key** and does not fall back to another language, so
  every key that `Main.cs` and `BmiCalculator.GetLanguageKey` ask for has to exist in both language
  files. Missing keys show up as empty labels at runtime, not as an error. `LanguageFilesTests`
  exists for exactly that reason.
- **The language files are loaded by file system location.** The package
  `HaemmerElectronics.SeppPenner.Language` enumerates the `languages` folder next to its own
  assembly, so `CopyToOutputDirectory=Always` is what makes the application work at all, and the
  installer has to ship the folder. A missing folder throws inside the `LanguageManager`
  constructor, before any of our code can react.
- **The combo box holds names, not identifiers.** It is filled with `ILanguage.Name` (`Deutsch`,
  `English (US)`) and `SetCurrentLanguageFromName` matches on exactly that string. The startup
  default is set once by identifier (`SetCurrentLanguage("de-DE")`), and the order of the entries
  is the order in which the files are enumerated.
- **Duplicated assets.** `src/BMI.ico` and `src/BMIRechner/BMI.ico` are byte identical, and so are
  `License.txt` in the root and in the project folder. The build and the installer both use the
  copies inside `src/BMIRechner`, the other two are leftovers. Leave them, deleting them is a
  separate decision.
- **`Main.resx` still holds the Visual Studio template entries** `Name1`, `Color1`, `Bitmap1` and
  `Icon1` next to the real `$this.Icon`. They are unused, they cost nothing, leave them.
- **Mixed control naming.** `numericUpDown_Size` and `numericUpDown_Weight` use the designer
  default with an underscore, every other control is PascalCase. Designer generated, not worth a
  rename that touches four files.
- **The BMI thresholds are the WHO bands** and they are exclusive upper bounds: `< 16`, `< 17`,
  `< 18.5`, `< 25`, `< 30`, `< 35`, `< 40`, everything else is grade III. The numeric up downs are
  limited to 1 to 230 cm (whole numbers) and 1 to 400 kg (one decimal), so a division by zero
  cannot be triggered from the user interface.
- **The installer is tracked although `.gitignore` excludes `*.exe`.** `Setup/BMIRechner-Setup.exe`
  is in the repository and has to be added with `git add -f` after every release build.
- **The publish is self contained since version 1.0.8.0.** The installed application no longer
  needs a .NET desktop runtime on the target machine, and in exchange the installer grew from
  1.8 MB to around 35 MB. A published `BMIRechner.runtimeconfig.json` with a `frameworks` block
  instead of `includedFrameworks` means the switch got lost, that is the fastest way to check it.
  Every release adds those megabytes to the git history for good, so moving the installer to a
  release asset is worth a thought at some point.
- **Inno Setup warns on every compile.** `PrivilegesRequired` defaults to `admin` while the quick
  launch icon uses `{userappdata}`. The icon is limited to Windows 7 and older via
  `OnlyBelowVersion: 0,6.1`, so it never gets created anyway. The warning is expected, do not
  mistake it for a broken script.
- **AppVeyor badge without CI in the repository.** `README.md` links an AppVeyor build that is
  configured outside of this repository. There is no `.github` folder and no pipeline file here.
- **`src/BMIRechner.sln.DotSettings`** is tracked and holds nothing but a ReSharper user dictionary
  (`Adiposity`, `H_00E4mmer`, `Rechner`). Leave it alone.
- **`.gitattributes` sets `* text=auto`** and every rule of the Visual Studio template below it is
  commented out. The two screenshots are recognized as binary by heuristic. Any binary file whose
  extension git cannot judge needs its own rule.

## Releasing

1. Make the change.
2. Add an entry at the top of `Changelog.md` in the existing format:
   `* **Version 1.0.8.0 (2026-08-11)** : Short description.`
3. Set `MyAppVersion` in `Setup/BMIRechner-Setup.iss` to the same four part version.
4. Commit that.
5. Tag the commit with the plain version number, no `v` prefix (`1.0.8`, `1.0.7`, ...). The existing
   tags are lightweight tags, create new ones the same way.
6. Only now build the installer, because the version in the executable comes from the tag:
   run `Setup/build-setup-files.bat`, then compile `Setup/BMIRechner-Setup.iss` with
   `ISCC.exe` (`C:\Program Files (x86)\Inno Setup 6\ISCC.exe`).
7. `git add -f Setup/BMIRechner-Setup.exe` and commit it, the existing commits for that step are
   called `Updated setup.`.
8. Push the commits and the tag.

The version in the `Changelog.md` has four parts (`1.0.8.0`), the tag has three (`1.0.8`).
GitVersion turns the tag into the assembly version, so an untagged commit produces something like
`1.0.8-1+Branch.master.Sha...` and that string would end up in the shipped executable and in the
window title. Building the installer before the tag is therefore a mistake, not a matter of taste.

## Git

- **Never amend a commit.** No `git commit --amend`, not for a typo in the message, not to add a
  forgotten file, not even when the commit is still local. Write a follow-up commit instead. The
  release versions come from tags on exact commits, an amended commit leaves its tag pointing at a
  commit that no longer exists in the branch.

## Writing style

- Commit messages are written **in English only**: short, precise subject line, explanatory body
  when needed.
- Code comments and comments in project files such as `.csproj` are **always English**, regardless
  of the language used in the conversation.
- **No em dashes or en dashes** (`—`, `–`), neither in prose, commit messages, code comments nor
  documentation. Use a regular hyphen, comma, colon, parentheses or a separate sentence.
- German texts (documentation, chat replies) always use real umlauts and ß, never ASCII
  transliterations such as `ae`, `oe`, `ue` or `ss`. Identifiers, file names and configuration keys
  stay unchanged where umlauts are technically undesirable. The user visible strings of this
  application live in the language files, not in the code.
