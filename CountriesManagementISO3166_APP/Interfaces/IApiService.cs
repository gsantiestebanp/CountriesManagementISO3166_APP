namespace CountriesManagementISO3166_APP.Interfaces
{
    public interface IApiService<T>
    {
        T Speculative { get; }
        T UserInitiated { get; }
        T Background { get; }

    }
}

