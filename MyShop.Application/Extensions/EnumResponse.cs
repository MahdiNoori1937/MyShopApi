using LibraryApi.Application.Response;

namespace LibraryApi.Application.Extensions;

public class EnumResponse<TEnum> where TEnum : Enum
{
    public ApiResponseNoData EnumReturn(TEnum value)
    {
        return new ApiResponseNoData
        {
            Status = Convert.ToInt32(value),
            Message = value.GetDisplayName(),
            isSuccess = true
        };
    }
}
