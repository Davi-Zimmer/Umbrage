public abstract class Decision {
    
    public string Name { get; protected set; }

    public abstract float Evaluate();

    public abstract void Execute();

    public virtual bool CanContinue() { return true; }

}