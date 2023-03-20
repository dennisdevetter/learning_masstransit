namespace LearningMassTransit.Contracts.Dtos;

public class AdresVoorstelCreatingDto
{
    public string TicketId { get; set; }

    public AdresVoorstelCreatingDto(string ticketId)
    {
        TicketId = ticketId;
    }
}