using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        //ID
        public int Id { get; set; }

        //NAME
        [Required(ErrorMessage = "{0} required")] /*CAMPO OBRIGATÓRIO*/
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} Name size should the between {1} and {2}")]
        public string Name { get; set; }    


        //EMAIL
        [Required(ErrorMessage = "{0} required")] /*CAMPO OBRIGATÓRIO*/
        [EmailAddress(ErrorMessage ="Enter a valid email")] 
        [DataType(DataType.EmailAddress)] /*Cria um redirecionamento para envio de email deixando o email como link (necessário configurar email de envio)*/
        public string Email { get; set; }

        //DATA DE NASCIMENTO
        [Required(ErrorMessage = "{0} required")] /*CAMPO OBRIGATÓRIO*/
        [Display(Name ="Birth Date")] /*Alterar o front-end ou display da tabela, nesse caso o campo Data de Aniversário*/
        [DataType(DataType.Date)] /*Deixa somente a data, remove o campo horas*/
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        //SALARIO BASE
        [Required(ErrorMessage = "{0} required")] /*CAMPO OBRIGATÓRIO*/
        [Range(100.0,50000.0, ErrorMessage ="{0} must be from {1} to {2}")]
        [Display(Name = "Base Salary")] /*altera o front-end Salario Base, colocando espaço entre o nome*/
        [DisplayFormat(DataFormatString = "{0:F2}")] /*F2 = 2 casas decimais*/
        public double BaseSalary { get; set; }
        
        //DEPARTAMENTO
        public Department Department { get; set; }

        //ID DEPARTMENTO
        public int DepartmentId { get; set; }
        
        //LISTA DO TIPO GENERICO ICOLLECTION
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
