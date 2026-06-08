Liberty Vehicle Services CE
===========================

Author: ekzestean
Licence: MIT License
Status: Public beta / work-in-progress release
Game: Grand Theft Auto IV: Complete Edition

Liberty Vehicle Services CE is a native-style vehicle services overhaul for
GTA IV Complete Edition. It adds vehicle dealers, registration offices,
owned vehicle persistence, insurance/recovery, workshop tuning, a fuel system,
metro map markers and optional LibertyCityPlates compatibility.

The design goal is to make vehicle ownership and vehicle services feel like a
natural part of Liberty City rather than a trainer-style external menu.


Main features
-------------

- Vehicle dealers with showroom and purchase logic.
- Registration offices for stolen/personal vehicles.
- Owned vehicle persistence by episode.
- Insurance and recovery mechanics.
- Workshop/tuning service.
- Dynamic repair/repaint/extra pricing.
- Vehicle values balanced through swankness and price overrides.
- Optional fuel system with refuelling stations.
- Optional metro station and entrance map markers.
- Optional LibertyCityPlates bridge support.
- Single INI configuration file for gameplay settings.


Requirements
------------

- Grand Theft Auto IV: Complete Edition.
- ASI loader.
- ScriptHook / ScriptHookDotNet setup compatible with GTA IV Complete Edition.
- FusionFix is strongly recommended and is the main tested baseline.
- LibertyCityPlates is optional. The mod can run without it, but plate
  persistence/integration is improved when the bridge is enabled and available.


Installation
------------

1. Make a clean backup of your current GTA IV scripts/plugins setup.
2. Copy the "scripts" folder into your GTA IV directory.
3. Copy the "plugins" folder into your GTA IV directory if you use the optional
   LibertyCityPlates bridge.
4. Open "scripts/LibertyVehicleServicesCE.ini" and enable or disable modules as
   needed.
5. Launch the game and check ScriptHookDotNet.log if something does not load.

Recommended baseline:

- GTA IV Complete Edition (no downgrade), Steam or equivalent legal copy.
- FusionFix installed.


Configuration
-------------

The main configuration file is:

scripts/LibertyVehicleServicesCE.ini

The most important sections are:

[Modules]
- Enables or disables the whole mod and individual systems.

[Registration]
- Controls whether service, emergency, taxi and industrial vehicles can be
  registered.

[Prices], [WorkshopPrices], [WorkshopPriceModifiers]
- Controls dealer prices, registration prices, insurance/recovery costs and
  workshop costs.

[SwanknessOverrides], [PriceOverrides]
- Allows per-vehicle economic balancing.

[Fuel]
- Controls fuel price, consumption, starting fuel, low-fuel thresholds and
  refuelling rate.

[Metro]
- Controls metro station and entrance markers.

[Plates]
- Controls LibertyCityPlates bridge behaviour.

[Controls]
- Controls keyboard bindings used by the mod menus.

[Debug]
- Diagnostic settings. Keep debug features disabled for normal play.


Compatibility notes
-------------------

Designed for GTA IV Complete Edition and tested around a FusionFix baseline.
The mod may conflict with mods that heavily alter vehicle spawning, vehicle
text entries, vehicles.ide, dealer areas, fuel stations, map blips or scripted
vehicle persistence.

LibertyCityPlates is supported as an optional compatibility target. This mod
does not include LibertyCityPlates itself.


Known limitations
-----------------

- Public beta: keep backups of saves and configuration files.
- Compatibility depends on the player's exact GTA IV CE, ScriptHookDotNet,
  FusionFix and mod setup.
- Mods that replace the same vehicle/service systems can interfere with this
  mod.


Credits
-------

See CREDITS.txt.


Licence
-------

The original code and documentation by ekzestean are released under the MIT
Licence.

This project is an unofficial fan-made mod and is not affiliated with Rockstar
Games or Take-Two Interactive.
