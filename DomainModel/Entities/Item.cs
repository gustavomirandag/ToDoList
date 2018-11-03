using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace DomainModel.Entities
{
    public class Item : INotifyPropertyChanged
    {
        public Guid Id { get; set; }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set => SetPropertyAndNotify(ref _title, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetPropertyAndNotify(ref _description, value, "Description");
        }

        private DateTime _publishDateTime;
        public DateTime PublishDateTime
        {
            get => _publishDateTime;
            set => SetPropertyAndNotify(ref _publishDateTime, value);
        }

        public Item()
        {
            Title = String.Empty;
            Description = String.Empty;
            PublishDateTime = DateTime.Now;
        }

        public Item(string title, string description)
        {
            Id = Guid.NewGuid();
            Title = title ?? String.Empty; //Return left hand if not null or apply ""
            Description = description ?? String.Empty;
            PublishDateTime = DateTime.Now;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Dispara notificação apenas se o valor de uma propriedade foi alterada.
        /// </summary>
        /// <typeparam name="T">Tipo da propriedade</typeparam>
        /// <param name="variable">Referencia </param>
        /// <param name="value">Valor a ser atribuido.</param>
        /// <param name="propertyNames">Nome da propriedade para notificar os listeners.  
        /// Nome da propriedade para notificar os listeners.  
        /// Esse valor é opcional porque será preenchido pelo compilador através do CallerMemberName.</param>
        /// <returns>Retorna TRUE se o valor foi alterado.</returns>
        protected bool SetPropertyAndNotify<T>(ref T variable, T value, [CallerMemberName]string propertyName = null)
        {
            if (object.Equals(variable, value))
                return false;

            variable = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Notifica aos listeners que uma determinada propriedade mudou..
        /// </summary>
        /// <param name="propertyNames">Nome da propriedade para notificar os listeners.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
