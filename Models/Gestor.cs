namespace Turnos.Models;

public class Gestor{
    public int Id {get; set;}
    public string? Nombre {get; set;}
    public string? Documento {get; set;}
    public required string Correo {get; set;}
    public string? Contraseña {get; set;}
}