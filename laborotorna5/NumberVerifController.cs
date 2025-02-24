using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace laborotorna5
{
    internal class NumberVerifController
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static string url;
        private static NumberVerificationResponseModel modelResponse = new NumberVerificationResponseModel();

        static NumberVerifController()
        {
            _httpClient.DefaultRequestHeaders.Add("apikey", "5pa94FNJvcUaf0hFdvREheVZyoXpOcNK");
        }
        private static async Task<string> ResponeURL()
        {
            string responseBody = "";
            url = "https://api.apilayer.com/number_verification/countries";
            try
            {
                using HttpResponseMessage responseMessage = await _httpClient.GetAsync(url);
                responseMessage.EnsureSuccessStatusCode();
                modelResponse.StatusCode = responseMessage.StatusCode;
                responseBody = await responseMessage.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                
            }
            return responseBody;
        }


        public async Task<NumberVerificationResponseModel> GetData()
        {
            string responseBody;
            try
            {
                responseBody = await ResponeURL();
                modelResponse.NumbersCodeAll = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, NumberCodeGet>>(responseBody);
                modelResponse.numberPostParam = null;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.ToString());
                modelResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            //await Console.Out.WriteLineAsync(modelResponse.ToString());
            return modelResponse;
        }
        public async Task<NumberVerificationResponseModel> PostData(string number)
        {
            if (number == null) {  throw new ArgumentNullException(nameof(number)); }
            url = $"https://api.apilayer.com/number_verification/validate?number={number}";
            string responseBody;
           

            try
            {
                using HttpResponseMessage responseMessage = await _httpClient.GetAsync(url);
                responseMessage.EnsureSuccessStatusCode();
                modelResponse.StatusCode = responseMessage.StatusCode;

                responseBody = await responseMessage.Content.ReadAsStringAsync();

                modelResponse.numberPostParam = System.Text.Json.JsonSerializer.Deserialize<NumberCodePostWithNumber>(responseBody);
                modelResponse.NumbersCodeAll = null;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }

            return modelResponse;
        }

    }
}
