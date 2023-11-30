using Ecommerce.BuildingBlocks.Domain;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Catalog.Domain.Common.Rules;

public sealed class ImageFormatMustBePNGOrJPGRule //: IBusinessRule
{
    private const string JpgFileType = "image/jpeg";
    private const string PngFileType = "image/png";
    private readonly IFormFile _imageFile;

    public ImageFormatMustBePNGOrJPGRule(IFormFile formFile)
    {
        _imageFile = formFile;
    }

    public static string Message => "The image file type must be .jpg or .png";

    public bool IsBroken()
    {
        if (!_imageFile.ContentType.EndsWith(JpgFileType) &&
            !_imageFile.ContentType.EndsWith(PngFileType))
        {
            return true;
        }

        return false;
    }

}
