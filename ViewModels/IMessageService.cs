namespace Common
{
    using System.Threading.Tasks;

    public interface IMessageService
    {
        Task<int> ShowMessage(string message);
    }
}