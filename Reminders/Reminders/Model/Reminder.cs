using SQLite;
using System;

namespace Reminders.Model
{
    public class Reminder
    {
        [PrimaryKey, AutoIncrement]
        public int IdReminder { get; set; }

        string titulo;
        string descricao;
        string detalhes;
        bool completo;
        DateTime dataLimite;
        TimeSpan horaLimite;

        public DateTime DataHoraLimite {
            get {
                var data = DataLimite;
                data = data.AddHours(horaLimite.Hours);
                data = data.AddMinutes(horaLimite.Minutes);
                data = data.AddSeconds(horaLimite.Seconds);
                return data;
            }
        }

        public DateTime DataLimite
        {
            get
            {
                return dataLimite.Date;
            }

            set
            {
                dataLimite = value;
            }
        }

        public TimeSpan HoraLimite
        {
            get
            {
                return horaLimite;
            }

            set
            {
                horaLimite = value;
            }
        }

        public string Titulo
        {
            get
            {
                return titulo;
            }

            set
            {
                titulo = value;
            }
        }

        public string Descricao
        {
            get
            {
                return descricao;
            }

            set
            {
                descricao = value;
            }
        }

        public string Detalhes
        {
            get
            {
                return detalhes;
            }

            set
            {
                detalhes = value;
            }
        }

        public bool Completo
        {
            get
            {
                return completo;
            }

            set
            {
                completo = value;
            }
        }
    }
}
