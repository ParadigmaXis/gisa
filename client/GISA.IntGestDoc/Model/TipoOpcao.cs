using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.IntGestDoc.Model
{
    public enum TipoOpcao
    {
        Sugerida = 0,        
        Trocada = 1,
        Original = 2,
        Ignorar = 3
    }

    public enum TipoEstado
    {
        Novo = 0,
        Editar = 1,
        Apagado = 2,
        SemAlteracoes = 3
    }
}
