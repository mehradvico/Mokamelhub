namespace Utility.BackgroundTask.Iface
{
    public interface IBackgroundTask
    {
        Task StartSyncSmsAsync();
        Task StartSyncCloseTicketAsync();
        Task StartSyncExpiredDiscountAsync();
    }
}
