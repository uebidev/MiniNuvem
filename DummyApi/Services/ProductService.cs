using Dominio.Entity;
using Newtonsoft.Json;
using Service.Path;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;

namespace Services
{
    public class ProductService
    {
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
                        ProductsCollection welcome = JsonConvert.DeserializeObject<ProductsCollection>(productsJson);
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
                        ProductsCollection welcome = JsonConvert.DeserializeObject<ProductsCollection>(productJson);
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
        public static async Task<bool> AddProductAsync(Product product)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string productJson = JsonConvert.SerializeObject(product);
                    StringContent content = new StringContent(productJson, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(PathService.PathProductAdd, content);

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
        public static async Task<bool> UpdateProductAsync(long id, Product product)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string updateUrl = $"{PathService.PathProductDelete}{id}";

                    string productJson = JsonConvert.SerializeObject(product);
                    StringContent content = new StringContent(productJson, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync(updateUrl, content);

                    return response.IsSuccessStatusCode;
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
                    string deleteUrl = "https://dummyjson.com/products/" + id;

                    HttpResponseMessage response = await client.DeleteAsync(deleteUrl);

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}

