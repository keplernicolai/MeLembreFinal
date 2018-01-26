using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeLembreFinal.Models
{
    public class Aniversario : RealmObject
    {
        [PrimaryKey]
        public long Id { get; set; }

        public string TipoAniver { get; set; }
        public string NomePessoa { get; set; }
        public string Titulo { get; set; }
        public DateTimeOffset? DataAniver { get; set; }
        public string Detalhes { get; set; }
        public bool SeteDias { get; set; }
        public bool UmDia { get; set; }
        public bool NoDia { get; set; }
        public DateTimeOffset? DataCriacao { get; set; }
        public string Foto { get; set; }
    }
}
