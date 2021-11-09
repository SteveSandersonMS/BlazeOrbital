using System.Web;

namespace BlazeOrbital.Data;

public partial class Part
{
    public string Url => $"https://letmebingthatforyou.com/?q={HttpUtility.UrlEncode(Name)}";
}
