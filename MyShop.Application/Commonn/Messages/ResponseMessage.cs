namespace MyShop.Application.Feature.Product.Command.Messages;

public class StatusMessageProvider
{
    private static readonly Dictionary<int, string> _messages = new()
    {
        [200] = "حساب کاربری با موفقیت ایجاد شد",
        [201] = "حساب کاربری با موفقیت ویرایش شد",
        [203] = "حساب کاربری با موفقیت حذف شد",
        [400] = "شماره تماس یا ایمیل در سایت وجود دارد لطفا موارد ایمیل یا موبایل دیگری راامتحان کنید",
        [401] = "عملیات شما با شکست مواجه شده است",
        [402] = "حساب کاربری شما پیدا نشد",
      
    };

    public string? GetMessage(int statusCode)
    {
        return _messages.GetValueOrDefault(statusCode);
    }
}
