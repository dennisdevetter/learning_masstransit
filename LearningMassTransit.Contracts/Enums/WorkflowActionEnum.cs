namespace LearningMassTransit.Contracts.Enums;

public enum WorkflowActieEnum
{
    None = 0,
    
    /* atomaire acties */
    ProposeStreetName = 100,
    ApproveAddress = 101,
    RejectAddress = 102,

    /* complexe acties */
    NieuwAdresMetStatusWijziging = 300
}