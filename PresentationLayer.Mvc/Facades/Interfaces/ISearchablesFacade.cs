using BusinessLayer.DTOs;
using Infrastructure.QueryObjects;
using PresentationLayer.Mvc.Models;

namespace PresentationLayer.Mvc.Facades.Interfaces;

public interface ISearchablesFacade
{
    Task<SearchablesFilterResultViewModel> GetSearchablesFilteredAsync(SearchablesFilterViewModel searchablesFilter);
}
