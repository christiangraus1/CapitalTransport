using System;
using System.Diagnostics;

namespace Github.BusinessLayer.Extensions;

public static class ExtensionMethods
{
    public static List<string> RemoveDuplicates(this List<string> items, bool ignoreCase = true, bool stripExtraSpace = true)
    {
        var tracking = new List<string>();
        var result = new List<string>();

        foreach(var item in items)
        {
            var track = ignoreCase ? item.ToLower() : item;

            if (stripExtraSpace)
                track = track.Trim();

            if(tracking.Where(e => e == track).FirstOrDefault() == null)
            {
                tracking.Add(track);
                result.Add(item);
            }
        }

        return result;
    }
}
