using System.Collections.ObjectModel;
using System.Reactive;
using DVar.RandCreds;
using DynamicData;
using Randik.Utils;
using ReactiveUI;

namespace Randik.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<string> Words { get; set; }

    private string? _selectedWord;

    public string? SelectedWord
    {
        get => _selectedWord;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedWord, value);
            CopyToClipboard(value);
        }
    }

    private string _copiedMessage = string.Empty;

    public string CopiedMessage
    {
        get => _copiedMessage;
        set => this.RaiseAndSetIfChanged(ref _copiedMessage, value);
    }
    
    public ReactiveCommand<Unit, Unit> GenerateCommand { get; }

    public MainWindowViewModel()
    {
        Words = new ObservableCollection<string>();

        void generate()
        {
            var w = RandWord.Words(10, 6);
            Words.Clear();
            Words.AddRange(w);
        }

        generate();

        GenerateCommand = ReactiveCommand.Create(generate);
    }

    private void CopyToClipboard(string? value)
    {
        if (SelectedWord is null) return;
        Clipboard.Get().SetTextAsync(value);
        CopiedMessage = $"\"{value}\" copied to clipboard!";
    }
}