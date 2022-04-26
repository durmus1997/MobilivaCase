namespace MobilivaCase
{
    public class ApiResponse
    {
        public enum StatusCode { Success = 1, Failed = -1 }
        public StatusCode Status { get; set; }
        public string ResultMessage { get; set; }
        public int ErrorCode { get; set; }
        public object Data { get; set; }
    }
}
