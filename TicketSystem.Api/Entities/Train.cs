﻿using Routine.Api.Entities;
namespace TicketSystem.Api.Entities;

public class Train
{
    public int TrainId { get; set; }
    public int LineId { get; set; }
    public string TrainName { get; set; }
    public string TypeOfTrain { get; set; }
    public Line Line { get; set; }
    public string Time { get; set; }
}