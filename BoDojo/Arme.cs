using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BO
{
    public class Arme: IdGenerated
    {
        public string Nom { get; set; }
        public int Degats { get; set; }
    }
}