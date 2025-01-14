using BusinessLayer.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using PresentationLayer.Mvc.Areas.Customer.Models;
using PresentationLayer.Mvc.Models;

namespace PresentationLayer.Mvc.Shared;

public static class UrlHelperExtensions
{
    public static string? GeneratePaginationUrl(this UrlHelper urlHelper, SearchablesFilterViewDto query, object key,
        object value, string action = "Index")
    {
        var updatedQuery = new SearchablesFilterViewDto();
        foreach (var prop in query.GetType().GetProperties()) prop.SetValue(updatedQuery, prop.GetValue(query));

        var propToUpdate = updatedQuery.GetType().GetProperty(key.ToString() ?? string.Empty);
        if (propToUpdate != null) propToUpdate.SetValue(updatedQuery, value);

        return urlHelper.Action(action, "Product", updatedQuery);
    }
}