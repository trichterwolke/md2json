using System;
using System.Collections.Generic;
using System.Text;

namespace Markdown2Json.Entities
{
    public enum PageType : int
    {
        // Hauptabsatz (1)
        Section = 1,
        
        // Unterabsatz (1.1)
        SubSection = 2,

        // Unterunterabsatz (1.1.1)
        SubSubSection = 3,

        // Neue Seite aber keine (sichtbare) Nummerierung
        Segment = 4,       
    }
}
