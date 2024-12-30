using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using PresentationLayer.Mvc.Models;

namespace PresentationLayer.Mvc.Shared;

public static class UrlHelperExtensions
{
    public static string? GeneratePaginationUrl(this UrlHelper urlHelper, SearchablesFilterViewModel query, object key, object value, string action = "Index")
    {
        var updatedQuery = new SearchablesFilterViewModel();
        foreach (var prop in query.GetType().GetProperties())
        {
            prop.SetValue(updatedQuery, prop.GetValue(query));
        }

        var propToUpdate = updatedQuery.GetType().GetProperty(key.ToString() ?? string.Empty);
        if (propToUpdate != null)
        {
            propToUpdate.SetValue(updatedQuery, value);
        }

        return urlHelper.Action(action, "Product", updatedQuery);
    }
}
