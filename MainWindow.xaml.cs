// Импорт необходимых пространств имен для использования стандартных библиотек .NET и Prism Commands.
using System; // Общие классы и структуры .NET.
using System.Collections.ObjectModel; // Классы для работы с коллекциями, такими как ObservableCollection.
using System.ComponentModel; // Классы для реализации поддержки привязки данных.
using System.Runtime.CompilerServices; // Классы для поддержки атрибутов компилятора C#.
using System.Windows; // Классы для создания приложений Windows.
using System.Windows.Input; // Классы для обработки пользовательского ввода в приложениях Windows.
using Prism.Commands; // Классы для реализации команд с использованием паттерна команд Prism.

// Пространство имен для приложения ToDoListApp.
namespace ToDoListApp
{
    // Класс представляет элемент задачи в списке дел.
    public class TaskItem : INotifyPropertyChanged
    {
        private string title; // Заголовок задачи.
        private bool isCompleted; // Признак завершенности задачи.

        // Свойство для заголовка задачи.
        public string Title
        {
            get { return title; } // Получает заголовок задачи.
            set // Устанавливает новое значение заголовка задачи и вызывает событие изменения свойства.
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged(); // Вызывает событие PropertyChanged при изменении свойства.
                }
            }
        }

        // Свойство для отметки завершенности задачи.
        public bool IsCompleted
        {
            get { return isCompleted; } // Получает значение завершенности задачи.
            set // Устанавливает новое значение завершенности задачи и вызывает событие изменения свойства.
            {
                if (isCompleted != value)
                {
                    isCompleted = value;
                    OnPropertyChanged(); // Вызывает событие PropertyChanged при изменении свойства.
                }
            }
        }

        // Событие, возникающее при изменении значения свойства.
        public event PropertyChangedEventHandler PropertyChanged;

        // Метод, вызывающий событие PropertyChanged.
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Класс, представляющий модель представления (ViewModel) для главного окна.
    public class MainViewModel : INotifyPropertyChanged
    {
        private string newTaskTitle; // Новый заголовок задачи.
        private ObservableCollection<TaskItem> tasks; // Коллекция задач.

        // Конструктор класса MainViewModel.
        public MainViewModel()
        {
            tasks = new ObservableCollection<TaskItem>(); // Инициализация коллекции задач.
            AddTaskCommand = new DelegateCommand(AddTask); // Создание команды для добавления задачи.
            DeleteTaskCommand = new DelegateCommand<TaskItem>(DeleteTask); // Создание команды для удаления задачи.
        }

        // Свойство для нового заголовка задачи.
        public string NewTaskTitle
        {
            get { return newTaskTitle; } // Получает новый заголовок задачи.
            set // Устанавливает новый заголовок задачи и вызывает событие изменения свойства.
            {
                if (newTaskTitle != value)
                {
                    newTaskTitle = value;
                    OnPropertyChanged(); // Вызывает событие PropertyChanged при изменении свойства.
                }
            }
        }

        // Свойство для коллекции задач.
        public ObservableCollection<TaskItem> Tasks
        {
            get { return tasks; } // Получает коллекцию задач.
            set // Устанавливает новое значение коллекции задач и вызывает событие изменения свойства.
            {
                if (tasks != value)
                {
                    tasks = value;
                    OnPropertyChanged(); // Вызывает событие PropertyChanged при изменении свойства.
                }
            }
        }

        // Команда для добавления задачи.
        public ICommand AddTaskCommand { get; }

        // Команда для удаления задачи.
        public ICommand DeleteTaskCommand { get; }

        // Метод для добавления задачи.
        private void AddTask()
        {
            if (!string.IsNullOrWhiteSpace(NewTaskTitle)) // Проверка на пустой заголовок задачи.
            {
                Tasks.Add(new TaskItem { Title = NewTaskTitle }); // Добавление новой задачи в коллекцию.
                NewTaskTitle = string.Empty; // Очистка поля нового заголовка задачи.
            }
        }

        // Метод для удаления задачи.
        private void DeleteTask(TaskItem taskItem)
        {
            if (taskItem != null) // Проверка на null.
            {
                Tasks.Remove(taskItem); // Удаление задачи из коллекции.
            }
        }

        // Событие, возникающее при изменении значения свойства.
        public event PropertyChangedEventHandler PropertyChanged;

        // Метод, вызывающий событие PropertyChanged.
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Класс, представляющий главное окно приложения.
    public partial class MainWindow : Window
    {
        // Конструктор класса MainWindow.
        public MainWindow()
        {
            InitializeComponent(); // Инициализация компонентов окна.
            DataContext = new MainViewModel(); // Установка контекста данных для окна.
        }
    }
}

