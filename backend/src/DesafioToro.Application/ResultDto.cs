namespace DesafioToro.Application
{
    public class ResultDto
    {
        public ResultDto(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
