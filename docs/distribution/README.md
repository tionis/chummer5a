# Chummer 5 — Quick start

Chummer creates and manages characters for Shadowrun Fifth Edition.
Extract the **entire ZIP** into a folder you can write to, such as your home
folder. Keep the DLLs and data folders alongside `Chummer5.exe`.

## Linux (Wine)

This build was tested on Debian 13, x86-64, with Wine and Microsoft .NET
Framework 4.8. On Debian, install Wine with 32-bit support:

```bash
sudo dpkg --add-architecture i386
sudo apt update
sudo apt install wine wine32 wine64 libwine libwine:i386 fonts-wine winetricks
```

If `winetricks` is unavailable, enable `contrib` in your Debian APT sources,
then run `sudo apt update` and retry. Other distributions have different
package-installation commands.

Install .NET Framework inside Wine once, **without sudo**:

```bash
winetricks -q dotnet48
```

Then open a terminal in the extracted `Chummer` folder and launch:

```bash
wine Chummer5.exe
```

Use the same Wine prefix for installing .NET and launching Chummer. If you
haven't set `WINEPREFIX`, both commands use `~/.wine`. You do not need the
Linux .NET SDK to run this ZIP.

## Windows

Install Microsoft .NET Framework 4.8 if it is not already available, then
double-click `Chummer5.exe` in the extracted folder.

## Your first character

- Choose **File → New** and select the character-settings preset your GM uses.
  If your GM supplies a settings XML file separately, place it in `settings/`
  and restart Chummer so it appears in the list.
- Complete the character-creation choices, then use the character tabs to add
  skills, equipment, and other options. Use **File → Open** for an existing
  character.
- Save your character regularly and keep a backup outside the application
  folder before replacing or upgrading this distribution.
- Enabled sourcebooks control which game content is available. PDF rulebooks
  are not included in this initial package; you can link your own PDFs in
  Chummer's global settings.

## If something goes wrong

For character-sheet or printing problems under Wine, try **Apply Linux printing
fix** in global settings. If the application won't start, check that .NET 4.8
was installed in the same Wine prefix and that you extracted all files.
For other issues, tell your GM what you clicked and include any error message;
application logs are written under `logs/`.

`LICENSE` contains the software license. `source.tar.gz` contains the source
and developer build instructions; you do not need to unpack it to play.
