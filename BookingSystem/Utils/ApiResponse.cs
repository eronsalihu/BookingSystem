namespace BookingSystem.Utils
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode) =>
            statusCode switch
            {
                400 => "You have made a bad request.",
                401 => "Yoú are not authorized.",
                404 => "Resources were not found.",
                500 => "Internal server error.",
                _ => throw new NotImplementedException(),
            };

    }
}
