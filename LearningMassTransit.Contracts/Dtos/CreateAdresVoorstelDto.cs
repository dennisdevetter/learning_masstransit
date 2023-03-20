using LearningMassTransit.Contracts.Enums;

namespace LearningMassTransit.Contracts.Dtos;

public class CreateAdresVoorstelDto
{
    public string PostinfoId { get; set; }
    public string StraatnaamId { get; set; }
    public string Huisnummer { get; set; }
    public string Busnummer { get; set; }
    public PositieGeometrieMethodeEnum PositieGeometriemethode { get; set; }
    public PositieSpecificatieEnum PositieSpecificatie { get; set; }
    public string Positie { get; set; }
}