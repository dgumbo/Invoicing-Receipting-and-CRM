using invoice_demo_app.Basic.models;
using System;
using System.Collections.Generic;

namespace invoice_app.Basic.models
{
    public abstract class ItemWithList<B> : BaseEntity where B : BaseEntity
    {
        public abstract List<B> GetListItems();

        public override void ValidateBaseEntityProperties()
        {
            GetListItems().ForEach(item => item.ValidateBaseEntityProperties());
            base.ValidateBaseEntityProperties();
        }
    }
}
