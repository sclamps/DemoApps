using System.Threading.Tasks;

namespace PayCardRecognizerSample
{
    public interface IPayCardRecognizerService
    {
        Task<PayCard> ScanAsync();
    }
}