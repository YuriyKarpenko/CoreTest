using System.Diagnostics;

namespace SIENN.DbAccess.Model
{
    public interface IDtoBase
    {
        int Id { get; set; }
        string Name { get; set; }
    }

    [DebuggerDisplay("({Id}) '{Name}'")]
    public abstract class DtoBase : IDtoBase
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{this.GetType().Name} [({Id}) '{Name}']";
        }
    }
}
