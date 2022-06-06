namespace SympliDevelopment.Api.Extenstions
{
    public static class StringExtension
    {
        /// <summary>
        /// Removes [] from array values
        /// e.g. [1,2,3,4] will be reduced to 1,2,3,4
        /// </summary>
        /// <param name="str">string with comma separated numbers</param>
        /// <returns>same list without brackets</returns>
        public static string ToPlainArray(this string str)
        {
            if (str == null) return null;
            else if(str.Length < 2) return str; // could be 0 already
            return str.Substring(1, str.Length -2); // cut off brackets
        }
    }
}
