using multiplixe.twitter.client;
using System;

namespace multiplixe.twitter.clienttest
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new TwitterOAuthClient();

            var response = client.ObterURL(Guid.Parse("5F22E669-8CF2-4702-A828-B32E832A6BA6"), "voraxgg");

            Console.Write("URL", response.Item);

        }
    }
}
