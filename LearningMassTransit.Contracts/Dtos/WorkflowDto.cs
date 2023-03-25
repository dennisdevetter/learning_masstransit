﻿using System;
using LearningMassTransit.Contracts.Enums;

namespace LearningMassTransit.Contracts.Dtos;

public class WorkflowDto
{
    public Guid WorkflowId { get; set; }
    public string UserId { get; set; }
    public DateTime CreationDate { get; set; }
    public string WorkflowType { get; set; }
    public string? Status { get; set; }
}