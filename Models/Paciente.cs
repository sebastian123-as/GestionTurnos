namespace Turnos.Models;

public class Paciente{
    public int Id {get; set;}
    public required string Documento {get; set;}
    public string? Nombre {get; set;}
    public int IdTipoDocumento {get; set;}
    
}