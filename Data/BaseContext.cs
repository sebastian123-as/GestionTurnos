using Microsoft.EntityFrameworkCore;

namespace Turnos.Data;

public class BaseContext : DbContext{
    public BaseContext(DbContextOptions<BaseContext> options) : base(options){}

    //Aqui van registrados los modelos que usan...


}