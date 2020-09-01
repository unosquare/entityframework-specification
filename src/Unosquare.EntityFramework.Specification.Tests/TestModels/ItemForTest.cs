using System;
using System.Linq.Expressions;
using Unosquare.EntityFramework.Specification.Common.Primitive;

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
    
    internal class SubItemSelector : Selector<ItemForTest, SubItemForTest>
    {
        public override Expression<Func<ItemForTest, SubItemForTest>> BuildExpression() =>
            x => x.SubItem;
    }
}