using Insurance.Shared.ViewModels;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Service.Services
{
    public static class RequestService
    {
        public static async Task<List<OfferViewModel>> GetOffersAsync(string path, string Plate, string IdentityNumber, string LicenseSerialCode, string LicenseSerialNo)
        {
            var query = new Dictionary<string, string>()
            {
                ["Plate"] = Plate,
                ["IdentityNumber"] = IdentityNumber,
                ["LicenseSerialCode"] = LicenseSerialCode,
                ["LicenseSerialNo"] = LicenseSerialNo,
            };
            var uri = QueryHelpers.AddQueryString(path, query);

            List<OfferViewModel> offers = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    offers = await response.Content.ReadAsAsync<List<OfferViewModel>>();
                }
            }
           
            return offers;
        }
    }
}
