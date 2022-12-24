namespace CG.CqrsCrud.Attributes.Commons
{
    public class PluralAttribute : Attribute
    {
        string plural;
        public PluralAttribute(string plural)
        {
            this.plural = plural;
        }

        public string GetPlural()
        {
            return plural;
        }
    }
}
