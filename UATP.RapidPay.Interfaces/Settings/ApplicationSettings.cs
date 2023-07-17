namespace UATP.RapidPay.Interfaces.Settings;

public class ApplicationSettings
{
    #region Singleton
    public static ApplicationSettings Config { get; private set; } = null;

    public static void Init(ApplicationSettings configuration)
    {
        if (Config is not null) throw new NotSupportedException();

        Config = configuration;
    }
    #endregion

    public string EncryptKey { get; set; } = string.Empty;
}
