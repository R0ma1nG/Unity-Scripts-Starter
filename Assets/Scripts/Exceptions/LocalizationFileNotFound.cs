using System;

namespace Exceptions {
    public class LocalizationFileNotFound : Exception {
        public LocalizationFileNotFound(string message) : base(message) {
            /* noop*/
        }
    }
}