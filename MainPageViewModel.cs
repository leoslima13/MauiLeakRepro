using System.Collections.ObjectModel;

namespace MemoryLeakTest;

public class MainPageViewModel : BindableObject
{
    public MainPageViewModel()
    {
        CollectGCCommand = new Command(GC.Collect);

        CollectTotalMemoryCommand = new Command(() =>
        {
            TotalMemory = $"{(GC.GetTotalMemory(false)/1024f) / 1024f} MB";
        });

        NavigateForward = new Command(async () =>
        {
            await (Application.Current!.MainPage as NavigationPage)!.Navigation.PushAsync(new MainPage());
        });

        var random = new Random();
        var value = random.Next(10, 50);

        for (var i = 0; i < value; i++)
        {
            Items.Add(CreateItemViewModel(i));
        }

        return;

        ItemViewModel CreateItemViewModel(int index)
        {
            var randomValue = random.Next(0, 4);
            var isEnabled = randomValue % 2 == 0;
            if (randomValue == 0)
                return new ItemViewModel($"Caption {index + 1}", DateTime.Now, FieldType.DatePicker,
                    isEnabled);
            if(randomValue == 1)
                return new ItemViewModel($"Caption {index + 1}", "Label Value", FieldType.Label,
                    isEnabled);
            if(randomValue == 2)
                return new ItemViewModel($"Caption {index + 1}", "Entry Value", FieldType.Entry,
                    isEnabled);
            
            return new ItemViewModel($"Caption {index + 1}", "Button Value", FieldType.Button,
                isEnabled);
        }
        
    }

    private string _totalMemory;

    public string TotalMemory
    {
        get => _totalMemory;
        set
        {
            _totalMemory = value;
            OnPropertyChanged();
        }
    }
    
    public Command CollectGCCommand { get; set; }
    public Command CollectTotalMemoryCommand { get; set; }
    public Command NavigateForward { get; set; }

    public ObservableCollection<ItemViewModel> Items { get; set; } = [];
}

public enum FieldType
{
    Label,
    Entry,
    DatePicker,
    Button
}

public class ItemViewModel
{
    public string Label { get; }
    public object Value { get; }
    public FieldType FieldType { get; }
    public bool IsEnabled { get; }

    public ItemViewModel(string label, object value, FieldType fieldType, bool isEnabled)
    {
        Label = label;
        Value = value;
        FieldType = fieldType;
        IsEnabled = isEnabled;
    }

    ~ItemViewModel()
    {
        System.Diagnostics.Debug.WriteLine("ItemViewModel being destroyed");
    }
}