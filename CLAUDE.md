# Project rules for Claude

## What this is

BMIRechner is a small Windows Forms application that calculates the body mass index from a size in
centimeters and a weight in kilograms, shows the matching WHO category as text and colors both
result boxes accordingly. The user interface can be switched between German and English at runtime.
The repository is an application, it is **not** a library and **not** published as a NuGet package:
no `GeneratePackageOnBuild`, no push script. What gets released is an Inno Setup installer that is
tracked in this repository.

One solution `src/BMIRechner.sln` with exactly one project:

- `src/BMIRechner/BMIRechner.csproj`, `OutputType` `WinExe`, `TargetFramework` `net9.0-windows`,
  `UseWindowsForms`, `ApplicationIcon` `BMI.ico`.

Layout inside `src/BMIRechner`:

- `Program.cs`: `[STAThread] Main`, enables visual styles and runs the `Main` form. Nothing else.
- `Main.cs`: everything the application does. The constructor initializes the designer controls,
  the language manager and the combo box, `ButtonResultClick` calculates, `DetermineColor` maps a
  BMI to a color and writes the category text, `OnLanguageChanged` retranslates the whole form.
  Keep new logic in that shape, one method per concern.
- `Main.Designer.cs` and `Main.resx`: designer generated, German comments, do not reformat.
- `GlobalUsings.cs`: all usings of the project.
- `languages/de-DE.xml` and `languages/en-US.xml`: the translations, copied to the output directory
  with `CopyToOutputDirectory=Always`.
- `License.txt`: copied to the output directory as well, the installer shows it.
- `BMI.ico`: the application and installer icon.

Repository root: `README.md` (the only user documentation, badges and the two screenshots),
`Changelog.md`, `License.txt` (MIT), `Screenshot_DE.PNG`, `Screenshot_EN.PNG`, `.gitattributes`,
`.gitignore` and the `Setup` folder. `src` holds `.editorconfig` and
`BMIRechner.sln.DotSettings` next to the solution. There is no `Updating.md`, no `HowToUse.md`,
no `.github` folder and no test project.

`Setup` holds the release machinery:

- `build-setup-files.bat`: deletes every `bin` and `obj` below `src`, publishes the application to
  `src/BMIRechner/bin/publish` and removes the `*.pdb` files. It does **not** compile the
  installer.
- `BMIRechner-Setup.iss`: the Inno Setup script, packs the whole publish folder.
- `BMIRechner-Setup.exe`: the built installer, tracked in git.

## Build

```powershell
dotnet build src/BMIRechner.sln -c Release
```

- Single target framework `net9.0-windows` in the only project, no multi-targeting.
  `RuntimeIdentifiers` is `win-x64`. The application is Windows only, it is a Windows Forms
  executable.
- All build properties live directly in `src/BMIRechner/BMIRechner.csproj`. There is **no**
  `Directory.Build.props` in this repository.
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
- There are no automated tests. A behaviour change is verified by running the executable: the form
  has to come up, the language combo box has to hold both languages, and a calculation has to
  produce the expected value and color.

## Code conventions

Follow the surrounding code, it is consistent throughout every hand written file:

- File header comment block with `<copyright file="..." company="Hämmer Electronics">` and a
  `<summary>`, then the file-scoped namespace. `Main.Designer.cs` is exempt, it is generated.
- XML doc comments on every type and every member, private members included, no exceptions.
- `Nullable`, `ImplicitUsings` and `LangVersion latest` are enabled.
- New `using` directives go into `GlobalUsings.cs`, inside the existing `#pragma warning disable
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

- **The window title is built in two places.** `LoadTitleAndDescription` writes
  `Application.ProductName + string.Empty + Application.ProductVersion`, `OnLanguageChanged` writes
  the translated `Title` word plus the same version. The constructor calls
  `LoadTitleAndDescription` last, so at startup the title is the assembly name with no separator
  before the version, and the translated title only appears once the language is switched.
  `Screenshot_DE.PNG` shows what it is supposed to look like: `BMI Rechner 1.0.0.1`.
- **`Application.ProductVersion` is the GitVersion informational version.** On an untagged commit
  the title therefore reads something like `BMI Rechner 1.0.8-1+Branch.master.Sha...`, on a tagged
  one just `1.0.8`.
- **The combo box calculates.** `LoadLanguagesToCombo` sets `SelectedIndex = 0`, which raises
  `SelectedIndexChanged`, which sets the language, which raises `OnLanguageChanged`, which calls
  `ButtonResultClick`. So the application computes a BMI from the default values 1 cm and 1 kg
  before the user has entered anything, which is 10000 and therefore dark red
  "Adipositas Grad III".
- **The result is formatted with `CultureInfo.InvariantCulture`.** The German user interface shows
  `24.07`, not `24,07`. That is what the released screenshot shows, do not switch it to the current
  culture without asking.
- **`GetWord` returns `null` for an unknown key** and does not fall back to another language, so
  every key used in `Main.cs` has to exist in both language files. Missing keys show up as empty
  labels at runtime, not as an error.
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
