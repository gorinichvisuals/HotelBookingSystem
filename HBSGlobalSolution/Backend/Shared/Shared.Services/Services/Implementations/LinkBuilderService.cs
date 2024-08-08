namespace Shared.Services.Services.Implementations;

internal sealed class LinkBuilderService(IOptions<CloudFrontOptions> options) : ILinkBuilderService
{
    private readonly CloudFrontOptions options = options.Value;

    public string? CreatePhotoLink(string imageIdFromAWS)
    {
        return imageIdFromAWS is not null
            ? $"{options.CloudFront}/{imageIdFromAWS}"
            : null;
    }
}