namespace Shared.Services.Services.Abstractions;

public interface ILinkBuilderService
{
    string? CreatePhotoLink(string imageIdFromAWS);
}