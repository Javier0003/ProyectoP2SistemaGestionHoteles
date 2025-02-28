using SGHT.Domain.Entities;

namespace SGHT.Domain.Base
{
    public class OperationResult
    {
        public OperationResult(){
            this.Success = true;
        }

        public bool Success { get; set; }
        public string? Message { get; set; }
        public dynamic? Data { get; set; }
        public int? Code { get; set; }

        public static OperationResult GetErrorResult(string message, int code, dynamic? data = null)
        {
            return new()
            {
                Success = false,
                Message = message,
                Data = data,
                Code = code
            };
        }

        public static OperationResult GetSuccesResult(dynamic data, int code, string? message = null)
        {
            return new()
            {
                Success = true,
                Message = message,
                Data = data,
                Code = code
            };
        }
    }
}
