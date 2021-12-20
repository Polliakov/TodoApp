using System;
using System.ComponentModel;
using System.Windows;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private readonly string path = $"{Environment.CurrentDirectory}\\todoList.json";
        private FileIO fileIO;
        private BindingList<TodoElement> todoList;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fileIO = new FileIO(path);
            LoadTodoList();

            dgTodoList.ItemsSource = todoList;
            todoList.ListChanged += TodoList_ListChanged;
        }

        private void TodoList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged ||
                e.ListChangedType == ListChangedType.ItemAdded ||
                e.ListChangedType == ListChangedType.ItemDeleted)
            {
                SaveTodoList();
            }
        }

        private void BtClearAll_Click(object sender, RoutedEventArgs e)
        {
            var messageBoxResult = MessageBox.Show("Очистить список задач?", "Предупреждение", 
                                                   MessageBoxButton.YesNoCancel);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                todoList.Clear();
            }
            expActions.IsExpanded = false;
        }

        private void BtClearUncompleted_Click(object sender, RoutedEventArgs e)
        {
            RemoveWhere(todoList, elem => !elem.IsDone);
            expActions.IsExpanded = false;
        }

        private void BtClearCompleted_Click(object sender, RoutedEventArgs e)
        {
            RemoveWhere(todoList, elem => elem.IsDone);
            expActions.IsExpanded = false;
        }

        private void SaveTodoList()
        {
            try
            {
                fileIO.SaveData(todoList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                Close();
            }
        }

        private void LoadTodoList()
        {
            try
            {
                todoList = fileIO.LoadData<TodoElement>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                Close();
            }
        }

        private void RemoveWhere(BindingList<TodoElement> list, Predicate<TodoElement> condition)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (condition(list[i]))
                {
                    todoList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}