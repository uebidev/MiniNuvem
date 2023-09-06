using Dominio.Entities;
using Dominio.Entities.ProductValidation;
using FluentValidation;
using Newtonsoft.Json;
using Service.Path;
using System.Net;
using System.Text;

namespace Services
{
    public class ProductService : IDisposable
    {
        private bool disposedValue;
        public static async Task<List<Product>> GetAllAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(PathService.PathProductsUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var productsJson = await response.Content.ReadAsStringAsync();
                        ProductCollection welcome = JsonConvert.DeserializeObject<ProductCollection>(productsJson);
                        List<Product> products = welcome.Products.ToList();
                        return products;
                    }
                    else
                    {
                        throw new Exception("erro");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
        public static async Task<Product> GetByIdAsync(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(PathService.PathProductId + id);
                    if (response.IsSuccessStatusCode)
                    {
                        var productJson = await response.Content.ReadAsStringAsync();
                        Product product = JsonConvert.DeserializeObject<Product>(productJson);
                        return product;
                    }
                    else
                    {
                        throw new Exception("Produto não encontrado.");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static async Task<List<Product>> GetNameAsync(string name)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(PathService.PathProductSearch + name);
                    if (response.IsSuccessStatusCode)
                    {
                        var productJson = await response.Content.ReadAsStringAsync();
                        ProductCollection welcome = JsonConvert.DeserializeObject<ProductCollection>(productJson);
                        List<Product> products = welcome.Products.ToList();
                        return products;
                    }
                    else
                    {
                        throw new Exception("Erro ao obter Produtos pelo nome");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static async Task<Product> AddProductAsync(Product product)
        {
            var httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(product);

            using (var response = await httpClient.PostAsync(PathService.PathProductAdd,
                new StringContent(json, Encoding.UTF8, "application/json")))
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return response.StatusCode == HttpStatusCode.NoContent
                    ? null
                    : JsonConvert.DeserializeObject<Product>(responseBody);
            }
        }
        public static async Task<Product> UpdateProductAsync(long id, Product product)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string updateUrl = $"{PathService.PathProductDelete}{id}";

                    string productJson = JsonConvert.SerializeObject(product);
                    StringContent content = new StringContent(productJson, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync(updateUrl, content);

                    return product;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
        public static async Task<bool> DeleteProductAsync(long id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.DeleteAsync(PathService.PathProductDelete + id);
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ProductService()
        // {
        //     // Não altere este código. Coloque o código de limpeza no método 'Dispose(bool disposing)'
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Não altere este código. Coloque o código de limpeza no método 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

}