# GitHub Copilot Instructions for Fire Extinguisher (Continued) Mod

## Mod Overview and Purpose

The Fire Extinguisher (Continued) mod is an update of peladors mod that adds a useful pawn firefighting tool. It includes a fire extinguisher that can be used by pawns and a fire foam belt that will trigger if the wearer or the belt itself is set on fire. The fire extinguisher can also be cleaned using foam that degrades over time. This mod requires the Fire Extinguisher research and can be made at the Machining table with a recipe of 50 steel and 2 Components.

## Key Features and Systems

The key features and systems of the Fire Extinguisher (Continued) mod include:

- A fire extinguisher that can be used by pawns to put out fires.
- A fire foam belt that will trigger if the wearer or the belt itself is set on fire.
- The fire extinguisher can be cleaned using foam that degrades over time.
- The fire extinguisher requires the Fire Extinguisher research and can be made at the Machining table.
- The fire extinguisher has a dedicated shelf for use and requires 10 stuff of either metal or stone.
- The fire extinguisher is classified as a manufactured item and works with existing saves.
- The mod includes a Simple Sidearms Temporary Fix to ensure that the fire extinguisher is not selected during combat.

## Coding Patterns and Conventions

The Fire Extinguisher (Continued) mod follows the standard RimWorld modding conventions and patterns. The code is organized into C# files and XML files, with the C# files containing the Harmony patches and the XML files containing the definitions for the mod items.

The C# files are organized into classes and methods, with the classes representing the mod items and the methods representing the behavior of the items. The Harmony patches are used to modify the behavior of vanilla RimWorld code to add the functionality of the mod items.

The XML files use the RimWorld XML definition format to define the mod items, with each item represented by a ThingDef or a DamageDef. The XML files also define the recipes for the mod items, including the ingredients and the resulting item.

## XML Integration

The Fire Extinguisher (Continued) mod integrates with the RimWorld XML definition system to define the mod items and their behavior. The XML files are located in the Defs directory of the mod and are loaded by the RimWorld game engine.

The XML files use the standard RimWorld XML format, with each item represented by a ThingDef or a DamageDef. The XML files also define the recipes for the mod items, including the ingredients and the resulting item.

The XML files for the Fire Extinguisher (Continued) mod include:

- Buildings_Fire_Ext.xml - defines the fire extinguisher and its behavior.
- Damages_Fire_Ext.xml - defines the fire damage and its behavior.
- FireFoam_Belt.xml - defines the fire foam belt and its behavior.
- ResearchProject_Fire_Ext.xml - defines the Fire Extinguisher research and its behavior.
- Turret_Fire_Ext.xml - defines the fire turret and its behavior.

## Harmony Patching

The Fire Extinguisher (Continued) mod uses Harmony patches to modify the behavior of vanilla RimWorld code to add the functionality of the mod items. The Harmony patches are located in the C# files of the mod and are applied using the Harmony library.

The Harmony patches for the Fire Extinguisher (Continued) mod include:

- Building_TurretGun_TryFindNewTarget.cs - modifies the behavior of the turret gun to make it target fires.
- FExt_Options_Initializer.cs - initializes the mod options.
- FExtFoam.cs - adds the behavior of the fire foam.
- FireFoamBelt.cs - adds the behavior of the fire foam belt.
- FireExtDefOf.cs - defines the mod items.

## Suggestions for Copilot

To use GitHub Copilot effectively when developing the Fire Extinguisher (Continued) mod, it is recommended to use the following suggestions:

- Use the Harmony patching system to modify the behavior of vanilla RimWorld code.
- Use the XML definition system to define the mod items and their behavior.
- Use the standard RimWorld modding conventions and patterns to organize the code.
- Use the C# and XML files provided in the mod to understand the behavior of the mod items.
- Use the Harmony patches and XML definitions as a reference for creating new mod items.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

