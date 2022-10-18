using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BluforCV : Carrier
{
    private static string hullClass = "CV";
    private static int hullNumber;
    public BluforCV(string name)
    {
        Name = name;
        IFF = IFF.BLUFOR;
        MinSize = 35;
        MaxSize = 200;
        Structure = 200;
        VPCost = 200;
        PlatformType = SpacePlatformType.CV;

        Class = "Langley";
        ++hullNumber;
        HullName = hullClass + "-" + hullNumber.ToString();
        MaxSpeed = 33f;

        Weapons.Add(new LightLaser());
        Weapons.Add(new LightLaser());
        Weapons.Add(new LightLaser());
        Weapons.Add(new LightLaser());

        ICarrierCraftHoldingFacility hangar = new Hangar();
        ICarrierCraftHoldingFacility transferArea = new TransferArea();
        IFlightDeck flightDeck = new FlightDeck();

        CarrierFacilities.Add(hangar);
        CarrierFacilities.Add(transferArea);
        CarrierFacilities.Add(flightDeck);
        CarrierFacilities.Add(new FlightControl(hangar, transferArea, flightDeck));


        for (int i = 0; i < 12; i++)
        {
            ICarrierCapable fighter = new BluforFighter();
            fighter.SetHomeplate(this);
            hangar.TeleportCraftHere(fighter);
        }

        for (int i = 0; i < 18; i++)
        {
            ICarrierCapable scout = new BluforScoutCraft();
            scout.SetHomeplate(this);
            hangar.TeleportCraftHere(scout);
        }
    }
}
