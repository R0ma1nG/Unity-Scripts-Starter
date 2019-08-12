package event

import objects.TranslationItem
import tornadofx.FXEvent

data class ImportSuccessEvent(
    val translations: List<TranslationItem>,
    val languages: List<String>
) : FXEvent()
