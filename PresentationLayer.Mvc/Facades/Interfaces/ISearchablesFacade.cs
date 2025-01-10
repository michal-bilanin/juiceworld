using PresentationLayer.Mvc.Models;

namespace PresentationLayer.Mvc.Facades.Interfaces;

public interface ISearchablesFacade
{
    Task<SearchablesFilterResultViewModel> GetSearchablesFilteredAsync(SearchablesFilterViewModel searchablesFilter);
}