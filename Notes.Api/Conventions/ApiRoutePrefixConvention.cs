using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Notes.Api.Conventions;

public class ApiRoutePrefixConvention : IApplicationModelConvention
{
    private readonly string _routePrefix;

    public ApiRoutePrefixConvention(string routePrefix)
    {
        _routePrefix = routePrefix;
    }

    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
            controller.Selectors[0].AttributeRouteModel = new AttributeRouteModel
                { Template = $"{_routePrefix}/{controller.ControllerName.ToLower()}" };
    }
}