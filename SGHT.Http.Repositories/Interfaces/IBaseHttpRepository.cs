namespace SGHT.Http.Repositories.Interfaces
{
    public interface IBaseHttpRepository
    {
        Task<HttpResponseMessage> SendGetRequestAsync(string url);
        Task<HttpResponseMessage> SendPostRequestAsync<T>(string url, T body) where T : class;
        Task<HttpResponseMessage> SendPatchRequestAsync<T>(string url, T body) where T : class;
        Task<HttpResponseMessage> SendDeleteRequestAsync<T>(string url, T id) where T : class;
    }
}
