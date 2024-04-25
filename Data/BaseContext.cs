using Microsoft.EntityFrameworkCore;
using Turnos.Models;

namespace Turnos.Data;

public class BaseContext : DbContext{
    public BaseContext(DbContextOptions<BaseContext> options) : base(options){}

    //Aqui van registrados los modelos que usan...
    DbSet<Gestor> Gestores {get; set;}
    DbSet<TipoDocumento> TipoDocumento {get; set;}
    DbSet<Paciente> Pacientes {get; set;}
    DbSet<TipoTurno> TipoTurno {get; set;}
    DbSet<Turno> Turnos {get; set;}

}