using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HeatApp.Models;
using HeatApp.Models.HeatModels.Bus;

namespace HeatApp.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDTO>> Get(int busID);
        Task<IEnumerable<TypeReportDTO>> GetTypeReport();
        Task<bool> Report(ReportDTO report);
        Task<bool> Save(ReviewDTO report);
    }
}