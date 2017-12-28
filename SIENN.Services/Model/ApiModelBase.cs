using System.Diagnostics;

namespace SIENN.Services.Model
{
    public interface IApiModelBase
    {
        int Code { get; set; }
        string Description { get; set; }
    }

    [DebuggerDisplay("({Code}) '{Description}'")]
    public abstract class ApiModelBase : IApiModelBase
    {
        public int Code { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{this.GetType().Name} [({Code}) '{Description}']";
        }
    }
}
