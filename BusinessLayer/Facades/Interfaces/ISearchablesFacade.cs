using BusinessLayer.DTOs;

namespace BusinessLayer.Facades.Interfaces;

public interface ISearchablesFacade
{
    Task<SearchablesFilterResultViewDto> GetSearchablesFilteredAsync(SearchablesFilterViewDto searchablesFilter);
}