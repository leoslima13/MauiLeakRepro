namespace MemoryLeakTest;

public class DisposableBehavior : Behavior<CollectionView>
{
    private CollectionView? _associatedObject;
    private readonly Dictionary<Guid, VisualElement> _collectionElements = new(); 

    protected override void OnAttachedTo(CollectionView bindable)
    {
        base.OnAttachedTo(bindable);
        _associatedObject = bindable;

        _associatedObject.ChildAdded += OnChildAdded;
        _associatedObject.ChildRemoved += OnChildRemoved;
    }
    
    protected override void OnDetachingFrom(CollectionView bindable)
    {
        base.OnDetachingFrom(bindable);
        _associatedObject!.ChildAdded -= OnChildAdded;
        _associatedObject.ChildRemoved -= OnChildRemoved;
        if(_collectionElements.Count > 0)
            foreach (var pair in _collectionElements)
            {
                ReleaseEventsAndDestroy(pair.Value);
            }
        _associatedObject = null;
    }
    
    private void OnChildAdded(object? sender, ElementEventArgs e)
    {
        _collectionElements.TryAdd(e.Element.Id, (e.Element as VisualElement)!);
    }
    
    private void OnChildRemoved(object? sender, ElementEventArgs e)
    {
        var toBeRemoved = e.Element.Id;
        if (!_collectionElements.Remove(toBeRemoved, out var element)) return;

        ReleaseEventsAndDestroy(element);
    }

    private void ReleaseEventsAndDestroy(VisualElement element)
    {
        if (element is Layout layout)
        {
            foreach (var view in layout.Children)
            {
                ReleaseEventsAndDestroy((view as VisualElement)!);
            }
        }
        else
        {
            element.Behaviors.Clear();
            if(element is IDisposable disposable)
                disposable.Dispose();
            
            (element.Handler)?.DisconnectHandler();
            //Not sure about that
            element.BindingContext = null;
            element = null!;
        }
    }
}