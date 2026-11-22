public interface IInteractable
{
    string ObjectID { get; }
    void  Interact();
    bool CanInteract();
}
