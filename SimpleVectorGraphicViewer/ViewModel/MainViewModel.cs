using SimpleVectorGraphicViewer.Model;
using SimpleVectorGraphicViewer.Methods;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace SimpleVectorGraphicViewer.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string _windowTitle = "Simple Vector Graphic Viewer", _statusBarText ="Plase open a file.", _activeFilePath = "";
        private ObservableCollection<GraphicModel> _graphics;
        private GraphicModel _selectedGraphic;
        private double _scale = 1;
        private bool _fileOpened = false;
        
        
        public string WindowTitle { get => _windowTitle + " | " + _activeFilePath ; set { _windowTitle = value; OnPropertyChanged(nameof(WindowTitle)); } } 
        public ObservableCollection<GraphicModel> Graphics { get => _graphics; set {_graphics = value; OnPropertyChanged(nameof(Graphics));   } }
        public GraphicModel SelectedGraphic { get => _selectedGraphic; set { _selectedGraphic = value; OnPropertyChanged(nameof(SelectedGraphic)); } }
        public double Scale { get => _scale; set { _scale = value; OnPropertyChanged(nameof(Scale)); } }
        public string StatusBarText { get => _statusBarText; set { _statusBarText = value; OnPropertyChanged(nameof(StatusBarText)); } }
        public string ActiveFilePath { get => _activeFilePath; set { _activeFilePath = value; OnPropertyChanged(nameof(ActiveFilePath)); } }
        public bool FileOpened { get => !string.IsNullOrEmpty(_activeFilePath); }


        public ICommand OpenFileCommand { get; }
        public ICommand CloseFileCommand { get; }
        public ICommand SaveFileCommand { get; }
        public ICommand SaveAsXMLCommand { get; }
        public ICommand SaveAsJSONCommand { get; }
        public ICommand ExitCommand { get; }
        public ICommand ShowAboutCommand { get; }
        public ICommand OpenDevsLinkedInCommand { get; }
        public ICommand OpenDevsGitHubCommand { get; }
        public ICommand OpenDevsXingCommand { get; }

        public MainViewModel()
        {
            LoadPage();
            Graphics = new ObservableCollection<GraphicModel>();
            OpenFileCommand = new ViewModelCommand(ExecuteOpenFileCommand);
            CloseFileCommand = new ViewModelCommand(ExecuteCloseFileCommand, IsFileOpened);
            SaveFileCommand = new ViewModelCommand(ExecuteSaveFileCommand, IsFileOpened);
            SaveAsXMLCommand = new ViewModelCommand(ExecuteSaveAsXMLCommand, IsFileChanged);
            SaveAsJSONCommand = new ViewModelCommand(ExecuteSaveAsJSONCommand, IsFileChanged);
            ExitCommand = new ViewModelCommand(ExecuteExitCommand);
            ShowAboutCommand = new ViewModelCommand(ExecuteShowAboutCommand);
            OpenDevsLinkedInCommand = new ViewModelCommand(ExecuteOpenDevsLinkedInCommand);
            OpenDevsGitHubCommand = new ViewModelCommand(ExecuteOpenDevsGitHubCommand);
            OpenDevsXingCommand = new ViewModelCommand(ExecuteOpenDevsXingCommand);
          
        }

  
        private void LoadPage()
        {
            _graphics = new ObservableCollection<GraphicModel>();
            _selectedGraphic = null;
        }

        #region 'MenuBarCommands'
        #region 'FileTab'
        private void ExecuteOpenFileCommand(object obj)
        {
            string filepath = FileProcesses.SelectSourceFile();
            Graphics = FileProcesses.GetGraphics(filepath) ?? Graphics;
            foreach (GraphicModel graphicModel in Graphics)
                Common.MainCanvas.Children.Add(graphicModel.GetShape());
            foreach (UIElement element in Common.MainCanvas.Children)
            {
                double left = (Common.MainCanvas.ActualWidth - element.RenderSize.Width) / 2;
                double top = (Common.MainCanvas.ActualHeight - element.RenderSize.Height) / 2;
                Canvas.SetLeft(element, left);
                Canvas.SetTop(element, top);
            }

        }

        private void ExecuteCloseFileCommand(object obj)
        {
            throw new NotImplementedException();
        }
        private void ExecuteSaveFileCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteSaveAsXMLCommand(object obj)
        {
            throw new NotImplementedException();
        }
        private bool CanExecuteSaveAsJSONCommand(object obj)
        {
            throw new NotImplementedException();
        }
        private void ExecuteSaveAsJSONCommand(object obj)
        {
            throw new NotImplementedException();
        }
        private void ExecuteExitCommand(object obj)
        {
            throw new NotImplementedException();
        }
        private bool IsFileChanged(object obj)
        {
            return false ;
        }

        private bool IsFileOpened(object obj)
        {
            return false ;
        }

        #endregion
        #region 'ViewTab'
        #endregion
        #region 'HelpTab'
        private void ExecuteOpenDevsXingCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteOpenDevsGitHubCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteOpenDevsLinkedInCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteShowAboutCommand(object obj)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region 'CanvasCommands'
        #endregion

        #region 'StatusBarCommands'
        #endregion

    }
}
