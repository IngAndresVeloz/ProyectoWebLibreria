using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design
{
    public class LibroDis
    {
        private string titulo;
        private string autor;
        private int anio;
        private string estado;

        public LibroDis()
        {

        }

        public LibroDis(String titulo, int anio, string autor, string estado)
        {
            this.titulo = titulo;
            this.anio = anio;
            this.autor = autor;
            this.estado = estado;
        }

        public string Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }
        public int Anio
        {
            get { return anio; }
            set { anio = value; }
        }
        public string Autor
        {
            get { return autor; }
            set { autor = value; }
        }
        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }

    }
}