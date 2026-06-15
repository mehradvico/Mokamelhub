namespace Application.Common.Enumerable
{
    public enum ResultCodeEnum : int
    {
        None = 0,

        SignUp = 1,
        OneFactor = 2,
        TwoFactor = 3,
        Locked = 4,

        VerifyCodeCountError = 5,

    }
}
