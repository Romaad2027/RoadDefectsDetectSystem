using System.Collections.Generic;
using System.Linq;

namespace Store.Models
{
    public class BulkAddAgentDatasRequestModel
    {
        public IEnumerable<ProcessedAgentDataRequestModel> Models { get; set; } =
            Enumerable.Empty<ProcessedAgentDataRequestModel>();
    }
}