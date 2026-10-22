public class Observer {
    
    public List<Action> Events = new();

    public void Trigger() { foreach( Action a in Events ) a(); }

    public void Add( Action action ) { Events.Add( action ); }

    public void Clear() { Events.Clear(); }

}