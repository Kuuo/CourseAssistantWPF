//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CourseAssistantWPF.Model {
    using System;
    using System.Collections.Generic;

    public partial class Rejoinder {
        public int ID { get; set; }
        public string StuID { get; set; }
        public string Judges { get; set; }

        public virtual Student Student { get; set; }
    }
}
