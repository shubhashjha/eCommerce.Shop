using eCommerce.Web.Models;
using eCommerce.Web.Services.IServices;
using Newtonsoft.Json;
using System.Text;

namespace eCommerce.Web.Services
{
    public class BaseServices : IBaseServices
    {
        private readonly IHttpClientFactory httpClientFactory;
        public BaseServices(IHttpClientFactory _httpClientFactory) 
        {
            httpClientFactory = _httpClientFactory;
        }
        public async Task<ResponseDTO> SendAsync(RequestDto request) 
        {
            try
            {
                HttpClient client = httpClientFactory.CreateClient("eCommerceAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Content-Type", "application/json");
                //Token
                message.RequestUri = new Uri(request.URL);
                if (request.RequestBody is not null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(request.RequestBody), Encoding.UTF8, "application/json");
                }
                switch (request.APIType)
                {
                    case Utility.SystemEnums.APITYPE.GET:
                        message.Method = HttpMethod.Get;
                        break;
                    case Utility.SystemEnums.APITYPE.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case Utility.SystemEnums.APITYPE.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case Utility.SystemEnums.APITYPE.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case Utility.SystemEnums.APITYPE.PATCH:
                        message.Method = HttpMethod.Patch;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                HttpResponseMessage httpResponse = null;

                httpResponse = await client.SendAsync(message);

                switch (httpResponse.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case System.Net.HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Denial" };
                    case System.Net.HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    case System.Net.HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };
                    default:
                        var apiresponse = await httpResponse.Content.ReadAsStringAsync();
                        var apiRsponseDTO = JsonConvert.DeserializeObject<ResponseDTO>(apiresponse);
                        return apiRsponseDTO;

                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
