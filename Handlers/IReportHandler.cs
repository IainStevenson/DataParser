using System.Text;

namespace DataParser
{
    public interface IReportHandler
    {
        StringBuilder Report(Analysis analysis);
    }
}
