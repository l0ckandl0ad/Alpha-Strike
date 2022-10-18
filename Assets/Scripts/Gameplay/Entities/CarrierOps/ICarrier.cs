using System.Collections.Generic;
/// <summary>
/// An entity that can act as a carrier through its facilities
/// </summary>
public interface ICarrier : IEntity
{
    List<ICarrierFacility> CarrierFacilities { get; }
}