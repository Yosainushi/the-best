//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BD
{
    using System;
    using System.Collections.Generic;
    
    public partial class Заказы
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Заказы()
        {
            this.Строка_заказа = new HashSet<Строка_заказа>();
        }
    
        public int id_заказа { get; set; }
        public Nullable<int> id_клиента { get; set; }
        public Nullable<int> id_Сотрудника { get; set; }
        public Nullable<decimal> Общая_сумма_заказа { get; set; }
        public System.DateTime Дата { get; set; }
    
        public virtual Клиент Клиент { get; set; }
        public virtual Сотрудники Сотрудники { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Строка_заказа> Строка_заказа { get; set; }
    }
}
