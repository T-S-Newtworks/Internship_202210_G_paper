//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはテンプレートから生成されました。
//
//     このファイルを手動で変更すると、アプリケーションで予期しない動作が発生する可能性があります。
//     このファイルに対する手動の変更は、コードが再生成されると上書きされます。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Internship_Template.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class T_DOCTOR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_DOCTOR()
        {
            this.T_CHART = new HashSet<T_CHART>();
        }
    
        public string ID { get; set; }
        public string NAME { get; set; }
        public string HOSPITAL_ID { get; set; }
        public string DEPARTMENT_CD { get; set; }
        public string GENDER { get; set; }
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> AGE { get; set; }
    
        public virtual T_HOSPITAL T_HOSPITAL { get; set; }
        public virtual T_DEPARTMENT T_DEPARTMENT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_CHART> T_CHART { get; set; }
    }
}
