﻿namespace TicketSystem.Api.Entities;

public class Station
{
    public int StationId { get; set; }
    public string StationName { get; set; }
    public bool IsTerminal { get; set; }
    public double Latitude { get; set; } //纬度
    public double Longitude { get; set; } //经度
    
}