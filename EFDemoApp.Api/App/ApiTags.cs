namespace EFDemoApp.Api.App
{
    internal static class ApiTags
    {
        private static Dictionary<string, string> apisTags = new Dictionary<string, string>()
        {
            { "PeopleApi", "People" },
            { "SampleApi", "Samples" },
        };

        internal static string GetTag(string key) => apisTags[key];
        internal static string GetPeopleTag() => apisTags["PeopleApi"];
        internal static string GetSampleTag() => apisTags["SampleApi"];
    }
}
