namespace MemoryLeakTest;

public class ZombieBehavior : Behavior<View>
{
    protected override void OnAttachedTo(View bindable)
    {
        base.OnAttachedTo(bindable);
        System.Diagnostics.Debug.WriteLine($"ZombieBehavior attached to {bindable}");
    }

    protected override void OnDetachingFrom(View bindable)
    {
        base.OnDetachingFrom(bindable);
        System.Diagnostics.Debug.WriteLine($"ZombieBehavior detaching from {bindable}");
    }
}