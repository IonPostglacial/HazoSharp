using Microsoft.EntityFrameworkCore;

namespace Hazo.Models;

public interface IHazoModel
{
    public static abstract void ConfigureModel(ModelBuilder modelBuilder);
}