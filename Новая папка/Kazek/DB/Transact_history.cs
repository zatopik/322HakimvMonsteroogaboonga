//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kazek.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transact_history
    {
        public int Trans_ID { get; set; }
        public Nullable<System.DateTime> Trans_date { get; set; }
        public Nullable<int> Prof_id { get; set; }
        public string Trans_Type { get; set; }
    
        public virtual Pofile Pofile { get; set; }
    }
}
