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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class testEntities1 : DbContext
    {
        public testEntities1()
            : base("name=testEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Блюда> Блюда { get; set; }
        public virtual DbSet<Заказы> Заказы { get; set; }
        public virtual DbSet<Клиент> Клиент { get; set; }
        public virtual DbSet<Поставщики> Поставщики { get; set; }
        public virtual DbSet<Сотрудники> Сотрудники { get; set; }
        public virtual DbSet<Строка_заказа> Строка_заказа { get; set; }
        public virtual DbSet<Филиалы> Филиалы { get; set; }
    }
}
