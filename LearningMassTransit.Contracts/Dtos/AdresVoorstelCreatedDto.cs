using LearningMassTransit.Contracts.Enums;

namespace LearningMassTransit.Contracts.Dtos;

public class AdresVoorstelCreatedDto
{
    public ActieEnum Actie = ActieEnum.ProposeStreetName;
    public string TicketId { get; set; }
}