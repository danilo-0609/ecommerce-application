using Ecommerce.Catalog.Domain.Comments;
using Ecommerce.Catalog.Domain.Products;

namespace Ecommerce.Catalog.Tests.UnitTests.Domain.Comments;

public sealed class CommentTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_Should_Return_Null_WhenCommentValueIsNullOrEmpty(string? value)
    {
        ProductId productId = ProductId.Create(Guid.NewGuid());

        //Act
        Comment? comment = Comment.Create(Guid.NewGuid(),
            productId, 
            value,
            DateTime.UtcNow);

        //Assert
        Assert.Null(comment);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Update_Should_Return_Null_WhenCommentValueIsNullOrEmpty(string? value)
    {
        ProductId productId = ProductId.Create(Guid.NewGuid());

        //Act
        Comment? comment = Comment.Update(Guid.NewGuid(),
            productId,
            value,
            DateTime.UtcNow,
            DateTime.Now);

        //Assert
        Assert.Null(comment);
    }

    [Theory]
    [InlineData(null)]
    public void Create_Should_ReturnNull_WhenProductIdIsNullOrEmpty(ProductId? value)
    {
        //Act
        Comment? comment = Comment.Create(Guid.NewGuid(),
            value,
            "Comment",
            DateTime.UtcNow);
        //Assert
        Assert.Null(comment);
    }

    [Theory]
    [InlineData(null)]
    public void Update_Should_ReturnNull_WhenProductIdIsNullOrEmpty(ProductId? value)
    {
        //Act
        Comment? comment = Comment.Update(Guid.NewGuid(),
            value,
            "Comment",
            DateTime.UtcNow,
            DateTime.Now);

        //Assert
        Assert.Null(comment);
    }
}
