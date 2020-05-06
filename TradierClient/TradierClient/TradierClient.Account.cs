﻿using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Tradier.Client.Models;

// ReSharper disable once CheckNamespace
namespace Tradier.Client
{
    public sealed partial class TradierClient
    {
        public async Task<Profile> GetUserProfile()
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, "user/profile");
            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            content = content.Replace("\"null\"", "null");

            return JsonConvert.DeserializeObject<ProfileRootObject>(content).Profile;
        }

        public async Task<Balances> GetBalances(string accountNumber)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, "accounts/" + accountNumber + "/balances");
            using var response = await _httpClient.SendAsync(request);
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                content = content.Replace("\"null\"", "null");

                return JsonConvert.DeserializeObject<BalanceRootObject>(content).Balances;
            }
        }

        public async Task<Positions> GetPositions(string accountNumber)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, "accounts/" + accountNumber + "/positions");
            using var response = await _httpClient.SendAsync(request);
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                content = content.Replace("\"null\"", "null");

                return JsonConvert.DeserializeObject<PositionsRootobject>(content).Positions;
            }
        }

        public async Task<History> GetHistory(string accountNumber, int page = 1, int limitPerPage = 25)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, $"accounts/{accountNumber}/history?page={page}&limit={limitPerPage}");
            using var response = await _httpClient.SendAsync(request);
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                content = content.Replace("\"null\"", "null");

                return JsonConvert.DeserializeObject<HistoryRootobject>(content).History;
            }
        }
    }
}