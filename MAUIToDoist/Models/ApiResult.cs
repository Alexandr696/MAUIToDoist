using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIToDoist.Models
{
    public enum ApiResultStatus
    {
        Success,
        Warning,
        Error
    }
    public class ApiResult<T>
    {
        public ApiResultStatus Status { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
