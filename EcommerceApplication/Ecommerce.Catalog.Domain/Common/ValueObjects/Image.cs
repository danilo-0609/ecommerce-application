using Ecommerce.BuildingBlocks.Domain;
using Ecommerce.Catalog.Domain.Common.Rules;
using Ecommerce.Catalog.Domain.Products.Errors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Catalog.Domain.Common.ValueObjects;

public sealed record Image : ValueObject
{
    public string UrlValue { get; private set; }

    public IFormFile ImageFile { get; private set; }

    public static ErrorOr<Image> Create(string urlValue, IFormFile imageFile)
    {
        var checkingRules = CheckRules(imageFile);

        if (checkingRules.IsError)
        {
            return checkingRules.FirstError;
        }

        return new Image(urlValue, imageFile);

    }

    private static ErrorOr<Unit> CheckRules(IFormFile imageFile)
    {
        ImageFormatMustBePNGOrJPGRule imageFormatRule = new(imageFile);

        if (imageFormatRule.IsBroken())
        {
            return ProductErrors.Images.InvalidFormat(ImageFormatMustBePNGOrJPGRule.Message);
        }

        ImageSizeMustBeLessThan10MbRule imageSizeRule = new(imageFile);

        if (imageSizeRule.IsBroken())
        {
            return ProductErrors.Images.ExcessiveSize(ImageSizeMustBeLessThan10MbRule.Message);
        }

        return Unit.Value;
    }

    private Image(string urlValue, IFormFile imageFile)
    {
        UrlValue = urlValue;
        ImageFile = imageFile;
    }
}
