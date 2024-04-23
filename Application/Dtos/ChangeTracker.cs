namespace Application.Dtos
{

    public class ChangeTracker<T>
    {

        #region Fields

        private bool _hasBeenSet;
        private T? _value;

        #endregion
        #region Constructors

        private ChangeTracker() { }

        public ChangeTracker(T value)
            => this.Value = value;

        public ChangeTracker(T value, bool hasBeenSet)
        {
            this._value = value;
            this.HasBeenSet = hasBeenSet;
        }

        #endregion
        #region Properties

        public bool HasBeenSet { get => this._hasBeenSet; protected set => this._hasBeenSet = value; }

        public T Value
        {
            get => this._value;
            set
            {
                this._value = value;
                this.HasBeenSet = true;
            }
        }

        #endregion

    }

}
