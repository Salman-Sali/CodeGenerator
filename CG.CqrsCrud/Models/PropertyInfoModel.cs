namespace CG.CqrsCrud.Models
{
    public class PropertyInfoModel
    {
        public PropertyMetaData Info { get; set; }
        public List<PropertyMetaData> SubProperties { get; set; }
    }

    public class PropertyMetaData
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
