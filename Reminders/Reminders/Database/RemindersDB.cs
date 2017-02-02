using Reminders.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Reminders.Database
{
    public class RemindersDB
    {
        readonly SQLiteAsyncConnection database;

        public RemindersDB(string path)
        {
            //instancia conexao
            database = new SQLiteAsyncConnection(path);

            //cria tabela de reminders
            database.CreateTableAsync<Reminder>().Wait();
        }

        public Task<List<Reminder>> GetItemsAsync()
        {
            return database.Table<Reminder>().ToListAsync();
        }

        public Task<int> InsertItemAsync(Reminder item)
        {
            return database.InsertAsync(item);
        }

        public Task<int> UpdateItemAsync(Reminder item)
        {
            return database.UpdateAsync(item);
        }

        public Task<int> DeleteItemAsync(Reminder item)
        {
            return database.DeleteAsync(item);
        }

        internal Task<List<Reminder>> GetItemsSelectedAsync(string filter)
        {
            return database.Table<Reminder>().Where(x => x.Titulo.Contains(filter) || x.Descricao.Contains(filter)).ToListAsync();
        }

        internal Task<List<Reminder>> GetItemsOrderedAsync(string order)
        {
            //TODO usar reflection
            switch (order)
            {
                case "Título":
                    return database.Table<Reminder>().OrderBy(x => x.Titulo).ToListAsync();
                case "Data Limite":
                    return database.Table<Reminder>().OrderBy(x => x.DataHoraLimite).ToListAsync();
                default:
                    return database.Table<Reminder>().OrderBy(x => x.Titulo).ToListAsync();
            }
        }
    }
}
