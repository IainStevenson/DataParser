using DataParser.Models.Analysis;
using System.Text;

namespace DataParser
{
    public interface IReportHandler
    {
        StringBuilder Report(Analysis analysis);
    }
}
