using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace DataAccess.Entities
{
    [Table("personas")]
    public class Persona
    {
        [Key]
        [Column("id_persona")]
        public int IdPersona { get; private set; }

        [Required]
        [Column("legajo")]
        public int Legajo { get; private set; }

        [Required]
        [Column("nombre")]
        [StringLength(30)]
        public string Nombre { get; private set; } = null!;

        [Required]
        [Column("apellido")]
        [StringLength(30)]
        public string Apellido { get; private set; } = null!;

        [NotMapped]
        public string NombreCompleto => $"{Nombre} {Apellido}";

        [Column("id_tipo_doc")]
        public int IdTipoDoc { get; private set; }

        [Required]
        [Column("num_doc")]
        [StringLength(20)]
        public string NumDoc { get; private set; } = null!;

        [Column("fecha_nacimiento")]
        public DateTime? FechaNacimiento { get; private set; }

        [Column("cuil")]
        [StringLength(15)]
        public string? Cuil { get; private set; }

        [Column("calle")]
        [StringLength(50)]
        public string? Calle { get; private set; }

        [Column("altura")]
        [StringLength(30)]
        public string? Altura { get; private set; }

        [Column("id_localidad")]
        public int IdLocalidad { get; private set; }

        [Column("id_genero")]
        public int IdGenero { get; private set; }

        [Column("correo")]
        [StringLength(100)]
        public string? Correo { get; private set; }

        [Column("celular")]
        [StringLength(30)]
        public string? Celular { get; private set; }

        [Column("fecha_ingreso")]
        public DateTime FechaIngreso { get; private set; }

        [ForeignKey("IdTipoDoc")]
        public virtual TipoDoc? TipoDoc { get; private set; }

        [ForeignKey("IdLocalidad")]
        public virtual Localidad? Localidad { get; private set; }

        [ForeignKey("IdGenero")]
        public virtual Genero? Genero { get; private set; }

        private Persona() { } // EF Core constructor

        public Persona(int legajo, string nombre, string apellido, int idTipoDoc, string numDoc, DateTime? fechaNacimiento, string? cuil, string? calle, string? altura, int idLocalidad, int idGenero, string? correo, string? celular, DateTime fechaIngreso)
        {
            Validate(legajo.ToString(), nombre, apellido, numDoc, cuil, calle, altura, correo);

            Legajo = legajo;
            Nombre = nombre;
            Apellido = apellido;
            IdTipoDoc = idTipoDoc;
            NumDoc = numDoc;
            FechaNacimiento = fechaNacimiento;
            Cuil = cuil;
            Calle = calle;
            Altura = altura;
            IdLocalidad = idLocalidad;
            IdGenero = idGenero;
            Correo = correo;
            Celular = celular;
            FechaIngreso = fechaIngreso;
        }

        public void Update(int legajo, string nombre, string apellido, int idTipoDoc, string numDoc, DateTime? fechaNacimiento, string? cuil, string? calle, string? altura, int idLocalidad, int idGenero, string? correo, string? celular, DateTime fechaIngreso)
        {
            Validate(legajo.ToString(), nombre, apellido, numDoc, cuil, calle, altura, correo);

            Legajo = legajo;
            Nombre = nombre;
            Apellido = apellido;
            IdTipoDoc = idTipoDoc;
            NumDoc = numDoc;
            FechaNacimiento = fechaNacimiento;
            Cuil = cuil;
            Calle = calle;
            Altura = altura;
            IdLocalidad = idLocalidad;
            IdGenero = idGenero;
            Correo = correo;
            Celular = celular;
            FechaIngreso = fechaIngreso;
        }

        private void Validate(string legajo, string nombre, string apellido, string numDoc, string? cuil, string? calle, string? altura, string? correo)
        {
            if (!int.TryParse(legajo, out _))
                throw new ArgumentException("El legajo debe ser un número válido.", nameof(legajo));
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));
            if (string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El apellido no puede estar vacío.", nameof(apellido));
            if (!long.TryParse(numDoc, out _))
                throw new ArgumentException("El número de documento debe ser numérico.", nameof(numDoc));
            if (!string.IsNullOrWhiteSpace(cuil) && !long.TryParse(cuil, out _))
                throw new ArgumentException("El CUIL debe ser numérico.", nameof(cuil));
            if (string.IsNullOrWhiteSpace(calle))
                throw new ArgumentException("La calle no puede estar vacía.", nameof(calle));
            if (!int.TryParse(altura, out _))
                throw new ArgumentException("La altura de la dirección debe ser un número.", nameof(altura));
            if (string.IsNullOrWhiteSpace(correo) || !IsValidEmail(correo))
                throw new ArgumentException("El formato del correo electrónico no es válido.", nameof(correo));
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}