using eCommerce.Web.Models;
using eCommerce.Web.Services.IServices;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace eCommerce.Web.Services
{
    public class BaseServices : IBaseServices
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ITokenProvider tokenProvider;

        public BaseServices(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            this.httpClientFactory = httpClientFactory;
            this.tokenProvider = tokenProvider;
        }

        public async Task<ResponseDTO> SendAsync(RequestDto request, bool withBearer = true)
        {
            try
            {
                HttpClient client = httpClientFactory.CreateClient("eCommerceAPI");
                HttpRequestMessage message = new();

                if (request.ContentType == Utility.SystemEnums.CONTENTTYPE.MultipartFormData)
                {
                    message.Headers.Add("Accept", "*/*");
                }
                else
                {
                    message.Headers.Add("Accept", "application/json");
                }

                if (withBearer)
                {
                    var token = tokenProvider.GetToken();
                    if (!string.IsNullOrWhiteSpace(token))
                    {
                        message.Headers.Add("Authorization", $"Bearer {token}");
                    }
                }

                message.RequestUri = new Uri(request.URL);

                if (request.ContentType == Utility.SystemEnums.CONTENTTYPE.MultipartFormData)
                {
                    var content = new MultipartFormDataContent();

                    if (request.RequestBody is not null)
                    {
                        foreach (var prop in request.RequestBody.GetType().GetProperties())
                        {
                            var value = prop.GetValue(request.RequestBody);
                            if (value is IFormFile formFile)
                            {
                                content.Add(new StreamContent(formFile.OpenReadStream()), prop.Name, formFile.FileName);
                            }
                            else
                            {
                                content.Add(new StringContent(value?.ToString() ?? string.Empty), prop.Name);
                            }
                        }
                    }

                    message.Content = content;
                }
                else if (request.RequestBody is not null)
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

                HttpResponseMessage httpResponse = await client.SendAsync(message);

                switch (httpResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Denial" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };
                    default:
                        var apiresponse = await httpResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDTO>(apiresponse);
                        return apiResponseDto ?? new ResponseDTO
                        {
                            IsSuccess = false,
                            Message = "Unable to deserialize response."
                        };
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
