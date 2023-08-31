using System;
using System.ComponentModel.DataAnnotations;
using task1.Models;

namespace card.Models
{
    public class Patient
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле IIN обязательно для заполнения")]
        public string IIN { get; set; } = "";
        [Required(ErrorMessage = "Поле FullName обязательно для заполнения")]
        public string FullName { get; set; } = "";
        [Required(ErrorMessage = "Поле Address обязательно для заполнения")]
        public string Address { get; set; } = "";
        [Required(ErrorMessage = "Поле Phone обязательно для заполнения")]
        public string Phone { get; set; } = "";
        public List<Visit> Visits { get; set; }  // Инициализация пустым списком
        public Patient()
        {
            Visits = new List<Visit>(); // Инициализируем в конструкторе
        }
    }
}

