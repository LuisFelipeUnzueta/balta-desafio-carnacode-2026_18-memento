using System;

namespace MementoPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Memento Pattern Challenge: Modern & Efficient Image Editor ===\n");

            var editor = new ImageEditor(1920, 1080);
            var history = new History(editor);

            // Initial state
            history.Backup();
            editor.DisplayInfo();

            // Editing state
            Console.WriteLine("--- Applying Changes ---");
            editor.ApplyBrightness(20);
            history.Backup();

            editor.ApplyFilter("Sepia");
            history.Backup();

            editor.Rotate(90);
            history.Backup();

            editor.Crop(1280, 720);
            // Not saving the crop yet
            editor.DisplayInfo();

            Console.WriteLine("--- Performing Undos ---");
            history.Undo(); // Reverts to before Crop
            editor.DisplayInfo();

            history.Undo(); // Reverts to before Rotate
            editor.DisplayInfo();

            history.Undo(); // Reverts to before Filter
            editor.DisplayInfo();
            Console.WriteLine("\nChallenge Completed with Success!");

            /*
             * 1. How to capture state without violating encapsulation?
             * 
             * SOLUTION: We use a private nested Memento class. Only the 'Originator' (ImageEditor) 
             * can see the fields inside 'ImageMemento'. The 'Caretaker' (History) only interacts 
             * with the 'IMemento' marker interface, which doesn't expose any state.
             */

            /*
             * 2. How to store snapshots efficiently?
             * 
             * SOLUTION: 
             * - Encapsulation avoids 'Getter/Setter' explosion, reducing surface area for accidental copies.
             * - We clone only necessary mutable parts (like the pixels array) to ensure integrity.
             * - In a real-world scenario with massive images, we could implement 'Incremental Mementos' (Delta),
             *   saving only the changed pixels or the operation details instead of the full buffer.
             */

            /*
             * 3. How to allow an object to restore itself without exposing internals?
             * 
             * SOLUTION: The 'Restore' method is part of the Originator. It takes an 'IMemento' 
             * and performs a type check. Because 'ImageMemento' is internal/nested, the Originator 
             * can access its raw data and apply it back to its own fields without any public setters.
             */

            /*
             * 4. How to externalize state while maintaining integrity?
             * 
             * SOLUTION: By using a specific 'IMemento' interface, we ensure that the state 'container' 
             * is opaque to the outside world. The state is 'externalized' to the History class, 
             * but it remains sealed and tamper-proof because the History class cannot modify it.
             */
        }
    }
}
