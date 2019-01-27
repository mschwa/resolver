namespace Resolver.Case.Api.Models
{
    public class Case
    {
        public string DisplayName { get; set; }
        public Insurer Insurer { get; set; }
        public Claimant Claimant { get; set; }
    }
}
