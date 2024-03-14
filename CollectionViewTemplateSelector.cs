
namespace MemoryLeakTest;

public class CollectionViewTemplateSelector : DataTemplateSelector
{
    private readonly DataTemplate _labelTemplate = new LabelTemplate();
    private readonly DataTemplate _entryTemplate = new EntryTemplate();
    private readonly DataTemplate _datePickerTemplate = new DatePickerTemplate();
    private readonly DataTemplate _buttonTemplate = new ButtonTemplate();
    
    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is not ItemViewModel itemViewModel)
            throw new Exception();

        return itemViewModel.FieldType switch
        {
            FieldType.Button => _buttonTemplate,
            FieldType.Entry => _entryTemplate,
            FieldType.Label => _labelTemplate,
            FieldType.DatePicker => _datePickerTemplate,
            _ => throw new Exception()
        };
    }
}