namespace AhmedTrading.Repository
{
    public class DbResponse<TObject>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public TObject Data { get; set; }
    }

    public class DbResponse
    {
        public DbResponse()
        {

        }
        public DbResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}