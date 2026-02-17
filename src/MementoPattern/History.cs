using System;
using System.Collections.Generic;
using System.Linq;

namespace MementoPattern
{
    /// <summary>
    /// The Caretaker doesn't depend on the Concrete Memento class. 
    /// Therefore, it doesn't have access to the originator's state, which is
    /// stored inside the memento. It works with all mementos via the base
    /// Memento interface.
    /// </summary>
    public class History
    {
        private List<IMemento> _mementos = new List<IMemento>();
        private readonly ImageEditor _originator;

        public History(ImageEditor originator)
        {
            _originator = originator;
        }

        public void Backup()
        {
            Console.WriteLine("[History] Saving state...");
            _mementos.Add(_originator.Save());
        }

        public void Undo()
        {
            if (_mementos.Count == 0)
            {
                Console.WriteLine("[History] No state to undo.");
                return;
            }

            var memento = _mementos.Last();
            _mementos.RemoveAt(_mementos.Count - 1);

            try
            {
                _originator.Restore(memento);
            }
            catch (Exception)
            {
                Undo();
            }
        }

        public void ShowHistory()
        {
            Console.WriteLine("[History] List of mementos:");
            foreach (var memento in _mementos)
            {
                Console.WriteLine(memento.GetName());
            }
        }
    }
}
