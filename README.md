# HammerFreedom

Build without limits in Valheim Hammer worlds.

HammerFreedom activates automatically when you join a world using the **Hammer** preset. It gives you:

- Unlimited stamina.
- No fall damage.
- No cold, freezing, or frost damage.
- No equipment durability loss.
- Safe, speed-limited flight without admin access.
- Your character and held item are hidden while flying by default.

## Flight controls

| Control | Action |
|---|---|
| `Z` (configurable) | Toggle flight |
| `Space` | Fly up |
| `Left Control` | Fly down |
| `Shift` | Fly faster |

## BepInEx configuration

Settings are stored in `BepInEx/config/Landoria.HammerFreedom.cfg`.

| Section | Setting | Default | Description |
|---|---|---|---|
| `Controls` | `ToggleShortcut` | `Z` | Shortcut used to toggle flight. On QWERTZ keyboards, use `Y` for the physical Z key. |
| `Flight` | `Speed` | `5` | Flight speed in metres per second, from `1` to `20`. Holding Shift doubles it. |
| `Flight` | `HideCharacter` | `true` | Hides your character and held item while flying and restores them after landing. |

[Watch flight in action on YouTube](https://youtu.be/wUBgHzN5hG8).

## Installation

Install HammerFreedom on your client. The server does not need the mod.

## Support

Report bugs through [GitHub Issues](https://github.com/landoria-gaming/Landoria.HammerFreedom/issues).
