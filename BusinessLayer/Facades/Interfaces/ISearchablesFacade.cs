using PresentationLayer.Mvc.Areas.Customer.Models;

namespace BusinessLayer.Facades.Interfaces;

public interface ISearchablesFacade
{
    Task<SearchablesFilterResultViewDto> GetSearchablesFilteredAsync(SearchablesFilterViewDto searchablesFilter);
}