using System.Threading.Channels;

namespace laborotorna5
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            NumberVerifController controller = new NumberVerifController();

            NumberVerificationResponseModel responseGet = await controller.GetData();
           
            Dictionary<string, NumberCodeGet> AllCodes = responseGet.NumbersCodeAll;
            foreach (var code in AllCodes)
            {
                Console.WriteLine(code);
            }
            await Console.Out.WriteLineAsync("/////////////////////////");

            NumberVerificationResponseModel responsePost = await controller.PostData("+380996141486");

            NumberCodePostWithNumber UkraineNumber = responsePost.numberPostParam;

            await Console.Out.WriteLineAsync(UkraineNumber.ToString());

        }
    }
}
