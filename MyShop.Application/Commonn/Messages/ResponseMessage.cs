namespace MyShop.Application.Commonn.Messages;

public class StatusMessageProvider
{
    private static readonly Dictionary<int, string> _messages = new()
    {
        #region SuccessUser

        [200] = "حساب کاربری با موفقیت ایجاد شد",
        [201] = "حساب کاربری با موفقیت ویرایش شد",
        [202] = "حساب کاربری با موفقیت حذف شد",
        [203] = "با موفقیت به حساب خود وارد شدید",

        #endregion

        #region SuccessProduct

        [210] = "محصول با موفقیت ایجاد شد",
        [211] = "محصول با موفقیت ویرایش شد",
        [212] = "محصول با موفقیت حذف شد",

        #endregion

        #region FailedUser

        [400] = "شماره تماس یا ایمیل در سایت وجود دارد لطفا موارد ایمیل یا موبایل دیگری راامتحان کنید",
        [401] = "عملیات شما با شکست مواجه شده است",
        [402] = "حساب کاربری شما پیدا نشد",

        #endregion

        #region FailedProduct

        [410] = "ایمیل محصول قبلا در سایت استفاده شده است",
        [411] = "عملیات شما با شکست مواجه شده است",
        [412] = "محصول شما پیدا نشد",
        [413] = "شما مجوز برای تقیر این محصول را ندارید",

        #endregion
    };

    public string? GetMessage(int statusCode)
    {
        return _messages.GetValueOrDefault(statusCode);
    }
}