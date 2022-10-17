using System;

namespace GameStore.Web.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Surname { get; set; } 
        
        
        // Сущность под вопросом
    }
}