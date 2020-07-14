namespace AhmedTrading.Data
{
    public class PageLinkAssign
    {
        public int LinkAssignId { get; set; }
        public int RegistrationId { get; set; }
        public int LinkId { get; set; }

        public virtual PageLink Link { get; set; }
        public virtual Registration Registration { get; set; }
    }
}
