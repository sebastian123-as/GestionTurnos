using Microsoft.EntityFrameworkCore;
using Turnos.Models;

namespace Turnos.Data;

public class BaseContext : DbContext{
    public BaseContext(DbContextOptions<BaseContext> options) : base(options){
        
    }

    //Aqui van registrados los modelos que usan...
    public DbSet<Gestor> Gestores {get; set;}
    public DbSet<TipoDocumento> TipoDocumento {get; set;}
    public DbSet<Paciente> Pacientes {get; set;}
    public DbSet<TipoTurno> TipoTurno {get; set;}
    public DbSet<Turno> Turnos {get; set;}

}