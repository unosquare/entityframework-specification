namespace Unosquare.EntityFramework.Specification.Tests.TestModels
{
    internal class ItemForTest
    {
        public ItemForTest(string id, SubItemForTest subItem = null)
        {
            Id = id;
            SubItem = subItem;
        }
        public string Id { get; }
        
        public SubItemForTest SubItem { get; }
    }
}