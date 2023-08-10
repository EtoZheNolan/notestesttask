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
        {
            controller.ControllerName = controller.ControllerName.ToLower();
            
            var selector = controller.Selectors[0];
            selector.AttributeRouteModel!.Template = $"{_routePrefix}/{selector.AttributeRouteModel!.Template}";
        }
            
    }
}