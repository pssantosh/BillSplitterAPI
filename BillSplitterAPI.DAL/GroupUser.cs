//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BillSplitterAPI.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class GroupUser
    {
        public long GroupUserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Comment { get; set; }
        public long GroupId { get; set; }
    
        public virtual Group Group { get; set; }
    }
}
