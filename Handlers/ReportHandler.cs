using DataParser.Models.Analysis;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataParser
{

    public class ReportHandler
    {
        private Dictionary<ReportTypes, IReportHandler> _reportHandlers;

        public ReportHandler(Dictionary<ReportTypes, IReportHandler> reportHandlers)
        {
            _reportHandlers = reportHandlers;
        }
        public StringBuilder Report(Analysis analysis, ReportTypes reportType)
        {
            StringBuilder response = new StringBuilder();
            if (_reportHandlers.ContainsKey(reportType))
            {
                var handler = _reportHandlers.FirstOrDefault(x => x.Key == reportType);
                return handler.Value.Report(analysis);
            }
            return response;
        }
    }
}
