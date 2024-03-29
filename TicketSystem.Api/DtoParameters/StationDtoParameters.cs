﻿namespace TicketSystem.Api.DtoParameters
{
    //查询和搜索、分页的参数设置
    public class StationDtoParameters
    {
        //静态成员 也可以写个基类放在一起
        private const int MaxPageSize = 20;

        public string? StationName { get; set; }

        public int PageNumber { get; set; } = 1;
        private int _pageSize = 5;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
