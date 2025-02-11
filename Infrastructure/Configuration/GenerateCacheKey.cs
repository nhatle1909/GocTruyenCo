namespace Infrastructure.Configuration
{
    public class CacheKeyUtils
    {
        public static string GenerateCacheKey(string[] searchField, string[] searchValue,
                                    string sortField, bool isAsc, int pageSize, int skip)
        {
            string key = $"Search_{sortField}_{isAsc}_{pageSize}_{skip}";

            if (searchField != null && searchValue != null && searchField.Length > 0 && searchValue.Length > 0)
            {
                for (int i = 0; i < searchField.Length; i++)
                {
                    key += $"_{searchField[i]}_{searchValue[i]}";
                }
            }
            return key;
        }
        public static string GenerateCacheKeyMinor(string[] searchField, string[] searchValue)
        {
            string key = $"Search_";
            if (searchField != null && searchValue != null && searchField.Length > 0 && searchValue.Length > 0)
            {
                for (int i = 0; i < searchField.Length; i++)
                {
                    key += $"_{searchField[i]}_{searchValue[i]}";
                }
            }
            return key;
        }
    }
}
