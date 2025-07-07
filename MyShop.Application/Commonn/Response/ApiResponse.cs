namespace LibraryApi.Application.Response;

public class ApiResponse<T>
{
    public bool isSuccess { get; set; }

    public int Status { get; set; }
    public string Message { get; set; }

    public T Data { get; set; }


    public static ApiResponse<T> Success(string message,T data,int status = 200)
    {
        return new ApiResponse<T>()
        {
            isSuccess = true,
            Message = message,
            Data = data,
            Status = status
        };
    }
    
    public static ApiResponse<T> Failed(string message,int status = 400)
    {
        return new ApiResponse<T>()
        {
            isSuccess = false,
            Message = message,
            Status = status
        };
    }
}
public class ApiResponseNoData
{
    public bool isSuccess { get; set; }
    public int Status { get; set; }
    
    public string? Message { get; set; }
    

    public static ApiResponseNoData Success(string? message,int status = 200)
    {
        return new ApiResponseNoData()
        {
            isSuccess = true,
            Message = message,
            Status = status
        };
    }
    
    public static ApiResponseNoData Failed(string? message,int status = 400)
    {
        return new ApiResponseNoData()
        {
            isSuccess = false,
            Message = message,
            Status = status
        };
    }
}