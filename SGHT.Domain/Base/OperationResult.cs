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

        public static OperationResult GetErrorResult(string message,dynamic? data= null, int? code = null)
        {
            return new()
            {
                Success = false,
                Message = message,
                Data = data,
                Code = code
            };
        }

        public static OperationResult GetSuccesResult(dynamic data, string? message = null, int? code = null) 
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
