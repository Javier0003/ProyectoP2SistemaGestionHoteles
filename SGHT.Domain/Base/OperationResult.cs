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

        public static OperationResult GetErrorResult(string message, dynamic? data)
        {
            return new()
            {
                Success = false,
                Message = message,
                Data = data
            };
        }

        public static OperationResult GetSuccesResult(string message, dynamic? data)
        {
            return new()
            {
                Success = true,
                Message = message,
                Data = data
            };
        }
    }
}
