using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BO
{
    public class Samourai: IdGenerated
    {
        
        public int Force { get; set; }
        public string Nom { get; set; }
        
        public virtual Arme Arme { get; set; }
        public virtual List<ArtMartial> ArtMartials { get; set; }
    }
}
