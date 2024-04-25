namespace Turnos.Models;

public class Turno{
    public int Id {get; set;}
    public bool Discapacidad {get; set;}
    public string? Tiket {get; set;}
    public bool Estado {get; set;}
    public int IdTipoTurno {get; set;}
    public int IdPaciente {get; set;}

}