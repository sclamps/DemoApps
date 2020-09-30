using System;
using System.Net.Http;

namespace SquareRt.Core
{
    public class SquareRtCalculator : ISquareRtCalculator
    {
        readonly HttpClient _httpClient = new HttpClient ();

        public SquareRtCalculator ()
        {
            _httpClient.DefaultRequestHeaders.Add ("Ocp-Apim-Subscription-Key", "<your-api-key>");
        }
        public double Calculate (double number) => Math.Sqrt (number);
        
        //This uses Bing API to calculate. It is just an example
        // ReSharper disable once HeapView.BoxingAllocation
        // try {
        //     var url = "https://api.cognitive.microsoft.com/bing/v7.0/search?" + $"q=sqrt({number})&responseFilter=Computation";
        //     var response = await _httpClient.GetAsync (url).ConfigureAwait (false);
        //     var json = await response.Content.ReadAsStringAsync ().ConfigureAwait (false);
        //     var squareRt = JsonConvert.DeserializeObject<SquareRootResponse> (json);
        //
        //     return squareRt.Computation.Value;
        //     
        // } catch (HttpRequestException error) {
        //     Console.WriteLine(error.Message);
        //     return 0.0;
        // }
    }
}