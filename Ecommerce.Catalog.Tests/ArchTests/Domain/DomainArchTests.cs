using Ecommerce.BuildingBlocks.Domain;
using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;
using Xunit.Abstractions;

namespace Ecommerce.Catalog.Tests.ArchTests.Domain;

public sealed class DomainArchTests
{
    private readonly ITestOutputHelper _output;

    public DomainArchTests(ITestOutputHelper output)
    {
        _output = output;
    }

    private readonly Assembly assembly = Catalog.Domain.AssemblyReference.Assembly;

    [Fact]
    public void Domain_Should_Not_HaveDependencyOnOtherProjectsMoreThanDomainBuildingBlocks()
    {
        //Arrange
        string[] otherProjects = new string[]
        {
            References.ApplicationNamespace,
            References.InfrastructureNamespace,
            References.ApiNamespace,
            References.IntegrationEventsNamespace,
            References.ApplicationBuildingBlocksNamespace,
            References.InfrastuctureBuildingBlocksNamespace
        };

        //Act
        TestResult testResult = Types
            .InAssembly(assembly).That().ResideInNamespaceStartingWith(References.DomainNamespace)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        //Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Domain_Should_HaveDependencyOnDomainBuildingBlocksProject()
    {
        //Act
        var result = Types
            .InAssembly(assembly).That().ResideInNamespaceStartingWith(References.DomainNamespace)
            .Should()
            .NotHaveDependencyOn(References.DomainBuildingBlocksNamespace)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeFalse();
    }

    [Fact]
    public void DomainEntities_Should_BePublic()
    {
        //Act
        TestResult result = Types
            .InAssembly(assembly).That().ImplementInterface(typeof(IEntity))
            .Should()
            .BePublic()
            .GetResult();
        //Assert

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainEntities_Should_BeSealed()
    {
        //Act
        TestResult result = Types
            .InAssembly(assembly).That().ImplementInterface(typeof(IEntity))
            .Should()
            .BeSealed()
            .GetResult();
        
        //Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainEntities_Should_HaveAnEmptyConstructor()
    {
        IEnumerable<Type> entityTypes = Types
            .InAssembly(assembly)
            .That().ImplementInterface(typeof(IAggregateRoot))
            .And().ImplementInterface(typeof(IEntity))
            .GetTypes();

        List<Type> failingTypes = new List<Type>();
        foreach (var entityType in entityTypes)
        {
            var constructors = entityType.GetConstructors(BindingFlags.NonPublic |
                                                          BindingFlags.Instance);

            if (!constructors.Any(u => u.IsPrivate && u.GetParameters().Length == 0))
            {
                failingTypes.Add(entityType);
            }
        }

        failingTypes.Should().BeEmpty();
    }

    [Fact]
    public void DomainEntities_Should_HaveOnlyPrivateConstructor()
    {
        IEnumerable<Type> entityTypes = Types
            .InAssembly(assembly)
            .That().ImplementInterface(typeof(IEntity))
            .GetTypes();

        List<Type> failingTypes = new List<Type>();
        foreach (var entityType in entityTypes)
        {
            var constructors = entityType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

            if (constructors.Any(u => u.IsPublic))
            {
                failingTypes.Add(entityType);
            }
        }

        failingTypes.Should().BeEmpty();
    }

    [Fact]
    public void DomainInterfaces_Should_StartsWithI()
    {
        TestResult result = Types
            .InAssembly(assembly)
            .That().AreInterfaces()
            .Should().HaveNameStartingWith("I")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainEvents_Should_EndsWithEvent()
    {
        TestResult result = Types
            .InAssembly(assembly).That().ResideInNamespaceStartingWith(References.DomainNamespace)
            .And().ImplementInterface(typeof(IDomainEvent))
            .Should()
            .HaveNameEndingWith("Event")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainEvents_Should_BeSealed()
    {
        var result = Types
            .InAssembly(assembly).That().ResideInNamespaceStartingWith(References.DomainNamespace)
            .And().ImplementInterface(typeof(IDomainEvent))
            .Should()
            .BeSealed()
            .GetResult();

        if (!result.IsSuccessful)
        {
            _output.WriteLine("Failing classes");

            foreach (var failure in result.FailingTypeNames)
            {
                _output.WriteLine($"- {failure}");
            }
        }

        result.IsSuccessful.Should().BeTrue();  
    } 
}
