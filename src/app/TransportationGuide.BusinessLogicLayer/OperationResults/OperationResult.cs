using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportationGuide.BusinessLogicLayer.OperationResults
{
    public class OperationResult
    {
        public string Message { get; set; }
        public object ReturnValue { get; set; }
        public OperationResultStatus Status { get; set; }

        public OperationResult(string message)
        {
            Message = message;
        }

        public OperationResult(OperationResultStatus status, string message)
            : this(message)
        {
            Status = status;

        }

        public OperationResult(OperationResultStatus status, string message, object returnValue)
            : this(status, message)
        {
            ReturnValue = returnValue;
        }
    }
}