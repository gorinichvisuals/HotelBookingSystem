namespace Mono.PaymentService.Services.Implementations;

public sealed class MonoPayService(
    IHttpClientFactory httpClientFactory,
    IOptions<BookingUrlOptions> options) : IMonoPayService
{
    private readonly BookingUrlOptions options = options.Value;

    public async Task<ApplicationResult<InvoiceMonoGetDTO>> CreateInvoice(InvoiceMonoCreateDTO createDTO)
    {
        HttpClient httpClient = httpClientFactory.CreateClient(HttpClientConstants.MonoBank);

        string urlAddress = httpClient.BaseAddress + SpecificUrl.CreateInvoice;

        InvoiceCreateModel invoiceCreateModel = new()
        {
            Amount = createDTO.Amount,          
            RedirectURL = options.RedirectUrl,
            WebHookUrl = options.WebHookUrl,
        };

        JsonContent content = JsonContent.Create(invoiceCreateModel);

        HttpResponseMessage? httpResponseMessage = await httpClient.PostAsync(urlAddress, content);

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            ErrorModel? error = await httpResponseMessage!.Content.ReadFromJsonAsync<ErrorModel>();

            return ApplicationResult<InvoiceMonoGetDTO>
                .Fail((int)httpResponseMessage.StatusCode, $"Error: {error!.ErrCode}, {error!.ErrText!}");
        }

        InvoiceMonoGetDTO? result = await httpResponseMessage!.Content.ReadFromJsonAsync<InvoiceMonoGetDTO>();

        return ApplicationResult<InvoiceMonoGetDTO>.Success((int)httpResponseMessage.StatusCode, result!);
    }
}