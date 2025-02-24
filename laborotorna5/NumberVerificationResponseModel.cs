using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace laborotorna5
{
    internal class NumberVerificationResponseModel
    {
        public HttpStatusCode StatusCode { get; set; }
        public Dictionary<string, NumberCodeGet> NumbersCodeAll { get; set; }

        public NumberCodePostWithNumber numberPostParam { get; set; }
        public string ShowDataGetAndStatusCode()
        {
            StringBuilder result = new StringBuilder($"Statuc code {(int)StatusCode} {StatusCode}\n");
            if (NumbersCodeAll != null)
            {
                foreach (var item in NumbersCodeAll)
                {
                    result.Append($"{item.Key}: {item.Value}\n");
                }
            }
            return result.ToString();
        }


    }

    internal class NumberCodeGet
    {
        public string country_name { get; set; }
        public string dialling_code { get; set; }

        public override string ToString()
        {
            return $"Country name - {country_name} code - {dialling_code}";
        }
    }

    internal class NumberCodePostWithNumber
    {
        public string carrier { get; set; }
        public string country_code { get; set; }
        public string country_name { get; set; }
        public string country_prefix { get; set; }
        public string international_format { get; set; }
        public string line_type { get; set; }
        public string local_format { get; set; }
        public string location { get; set; }
        public string number { get; set; }
        public bool valid { get; set; }

        public override string ToString()
        {
            return $"carrier: {carrier}\n" +
                   $"country_code: {country_code}\n" +
                   $"country_name: {country_name}\n" +
                   $"country_prefix: {country_prefix}\n" +
                   $"international_format: {international_format}\n" +
                   $"line_type: {line_type}\n" +
                   $"local_format: {local_format}\n" +
                   $"location: {location}\n" +
                   $"number: {number}\n" +
                   $"valid: {valid}\n";
        }
    }
}
