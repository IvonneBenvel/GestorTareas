public class Tarea
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public TipoTarea Tipo { get; set; }
    public bool Prioridad { get; set; }

    public override string ToString()
    {
        return $"ID: {Id} | Nombre: {Nombre} | Tipo: {Tipo} | Prioridad: {Prioridad}";
    }

    public string ToFile()
    {
        return $"{Id};{Nombre};{Descripcion};{Tipo};{Prioridad}";
    }
}