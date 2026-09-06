<p align="center"><h1>Chummer 5</h1></p>
<p align="center"><img src="https://i.ibb.co/y0WC3j9/logo.png"></p>

[![Github Latest Release Date](https://img.shields.io/github/release-date/chummer5a/chummer5a?label=Latest%20Milestone%20Release)](https://github.com/chummer5a/chummer5a/releases/latest)
[![GitHub Issues](https://img.shields.io/github/issues/chummer5a/chummer5a.svg)](https://github.com/chummer5a/chummer5a/issues)
[![Build status](https://ci.appveyor.com/api/projects/status/wf0jbqd5xp05s4hs?svg=true)](https://ci.appveyor.com/project/chummer5a/chummer5a)
[![Discord](https://img.shields.io/discord/365227581018079232?label=discord)](https://discord.gg/8FKUPjTX2w)
[![License](https://img.shields.io/github/license/chummer5a/chummer5a)](https://www.gnu.org/licenses/gpl-3.0.html)
[![Donations](https://img.shields.io/badge/buy%20me%20a%20coffee-donate-yellow.svg)](https://ko-fi.com/Z8Z7IP4E)

## Basic Overview

Chummer is a character creation and management application for the tabletop RPG [Shadowrun, Fifth Edition](https://www.shadowruntabletop.com/products-page/getting-started/shadowrun-fifth-edition) running on Windows. Not only can you create your character quickly and easily, but you can also use Chummer during your character's shadowrunning career, to accurately track your Karma, Nuyen, ammo, and everything else all in one place. Chummer also includes support for a number of optional rules and house rules and even includes support for critters and is useful for players and Game Masters alike! It also supports a number of languages: supports multiple languages: English (US), French, German, Japanese, Portuguese (Brazil) and Chinese (Simplified) files are pre-installed, while additional languages can be added and shared through use of our in-house translator tool.

## Requirements
| Operating System | .NET Framework |
| --- | --- |
| Windows 7 SP1 or 8.1+ | 4.8+ |

## Installation - Windows

Chummer uses a single tree release strategy with two release channels; **Milestone** and **Nightly**.

* **Milestone** releases are a fixed-point for use by living communities and people that prefer not to update their application regularly. These releases are considered to be stable and are recommended for general use. 
* **Nightly** releases are an automated build created with Appveyor at 0000 UTC daily. These releases are more likely to be unstable, but also receive new features and bugfixes faster than the Milestone releases. These are recommended for users that have a specific issue from Milestone that was resolved in Nightly, or are comfortable with testing features. 

1. Download the archive for your preferred update channel [Milestone](https://github.com/chummer5a/chummer5a/releases/latest) or [Nightly](https://github.com/chummer5a/chummer5a/releases) (Select the latest Nightly tag)
2. Extract to preferred folder location. If upgrading, you can extract over the top of an existing folder path.
3. Run Chummer5.exe.

## Installation - Linux and OSX

As Chummer is a WinForms application, support for other operating systems is limited. For Linux, macOS, and Chrome OS, Chummer can be run through one of three possible ways:

1. Set up and run [Wine](https://www.winehq.org/), an open-source Windows compatibility layer. This is usually not for the faint-of-heart, especially on Chrome OS, but it is completely free. Some details about the steps necessary to run Chummer5a under Wine can be found on [the wiki](https://github.com/chummer5a/chummer5a/wiki#installation). Note that even after you set up Chummer5a to run on Wine, Wine is not perfect and you will encounter some additional bugs while using Chummer5a that you wouldn't run into under Windows.
2. Set up and run [CrossOver](https://www.codeweavers.com/crossover), a hassle-free version of Wine with commercial support. It costs money (though it has a limited free trial), but what you are effectively purchasing is for someone else to do all the hard work setting up Wine for you, no matter what you want to run on it. If you do not want to mess around with technical stuff, we highly recommend using CrossOver.
3. Set up and run a Windows virtual machine through programs like [VirtualBox](https://www.virtualbox.org/), [VMWare Fusion](https://www.vmware.com/products/fusion.html), or [Parallels](https://www.parallels.com/). You will need a valid copy of Windows and lots of disk space, but Chummer5a will run on a Windows virtual machine exactly how it would run under full Windows. Virtual machine hosts are generally not available for Chrome OS, though with some behind-the-scenes tinkering, it can still be possible to run a Windows virtual machine on Chrome OS.

### Debian 13 Wine setup (x86-64)

A user reported successfully running the Linux-built application with the
following Wine setup. Install Wine with 32-bit support and Winetricks:

```sh
sudo dpkg --add-architecture i386
sudo apt update
sudo apt install wine wine32 wine64 libwine libwine:i386 fonts-wine winetricks
```

Debian's [winetricks package](https://packages.debian.org/trixie/winetricks) is in
the `contrib` repository component; enable it in your Debian APT sources if the
package is unavailable, then run `sudo apt update` again.

Install .NET Framework 4.8 as your regular user:

```sh
winetricks -q dotnet48
```

Use the same Wine prefix when installing .NET and launching Chummer. With
`WINEPREFIX` unset, both commands use the default prefix, `~/.wine`.
The Linux .NET SDK used for compilation and the .NET Framework installed inside
Wine serve separate purposes; both are needed to build and run this way.

## Contributing

Please take a look at our [contributing](https://github.com/chummer5a/chummer5a/blob/master/CONTRIBUTING.md) guidelines if you're interested in helping!

### Building the desktop application on Linux

Install the .NET 8 SDK (8.0.401 or a newer 8.0.4xx patch, as specified by
`global.json`). From the repository root, run:

```sh
dotnet build Chummer/Chummer.csproj -c Release
```

The first build requires access to NuGet to download dependencies, including
the .NET Framework reference assemblies. The output is in `Chummer/bin/Release/`;
keep that directory's DLLs and data files alongside `Chummer5.exe`.
This builds the desktop application, not the full solution's optional tools and
plugins. It uses the generated C# files already checked into the repository;
editing T4 templates requires regenerating those files separately.

The output still targets Windows and .NET Framework 4.8. Run it using your
configured Wine environment, for example:

```sh
cd Chummer/bin/Release
wine Chummer5.exe
```

A successful Linux build does not establish Wine runtime compatibility.

### Creating a distribution ZIP

With the .NET 8 SDK, GNU Make, Git, GNU tar, and `zip` installed, run from the
repository root:

```sh
make dist
```

Running `make` without a target does the same thing. The result is
`dist/chummer.zip`, containing a `Chummer/` folder with the application,
dependencies, game data, this README, license notices, and `source.tar.gz`.
The source archive contains the working copies of Git-tracked files plus the
Makefile, so tracked local edits are included.

Packaging builds into a fresh temporary directory rather than copying your used
`bin/Release` directory. Personal saves, logs, profiling data, and debug symbols
are excluded from the application ZIP. Campaign presets and rulebook PDFs are
not added by this initial packaging target. Players should extract the entire
ZIP and use the Windows or Wine setup instructions above.

### Measuring character UI loading time

To collect local timings with a Release build, close Chummer and start it with:

```sh
cd Chummer/bin/Release
CHUMMER_PERF=1 wine Chummer5.exe
```

Reproduce character creation and switch between the character's tabs, then
repeat the same actions in the same session to distinguish first-use costs.
Timings are written to `logs/performance-*.tsv` in the application directory.
Each row contains a UTC completion timestamp, elapsed milliseconds, and a code
stage name. This diagnostic file is local and does not include character names
or file contents. Omit `CHUMMER_PERF` to disable it on the next launch.

`CharacterCreate.InitializeComponent`, `theme`, `translation`, and `tooltips`
measure UI construction stages. `load_frm_create_*` measures existing load
phases, while `_lstActiveSkills` and related entries measure skill-list setup.
Parent timings include child timings, so do not sum every row. Concurrent
operations may overlap, and diagnostic file writes add some overhead.
`tab.next_ui_callback.*` measures tab selection through the next queued UI
callback; it does **not** measure completed painting. Compare it with
`CharacterCreate.tab.RefreshPasteStatus` and the visible delay when diagnosing
tab switching. These tab probes cover character creation's main and street-gear
tabs; they do not cover every dialog or career-mode tab.

## History

This project is a continuation of work on the original Chummer projects for Shadowrun 4th and 5th editions, developed by Keith Rudolph and Adam Schmidt. Due to the closure of code.google.com, github repositories of their code have been created as a marker of their work. Please note, Chummer 4 is considered abandonware and is not maintained by the chummer5a team, and exists solely for historical purposes.

* Chummer 4, Keith Rudolph: https://github.com/chummer5a/chummer
* Chummer 5, Keith Rudolph and Adam Schmidt: https://github.com/chummer5a/chummer5

## Sponsors

* [JetBrains](http://www.jetbrains.com/) have been kind enough to provide our development team with licences for their excellent tools:
    * [ReSharper](http://www.jetbrains.com/resharper/)
