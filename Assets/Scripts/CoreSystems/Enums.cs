public enum ModuleType { Base, Weapon, Propulsion, Hull, Sensor, FlightControl, Hangar, TransferArea, FlightDeck, EWAR, Armor, Shield} // nah.....

public enum SpacePlatformType { BASE, DD, CA, CL, BB, CV, FTR, SCOUT }

public enum IFF { EMPTY, ASTRO, BLUFOR, OPFOR }

public enum WeaponType { Kinetic, Laser, Plasma, Launcher } // Is it really the way to approach this? DamageType maybe?

public enum MessagePrecedence { ROUTINE, PRIORITY, IMMEDIATE, FLASH }

public enum CommandState { READY, RUNNING, COMPLETED }

public enum CarrierOpsState { Unready, Ready, InFlight }