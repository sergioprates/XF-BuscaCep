using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BuscaCep.Clients
{
    class ViaCepHttpClient
    {
        private static Lazy<ViaCepHttpClient> _lazy = new Lazy<ViaCepHttpClient>(() => new ViaCepHttpClient());
        private HttpClient _httpClient;

        public static ViaCepHttpClient Current { get => _lazy.Value; }

        private ViaCepHttpClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> BuscarCep(string cep)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cep))
                    throw new ArgumentException("Informe o CEP.");

                using (var response = await _httpClient.GetAsync($"http://viacep.com.br/ws/{cep}/json"))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new InvalidOperationException("Erro ao consultar o CEP!");

                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
